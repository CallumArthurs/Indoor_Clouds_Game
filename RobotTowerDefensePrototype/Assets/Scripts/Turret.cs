using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building {
    public GameObject target = null, nozzle, shootParticles;
    public BlackBoard blackBoard = null;
    public float range = 20.0f, cooldown = 5.0f, rotSpeed = 5.0f;
    public int damage = 2, turretID = 0, pubCost = 50;

    public float modifier = 1;
    public static bool noElectricity = false;
    private RaycastHit _rayHit;
    private float _curCooldown;
    private void Awake()
    {
        //set the static cost of the turret to be what was set in the inspector
        cost[turretID] = pubCost;
    }
    void Start() {
        //get access to the blackboard and request targets
        blackBoard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
        FindTargets();
    }

    void Update() {
        //request a target if it hasn't got one
        if (target == null)
        {
            FindTargets();
        }
        // if the turret is powered then active and shoot at their target
        else if (powered)
        {
            TurretActivate();
        }
        //shooting cooldown
        _curCooldown -= Time.deltaTime;
    }
    //request targets from the blackboard
    void FindTargets()
    {
        blackBoard.RequestTargets(this);
    }
    //turret shoots at it's target
    void TurretActivate()
    {
        //if the target exited it's range then don't make it the target anymore
        if ((target.transform.position - transform.position).magnitude > range)
        {
            target = null;
            return;
        }
        //lerp the nozzle of the turret to the enemy
        nozzle.transform.rotation = 
            Quaternion.Slerp(nozzle.transform.rotation, 
            Quaternion.LookRotation(target.transform.position - nozzle.transform.position), 
            rotSpeed * Time.deltaTime);
        //raycast hits it's target, and it's cooldown is at zero (mostlikely made like this so if the saucer out runs the gun it doesn't get hit anyway)
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.forward, out _rayHit) && _curCooldown <= 0)
        {
            //check if what the raycast hit was a flying saucer
            FlyingSaucer enemy = _rayHit.collider.gameObject.GetComponent<FlyingSaucer>();
            if (enemy == null)
            {
                return;
            }
            //make the shooting particles from the nozzle of the turret
            Instantiate(shootParticles, nozzle.transform);
            enemy.TakeDamage(damage);
            //cooldown reset and a modifier is potentially added
            _curCooldown = cooldown * modifier;
        }
    }
}
