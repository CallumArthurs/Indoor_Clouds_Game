using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour {
    public float Range;
    public int Damage;
    public GameObject[] Targets;

    void Start() {
        FindTargets();
    }

    void Update() {
        if (Targets.Length == 0)
        {
            FindTargets();
        }
        else
        {
            TurretActivate();
        }
    }

    void FindTargets()
    {
        return;
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

        transform.LookAt(Targets[0].transform);


    }
}
