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
    public Camera camera;//We use GameObject because we attach them in the Parent
    // Start is called before the first frame update
    void Start()
    {

		camera = GetComponentInChildren<Camera>(); //Camera in attached in player;

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

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,range))
        {
            //If Hit the player ID
            if (hit.collider.CompareTag("Player"))
            {
                Cmd_PlayerShot();
            }
        }
    }
}
