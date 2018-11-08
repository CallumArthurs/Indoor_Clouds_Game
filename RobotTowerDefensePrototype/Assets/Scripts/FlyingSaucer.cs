using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public List<GameObject> path = null;
    public HQ destination = null;
    public float speed = 5.0f, range = 20.0f, cooldown = 5.0f;
    public int health = 10, worth = 0, damage = 2;
    public GameObject particles, target;

    private RaycastHit _rayHit;
    private float _curCooldown;
    void Start () {
		
	}
	
	void Update () {
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
            FindTargets();
        }
        _curCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if(health < 0)
        {
            blackBoard.resourceManager.ChangeMoney(worth);
            DestroyMe();
        }
    }

    public void DestroyMe()
    {
        Instantiate(particles, transform.position, transform.rotation);
        BlackBoard.saucerTargets.Remove(gameObject);
        Destroy(gameObject);

    }

    public void FollowPath()
    {
        transform.Translate((path[0].transform.position - gameObject.transform.position) * speed * Time.deltaTime);

        if ((path[0].transform.position - gameObject.transform.position).magnitude < 2.0f)
        {
            path.RemoveAt(0);
        }
    }

    private void FindTargets()
    {
        blackBoard.RequestBuildingTargets(this);
    }

    private void SaucerShoot()
    {
        if ((target.transform.position - transform.position).magnitude > range)
        {
            target = null;
            return;
        }

        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out _rayHit) && _curCooldown <= 0)
        {
            Building enemy = _rayHit.collider.gameObject.GetComponent<Building>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damage);
            _curCooldown = cooldown;
        }
    }
}
