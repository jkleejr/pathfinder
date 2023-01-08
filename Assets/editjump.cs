using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editjump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D> ();
    }

    // void FixedUpdate(){ // if falling apply fall multiplier
    //    if (rb.velocity.y < 0) {
    //         rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
    //     } 
    //     else if (rb.velocity.y > 0 && !Input.GetButton ("Jump"))
    //     {
    //         rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    //     }
    // }

    void FixedUpdate(){
        if (rb.velocity.y < 0) {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton ("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else // when actively jumping or at rest
        {
            rb.gravityScale = 1f; //normal
        }
    }
}

