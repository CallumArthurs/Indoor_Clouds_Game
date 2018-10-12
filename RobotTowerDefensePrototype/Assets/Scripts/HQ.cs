using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour {
    public int health;
    public Text healthtxt;

	void Start () {
        UpdateHealth();
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        health--;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthtxt.text = "Health: " + health.ToString();
    }
}
