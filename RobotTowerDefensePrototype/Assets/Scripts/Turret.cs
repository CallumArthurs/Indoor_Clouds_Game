using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building {
    public GameObject target = null, nozzle;
    public BlackBoard blackBoard = null;
    public float range = 20.0f, cooldown = 5.0f, rotSpeed = 5.0f;
    public int damage = 2, turretID = 0, pubCost = 50;

    public static float modifier = 1;
    public static bool noElectricity = false;
    private RaycastHit _rayHit;
    private float _curCooldown;
    private void Awake()
    {
        cost[turretID] = pubCost;
    }
    void Start() {
        blackBoard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
        FindTargets();
    }

    void Update() {

        if (target == null)
        {
            FindTargets();
        }
        else if (powered)
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
        if ((target.transform.position - transform.position).magnitude > range)
        {
            target = null;
            return;
        }

        nozzle.transform.rotation = 
            Quaternion.Slerp(nozzle.transform.rotation, 
            Quaternion.LookRotation(target.transform.position - nozzle.transform.position), 
            rotSpeed * Time.deltaTime);

        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.forward, out _rayHit) && _curCooldown <= 0)
        {
            FlyingSaucer enemy = _rayHit.collider.gameObject.GetComponent<FlyingSaucer>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damage);
            _curCooldown = cooldown * modifier;
        }
    }
}
