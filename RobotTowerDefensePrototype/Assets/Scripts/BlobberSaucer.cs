using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlobberSaucer : FlyingSaucer
{

    private GameObject[] PowerPlants;
    private System.Random rnd = new System.Random();
    void Start()
    {

    }

    void Update()
    {
        if (target == null)
        {
            FindTargets();
        }
        else
        {
            Hover();
        }

    }

    public override void FindTargets()
    {
        PowerPlants = GameObject.FindGameObjectsWithTag("PowerPlant");
        if (PowerPlants.Length > 0)
        {
            target = PowerPlants[rnd.Next(0, PowerPlants.Length)];
        }
    }

    private void Hover()
    {
        transform.Translate((target.transform.position - gameObject.transform.position + new Vector3(0, 15.0f)).normalized * speed * Time.deltaTime);

        if ((target.transform.position - gameObject.transform.position).magnitude < 5.0f)
        {
            ChangeSpawner(true);
        }
    }

    private void ChangeSpawner(bool value)
    {
        SaucerSpawner[] temp = GetComponentsInChildren<SaucerSpawner>();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].active = value;
        }
    }
}
