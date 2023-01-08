using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : InputManager // inherit from inputmanager
{
    // events that will use delegates
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;


    void Update()
    {
        // move camera, wasd
        if (Input.GetKey(KeyCode.W)) {
            OnMoveInput?.Invoke(Vector3.forward);
        }

    }
}

