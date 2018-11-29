﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] checkPoints;
    public int currentLoc = 0;
    private int totalChecks;
    public string Left;
    public string Right;
    public string LookUpCunt;
    public float speed;
    public Transform motherShip;
    private Quaternion mumsRotation;
    private Vector3 idkWhat;

    // Use this for initialization
    void Start()
    {
        transform.position = checkPoints[currentLoc].position;
        transform.rotation = checkPoints[currentLoc].rotation;
        totalChecks = checkPoints.Length - 1;
        Locupdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            currentLoc = currentLoc - 1;
        }
        if (Input.GetKey("d"))
        {
            currentLoc = currentLoc + 1;
        }
        Locupdate();


        transform.position = Vector3.MoveTowards(transform.position, checkPoints[currentLoc].position, speed * Time.deltaTime);
        if (Input.GetKey("w"))
        {
            idkWhat = motherShip.position - transform.position;
            mumsRotation = Quaternion.LookRotation(idkWhat);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, mumsRotation, speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, checkPoints[currentLoc].rotation, speed * Time.deltaTime);
        }
    }


    void Locupdate()
    {
        if (currentLoc < 0)
        {
            currentLoc = totalChecks;
        }
        else if (currentLoc > totalChecks)
        {
            currentLoc = 0;
        }
        // transform.position = checkPoints[currentLoc].position;
        // transform.rotation = checkPoints[currentLoc].rotation;
    }

}