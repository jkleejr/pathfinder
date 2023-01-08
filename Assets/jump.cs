using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    [Range(1,10)]
    public float jumpVelocity;

    bool jumpRequest;

    void Update(){
        if (Input.GetButtonDown ("Jump")) {
            jumpRequest = true;
        }
    }

    void FixedUpdate(){ // handling unity physics properly
        if (jumpRequest) { // if true
            //GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpVelocity;

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity);

            jumpRequest = false;
        }
    }
}
