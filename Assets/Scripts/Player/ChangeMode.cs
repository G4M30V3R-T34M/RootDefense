using FeTo.SOArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeMode : MonoBehaviour
{
    [SerializeField]
    InputActionReference changeMode;
    [SerializeField]
    GameEvent changeCamera;
    private void OnEnable() {
        Debug.Log("Performed subscription");
        changeMode.action.performed += DoChangePointOfView;
    }

    private void OnDisable() {
        changeMode.action.performed -= DoChangePointOfView;
    }

    public void DoChangePointOfView(InputAction.CallbackContext obj) {
        Debug.Log("Perfomring - Changemode - DoChangePointOfView");
        changeCamera.Raise();
    }
}
