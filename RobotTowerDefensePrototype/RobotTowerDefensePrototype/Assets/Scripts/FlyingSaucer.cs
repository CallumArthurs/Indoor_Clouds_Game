using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public List<GameObject> path = null;
    public HQ destination = null;
    public float speed = 5.0f;
    public int health = 10;
    public int worth = 0;

	void Start () {
		
	}
	
	void Update () {
        if (path.Count != 0)
        {
            FollowPath();
        }
        else
        {
            path.Add(destination.gameObject);
        }
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if(health < 0)
        {
            blackBoard.blackBoardTargets.Remove(this.gameObject);
            blackBoard.resourceManager.ChangeMoney(worth);
            Destroy(gameObject);
        }
    }
    public void DestroyMe()
    {
        blackBoard.blackBoardTargets.Remove(this.gameObject);
        Destroy(this.gameObject);
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
