using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingSaucer : MonoBehaviour
{
    public BlackBoard blackBoard = null;
    public List<GameObject> path = null;
    public HQ destination = null;
    public float speed = 5.0f, range = 20.0f, cooldown = 5.0f;
    public int health = 10, worth = 0, damage = 2;
    public GameObject particles, target, damageParticles;
    public bool Stunned = false;
    public float stunLength = 5.0f;

    private RaycastHit _rayHit;
    private int hoverNum;
    private List<Vector3> hoverPath = new List<Vector3>();
    private System.Random rnd = new System.Random();

    protected float _curCooldown, _curStunTime;
    void Start()
    {

    }

    void Update()
    {
        //logic below : saucer has a target shoot at the saucer, then if it has a path avaliable to it follow it, then add the HQ as a path and find a target

        if (!Stunned)
        {
            if (destination == null)
            {
                destination = (GameObject.FindGameObjectWithTag("HeadQuaters")).GetComponent<HQ>();
            }
            if (target == null)
            {
                FindTargets();
            }

            if (target != null)
            {
                SaucerShoot();
            }
            else if (path.Count != 0)
            {
                FollowPath();
            }
            else
            {
                path.Add(destination.gameObject);
            }
            //shooting cooldown
            _curCooldown -= Time.deltaTime;
        }

        if (_curStunTime <= 0.0f)
        {
            Stunned = false;
        }
        _curStunTime -= Time.deltaTime;
    }

    virtual public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health < 0)
        {
            blackBoard.resourceManager.ChangeMoney(worth);
            DestroyMe();
        }
    }
    virtual public void GetStunned()
    {
        Stunned = true;
        _curStunTime = stunLength;
    }
    virtual public void DestroyMe()
    {
        //spawn the saucer's death particle effect
        Instantiate(particles, transform.position, Quaternion.Euler(90, 0, 0));
        //remove itself from the blackboard
        BlackBoard.saucerTargets.Remove(gameObject);
        Destroy(gameObject);
    }

    virtual public void FindTargets()
    {
        //get target from the blackboard and set up a random hover pattern above the target
        blackBoard.RequestBuildingTargets(this);
        if (target == null)
        {
            return;
        }
        hoverPath.Clear();
        hoverPath.Add(target.transform.position + new Vector3(rnd.Next(-4, 1), 15.0f, rnd.Next(1, 4)));
        hoverPath.Add(target.transform.position + new Vector3(rnd.Next(-1, 4), 15.0f, rnd.Next(-1, 4)));
        hoverPath.Add(target.transform.position + new Vector3(rnd.Next(-2, 2), 15.0f, rnd.Next(-2, 4)));
    }

    protected void FollowPath()
    {
        //moving through the path in it's list
        transform.Translate((path[0].transform.position - gameObject.transform.position).normalized * speed * Time.deltaTime);
        //remove the path node from the list if the saucer is close enough to it
        if ((path[0].transform.position - gameObject.transform.position).magnitude < 2.0f)
        {
            path.RemoveAt(0);
        }
    }

    private void SaucerShoot()
    {
        //target out of it's range make it null
        if ((target.transform.position - transform.position).magnitude > range)
        {
            target = null;
            return;
        }
        //make the hovering above the enemy
        Hover();
        //raycast to get the enemy and make the enemy take damage
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out _rayHit, LayerMask.NameToLayer("Saucer")) && _curCooldown <= 0)
        {
            Building enemy = _rayHit.collider.gameObject.GetComponentInChildren<Building>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damage);
            _curCooldown = cooldown;
        }
    }
    //moving through the hover pattern for each enemy
    private void Hover()
    {
        if (hoverNum > 2)
        {
            hoverNum = 0;
        }

        transform.Translate((hoverPath[hoverNum] - gameObject.transform.position).normalized * speed * Time.deltaTime);
        //move on to the next hover node if the saucer is close enough to it
        if ((hoverPath[hoverNum] - gameObject.transform.position).magnitude < 1.0f)
        {
            hoverNum++;
        }
    }
}
