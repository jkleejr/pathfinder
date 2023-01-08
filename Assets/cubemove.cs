using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubemove : MonoBehaviour
{
    public int speed = 300;
    bool isMoving = false; // prevent moving while in roll


    void Update() {
        if (isMoving) {
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            StartCoroutine(Roll(Vector3.right));
        } 
        else if (Input.GetKey(KeyCode.LeftArrow)){
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.UpArrow)){
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            StartCoroutine(Roll(Vector3.back));
        }
    }


    IEnumerator Roll(Vector3 direction) { // roll coroutine, for all directions
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction /2 + Vector3.down/2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction); 
        // x perpendicular to direction y, cross product works

        while (remainingAngle > 0) { 
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle); // angle of rotation can never be > than remaining angle
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null; // return null to continue execution on next frame
        }

        isMoving = false;
    }
}

