using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {
    private int health = 15;
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
            Destroy(this);
        }
    }
}
