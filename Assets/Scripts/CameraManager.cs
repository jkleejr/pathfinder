using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")]
    public Vector2 cameraOffset = new Vector2(10f, 14f); // 10 is x, 14 is y

    public float lookAtOffset = 2f; // raise offset 2 units

    [Header("Move Controls")] // speed for controls
    public float inOutSpeed = 5f; 
    public float lateralSpeed = 5f;  
    public float rotateSpeed = 45f;

    [Header("Move Bounds")] // where focal point can actually move
    public Vector2 minBounds, maxBounds;

    [Header("Zoom Controls")]
    public float zoomSpeed = 4f;
    public float nearZoomLimit = 2f;
    public float farZoomLimit = 16f;
    public float startingZoom = 5f;

    IZoomStrategy zoomStrategy; 
    Vector3 frameMove; // collecting multiple movement inputs on a frame
    float frameRotate; // collect any rotation info
    float frameZoom; // collect zoom info
    Camera cam;


    private void Awake() {
        cam = GetComponentInChildren<Camera>(); // get camera
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x)); //position camera based on offset, abs to make sure it is above the ground
        // -mathf.abs cameraoffset.x, x (z) is the horizonal value of offset, so camera is - on the z axis facing toward 0 focal point
        zoomStrategy = new OrthographicZoomStrategy(cam, startingZoom); 
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset); 

    }

    private void OnEnable() { //
        KeyboardInputManager.OnMoveInput += UpdateFrameMove;
        KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
      
    }
    private void OnDisable() { 
        KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
        KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
       
    }

    private void UpdateFrameMove(Vector3 moveVector) {
        frameMove += moveVector;
    }
    private void UpdateFrameRotate(float rotateAmount)
    {
        frameRotate += rotateAmount;
    }
    private void UpdateFrameZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    private void LateUpdate() {
        if (frameMove != Vector3.zero) { 
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            // adjust position, translate speedmodframemove specific to our current rotation and direction
            transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
            LockPositionInBounds(); // lock position
            frameMove = Vector3.zero;

        }
        if (frameRotate != 0f)
        {
            transform.Rotate(Vector3.up, frameRotate * Time.deltaTime * rotateSpeed);
            frameRotate = 0f;
        }

        if (frameZoom < 0f)
        {
            zoomStrategy.ZoomIn(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0f;
        }
        else if (frameZoom > 0f)
        {
            zoomStrategy.ZoomOut(cam, Time.deltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0f;
        }
    }

    private void LockPositionInBounds() // ensure within the bounds
    {
        transform.position = new Vector3(

            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y)

        );
    }
    
    
 }



