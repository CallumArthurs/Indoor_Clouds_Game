using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public GameObject target = null;
    public BlackBoard blackBoard = null;
    public float range = 20.0f;
    public float cooldown = 5.0f;
    public int damage = 2;
    public int electricityCost = 10;
    public static int[] cost = {50};
            
    private RaycastHit _rayHit;
    private float _curCooldown;
    void Start() {
        blackBoard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
        FindTargets();
    }

    void Update() {
        if (target == null)
        {
            FindTargets();
        }
        else
        {
            TurretActivate();
        }
        _curCooldown -= Time.deltaTime;
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
        if ((target.transform.position - transform.position).magnitude > range)
        {
            target = null;
            return;
        }
        transform.LookAt(target.transform);
        if (Physics.Raycast(transform.position, transform.forward,out _rayHit) && _curCooldown <= 0)
        {
            FlyingSaucer enemy = _rayHit.collider.gameObject.GetComponent<FlyingSaucer>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damage);
            _curCooldown = cooldown;
        }
    }
}
