﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour
{
    //Speed of lerping rotation & position
    public float lerpRate = 15;

    //Threshold for when send commands
    public float positionThreshold = 0.5f;
    public float rotationThreshold = 5.0f;

    //Records the previous position & rotation that was sent to the server
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    //Variables to be synced across the network
    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;

    //Obtain rigidbody
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();

        TransmitRotation();
        LerpRotation();

    }
    #region FUNCTION
    void LerpPosition()
    {
        //If the current instance is not the local player
        if(!isLocalPlayer)
        {
            //Lerp position of all other connected clients
            rigid.position = Vector3.Lerp(rigid.position, syncPosition, Time.deltaTime * lerpRate);
        }
    }
    void LerpRotation()
    {
        if(!isLocalPlayer)
        {
            rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }
    [Command] void CmdSendPositionToServer(Vector3 _position)
    {
        syncPosition = _position;
        Debug.Log("Position Command");
    }
    [Command] void CmdSendRotationToServer(Quaternion _rotation)
    {
        syncRotation = _rotation;

    }
    [ClientCallback] void TransmitPosition()
    {
        if(isLocalPlayer && Vector3.Distance(rigid.position,lastPosition) > positionThreshold)
        {
            CmdSendPositionToServer(rigid.position);
            lastPosition = rigid.position;
        }
    }
    [ClientCallback] void TransmitRotation()
    {
        if(isLocalPlayer && Quaternion.Angle(rigid.rotation,lastRotation) > rotationThreshold)
        {
            CmdSendRotationToServer(rigid.rotation);
            lastRotation = rigid.rotation;
        }
    }
    #endregion
}
