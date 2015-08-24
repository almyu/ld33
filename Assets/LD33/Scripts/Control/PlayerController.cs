using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public Transform model;
    public Transform turner, pitcher;
    public Animator animator;

    public float animationSpeed = 3f;
    public float speed = 3f;
    public float jumpSpeed = 5f;
    public float extraJumpTime = 0.1f;
    public float airSpeedFactor = 1f;
    public AnimationCurve airSpeedFalloff = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    public float jumpKickFactor = 1f;
    public AnimationCurve jumpKickFalloff = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    public LayerMask groundLayers = 1;
    public float groundThreshold = 0.1f;
    public Vector2 mouseSens = new Vector2(2, -1);
    public Vector2 stickSens = new Vector2(4, -2);
    public Vector2 pitchRange = new Vector2(-60f, 90f);

    private bool grounded;
    private float airTime, nextJumpTimer;
    private Vector3 jumpKick;

    private Rigidbody body;


    private void Awake() {
        body = GetComponent<Rigidbody>();
    }

    private void Update() {
        CheckGround();
        HandleTurning();
        HandleMovement();
        HandleJumping();
        UpdateAnimator();
        UpdateModel();
    }

    private void HandleMovement() {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (input.sqrMagnitude > 1f)
            input.Normalize();

        var move = turner.TransformDirection(input) * speed;

        if (!grounded) {
            move *= airSpeedFactor * airSpeedFalloff.Evaluate(airTime);
            move += jumpKick * jumpKickFalloff.Evaluate(airTime);
        }

        body.velocity = move.WithY(body.velocity.y);
    }

    private void CheckGround() {
        var ray = new Ray(transform.position + Vector3.up * groundThreshold, Vector3.down);

        RaycastHit hit;
        grounded = Physics.Raycast(ray, out hit, groundLayers.value) && hit.distance <= groundThreshold * 2f;

        if (grounded) airTime = 0f;
        else airTime += Time.deltaTime;
    }

    private void HandleJumping() {
        nextJumpTimer -= Time.deltaTime;

        if (!grounded) return;
        if (airTime > extraJumpTime || nextJumpTimer > 0f) return;
        if (!Input.GetButtonDown("Jump")) return;

        jumpKick = body.velocity.WithY(0f) * jumpKickFactor;
        nextJumpTimer = extraJumpTime;

        body.velocity += Vector3.up * jumpSpeed + jumpKick;
    }

    private void HandleTurning() {
        var deltaTurn = Input.GetAxis("Mouse X") * mouseSens.x + Input.GetAxis("RightHorizontal") * stickSens.x * Time.deltaTime;
        var deltaPitch = Input.GetAxis("Mouse Y") * mouseSens.y + Input.GetAxis("RightVertical") * stickSens.y * Time.deltaTime;

        turner.Rotate(Vector3.up, deltaTurn);

        var pitcherEuler = pitcher.localEulerAngles;
        var signedPitch = pitcherEuler.x <= 180f ? pitcherEuler.x : pitcherEuler.x - 360f;
        pitcherEuler.x = Mathf.Clamp(signedPitch + deltaPitch, pitchRange[0], pitchRange[1]);
        pitcher.localEulerAngles = pitcherEuler;
    }

    private void UpdateAnimator() {
        animator.SetFloat("Forward", body.velocity.WithY(0f).magnitude * animationSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("Jump", grounded ? 0f : 1f, 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", grounded);
    }

    private void UpdateModel() {
        var planarVelocity = body.velocity.WithY(0f);
        if (planarVelocity.sqrMagnitude < 0.1f) return;

        var desiredFacing = Quaternion.LookRotation(planarVelocity);
        model.rotation = Quaternion.Slerp(model.rotation, desiredFacing, Time.deltaTime * 10f);
    }
}
