using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookScript : NetworkBehaviour
{
    public float mouseSensitivity = 2.0f;
    public float minimumY = -90f;
    public float maximumY = 90f;

    //Yaw of the camera (Rotation on Y)
    private float yaw = 0f;
    //Pitch of the camera (rotation on X)
    private float pitch = 0f;

    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera cam = GetComponentInChildren<Camera>();
        if(cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(isLocalPlayer)
        {
            HandleInput();
        }
    }
    private void LateUpdate()
    {
        if(isLocalPlayer)
        {
            //Camera to look up and down
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void HandleInput()
    {
        //Set yaw to (rotationYaxis) + MouseX * mouseSensitivity
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        //Set pitch to pitch + MouseY * mouseSensitivity
        pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        //Clamp pitch between Minimum Y and MaximumY
        pitch = Mathf.Clamp(pitch, minimumY, maximumY);
        //Set local rotationY(axis) to yaw
        transform.Rotate(0, yaw, 0);
        
    }
}
