using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour {
    public int health = 50;
    public Text healthtxt = null;
    public SceneSwitcher SceneManager = null;
    public GameObject particles;

	void Start () {
        UpdateHealth();
    }
	
	void Update () {
		if (health <= 0)
        {
            SceneManager.LostTheGame();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        FlyingSaucer tempSaucer = other.gameObject.GetComponent<FlyingSaucer>();
        if (tempSaucer == null)
        {
            return;
        }

        health--;
        UpdateHealth();
        tempSaucer.DestroyMe();
    }
    
    void UpdateHealth()
    {
        healthtxt.text = "Health: " + health.ToString();
    }
}
