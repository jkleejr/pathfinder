using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedjump : MonoBehaviour
{
    // this jump is not fixed just for me to test
    [Range(1,10)]
    public float jumpVelocity;

    void FixedUpdate(){
        if (Input.GetButtonDown ("Jump")) {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }
}

