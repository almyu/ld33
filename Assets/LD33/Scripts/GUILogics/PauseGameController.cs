using UnityEngine;

public class PauseGameController : MonoSingleton<PauseGameController> {
    
    private void OnEnable() {
        TimescaleStack.Push(0.0f);
    }

    private void OnDisable() {
        TimescaleStack.Pop();
    }
}
