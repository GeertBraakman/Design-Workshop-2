﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementSimpleV2 : MonoBehaviour
{
    //public GameObject Bullet;
    public Text hitPlaceholder;
    public AudioSource audioData;
    //private Transform Turret;
    //private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;
    //private float turretRotSpeed = 10.0f;
    private float maxForwardSpeed = 300.0f;
    private float maxBackwardSpeed = -300.0f;

    //Bullet shooting rate
    protected float shootRate;
    protected float elapsedTime;

    void Start()
    {
        //Tank Settings
        rotSpeed = 150.0f;

        //Get the turret of the tank
        //Turret = gameObject.transform.GetChild(0).transform;
        //bulletSpawnPoint = Turret.GetChild(0).transform;
    }

    void OnEndGame()
    {
        // Don't allow any more control changes when the game ends
        this.enabled = false;
    }
    public void Explode()
    {
        hitPlaceholder.text = "You don't deserve this hell!";
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        float rndX = Random.Range(10.0f, 30.0f);
        float rndZ = Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 5; i++)
        {
            //GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().AddForce(transform.forward * 1.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 5, 0);
        //hitPlaceholder.text = "";
        //Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        UpdateControl();
        //UpdateWeapon();
    }

    void UpdateControl()
    {
        ////AIMING WITH THE MOUSE
        //// Generate a plane that intersects the transform's position with an upwards normal.
        //Plane playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0, 0));

        //// Generate a ray from the cursor position
        //Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        //// Determine the point where the cursor ray intersects the plane.
        //float HitDist = 0;

        //// If the ray is parallel to the plane, Raycast will return false.
        //if (playerPlane.Raycast(RayCast, out HitDist))
        //{
        //    // Get the point along the ray that hits the calculated distance.
        //    Vector3 RayHitPoint = RayCast.GetPoint(HitDist);

        //    Quaternion targetRotation = Quaternion.LookRotation(RayHitPoint - transform.position);
        //    Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, targetRotation, Time.deltaTime * turretRotSpeed);
        //}

        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed = maxForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetSpeed = maxBackwardSpeed;
        }
        else
        {
            targetSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotSpeed * Time.deltaTime, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0.0f);
        }

        //Determine current speed
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, 7.0f * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    //void UpdateWeapon()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (elapsedTime >= shootRate)
    //        {
    //            //Reset the time
    //            elapsedTime = 0.0f;

    //            //Also Instantiate over the PhotonNetwork
    //            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    //        }
    //    }
    //}
}
