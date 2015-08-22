using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public Transform inner;
    public Transform pitcher;
    public Animator animator;

    public float speed = 3f;
    public float jumpSpeed = 5f;
    public float extraJumpTime = 0.1f;
    public LayerMask groundLayers = 1;
    public float groundThreshold = 0.1f;
    public Vector2 mouseSens = Vector2.one;

    private bool grounded = false;
    private float groundDistance;
    private Vector3 jumpVelocity;
    private float jumpTimer;

    private Rigidbody body;


    private void Awake() {
        body = GetComponent<Rigidbody>();
    }

    private void Update() {
        CheckGround();
        ApplyGravity();
        HandleMovement();
        HandleJumping();
        HandleTurning();
        UpdateAnimator();
        UpdateInner();
    }

    private void HandleMovement() {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (input.sqrMagnitude > 1f)
            input.Normalize();

        var move = transform.TransformDirection(input) * speed;

        if (body.isKinematic)
            transform.position += move * Time.deltaTime;
        else
            body.velocity = move.WithY(body.velocity.y);
    }

    private void CheckGround() {
        var ray = new Ray(transform.position + Vector3.up * groundThreshold, Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, groundLayers)) {
            groundDistance = hit.distance - groundThreshold;
            grounded = groundDistance <= groundThreshold;
        }
        else {
            grounded = false;
            groundDistance = 0f;
        }

        if (grounded) jumpTimer = extraJumpTime;
        else jumpTimer -= Time.deltaTime;
    }

    private void ApplyGravity() {
        if (!body.isKinematic) return;

        var deltaVelocity = Physics.gravity * Time.deltaTime;
        var deltaPosition = jumpVelocity + deltaVelocity * 0.5f;

        if (deltaPosition.y < 0f && deltaPosition.y < -groundDistance)
            deltaPosition.y = -groundDistance;

        jumpVelocity += deltaVelocity;
        transform.position += deltaPosition;
    }

    private void HandleJumping() {
        if (!grounded && jumpTimer < 0f) return;
        if (!Input.GetButtonDown("Jump")) return;

        if (body.isKinematic) jumpVelocity = Vector3.up * jumpSpeed;
        else body.velocity += Vector3.up * jumpSpeed;
    }

    private void HandleTurning() {
        var yaw = Input.GetAxis("Mouse X");
        var pitch = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, yaw * mouseSens.x);
        pitcher.Rotate(Vector3.right, pitch * mouseSens.y, Space.Self);
    }

    private void UpdateAnimator() {
        animator.SetFloat("Forward", body.velocity.WithY(0f).magnitude, 0.1f, Time.deltaTime);
        animator.SetFloat("Jump", grounded ? 0f : 1f, 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", grounded);
    }

    private void UpdateInner() {
        var planarVelocity = body.velocity.WithY(0f);
        if (planarVelocity.sqrMagnitude < 0.1f) return;

        var desiredFacing = Quaternion.LookRotation(planarVelocity);
        inner.rotation = Quaternion.Slerp(inner.rotation, desiredFacing, Time.deltaTime * 10f);
    }
}
