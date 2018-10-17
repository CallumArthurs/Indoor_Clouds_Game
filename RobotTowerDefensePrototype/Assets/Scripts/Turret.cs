﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public float range;
    public int damage = 2;
    public GameObject[] targets;
    public BlackBoard blackBoard;
    public float cooldown = 5.0f;

    private Ray ray;
    private RaycastHit rayHit;
    private float curCooldown;
    void Start() {
        blackBoard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
        FindTargets();
    }

    void Update() {
        if (targets.Length == 0)
        {
            FindTargets();
        }
        else
        {
            TurretActivate();
        }
        curCooldown -= Time.deltaTime;
    }

    void FindTargets()
    {
        blackBoard.RequestTargets(this);
    }
    
    void TurretActivate()
    {
        #region lerped lookat?
        //Vector3 TargetPos = Targets[0].transform.position;
        //float Angle = Vector3.Angle(transform.position, TargetPos);

        //Debug.Log(Angle.ToString());

        //if (transform.rotation.y == Angle)
        //{
        //    return;
        //}
        //transform.Rotate(transform.up, (Angle) * Time.deltaTime);
        #endregion
        if(targets.Length > 0)
        {
            //if (targets[0] = null)
            //{
            //    FindTargets();
            //    return;
            //}
            transform.LookAt(targets[0].transform);
            if (Physics.Raycast(transform.position, transform.forward,out rayHit) && curCooldown <= 0)
            {
                FlyingSaucer enemy = rayHit.collider.gameObject.GetComponent<FlyingSaucer>();
                enemy.TakeDamage(damage);
                curCooldown = cooldown;
            }
        }
    }
}
