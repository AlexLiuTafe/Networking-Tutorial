using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    //Public
    public float fireInterval;
    public float fireRate = 1f;
    public float range = 100f;
    public LayerMask mask;
    //Private
    private Transform shootPos;
    private float fireFactor = 0f;
    private GameObject mainCamera;//We use GameObject because we attach them in the Parent
    // Start is called before the first frame update
    void Start()
    {

        Camera mainCamera = GetComponentInChildren<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }
    [Command]
    void Cmd_PlayerShot()
    {
        Debug.Log("Player" + "id" + "has been shot");
    }
    void HandleInput()
    {
        fireFactor += Time.deltaTime;
        fireInterval = 1 / fireRate;
        if (fireFactor >= fireInterval)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();

            }
        }


    }
    void Shoot()
    {

        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, range))
        {
            //If Hit the player ID
            if ()
            {
                Cmd_PlayerShot();
            }
        }
    }
}
