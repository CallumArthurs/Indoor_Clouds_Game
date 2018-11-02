using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public List<GameObject> path = null;
    public HQ destination = null;
    public float speed = 5.0f;
    public int health = 10, worth = 0;
    public GameObject particles;

	void Start () {
		
	}
	
	void Update () {
        if (path.Count != 0)
        {
            FollowPath();
        }
        else
        {
            if (destination != null)
            {
                path.Add(destination.gameObject);
            }
        }
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
}