using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{
    [Header("Player Status")]
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float jumpHeight = 4.0f;
    private bool isGrounded = false;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
        //Get Ausdio Listener from camera
        AudioListener audioListener = GetComponentInChildren<AudioListener>();
        //Get Camera
        Camera camera = GetComponentInChildren<Camera>();

        //If the current instance is the local player
        if(isLocalPlayer)
        {
            //Enable everything
            camera.enabled = true;
            audioListener.enabled = true;
        }
        else
        {
            //Disable everything
            camera.enabled = false;
            audioListener.enabled = false;
            AssignRemoteLayer();

        }
        RegisterPlayer();
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }

    #region FUNCTIONS
    void Move(KeyCode _key)
    {
        Vector3 position = rigid.position;
        Quaternion rotation = rigid.rotation;

        switch (_key)
        {
            case KeyCode.W:
                position += transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.S:
                position += -transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                //To Rotate player but we have the player LookScript!(so we dont need it)
                //rotation *= Quaternion.AngleAxis(-rotationSpeed, Vector3.up);
                position += -transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.D:
                //To Rotate player but we have the player LookScript!(so we dont need it)
                //rotation *= Quaternion.AngleAxis(rotationSpeed, Vector3.up);
                position += transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.Space:
                if (isGrounded)
                {
                    rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    isGrounded = false;
                }
                break;

        }
        rigid.MovePosition(position);
        rigid.MoveRotation(rotation);
    }
    void HandleInput()
    {
        KeyCode[] keys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space };
        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }

        }
    }
    private void OnCollisionEnter(Collision _col)
    {
        isGrounded = true;
    }
    void RegisterPlayer()
    {
        //Get the id from the network identity component
        string ID = "Player" + GetComponent<NetworkIdentity>().netId;
        this.name = ID; // Assign new ID to name
    }
    void AssignRemoteLayer()// Assign remote layer to current gameObject (if it is not local player)
    {
        gameObject.layer = LayerMask.NameToLayer("RemoteLayer");
    }
    #endregion

}
