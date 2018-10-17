using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {
    public int health = 10;
    public HQ destination;
	void Start () {
		
	}
	
	void Update () {
        
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
