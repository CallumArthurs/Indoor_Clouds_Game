using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlobberSaucer : FlyingSaucer {
    private GameObject[] PowerPlants;
    private System.Random rnd = new System.Random();
    private Animator animator;

	void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	void Update () {
        if (!Stunned)
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

        if (_curStunTime <= 0.0f)
        {
            Stunned = false;
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
        if ((target.transform.position - gameObject.transform.position + new Vector3(0,15.0f)).magnitude <= 0.5f)
        {
            ChangeSpawner(true);
            animator.SetInteger("ActID", 1);
            return;
        }

        transform.Translate((target.transform.position - gameObject.transform.position + new Vector3(0, 15.0f)).normalized * speed * Time.deltaTime);
    }

    private void ChangeSpawner(bool value)
    {
        SaucerSpawner[] temp = GetComponentsInChildren<SaucerSpawner>();
        for(int i = 0; i < temp.Length; i++)
        {
            temp[i].active = value;
        }
    }
}
