using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour {
    public int health;
    public Text healthtxt;
    public SceneSwitcher SceneManager;

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
        health--;
        UpdateHealth();
        FlyingSaucer tempSaucer = other.gameObject.GetComponent<FlyingSaucer>();
        tempSaucer.DestroyMe();
    }

    void UpdateHealth()
    {
        healthtxt.text = "Health: " + health.ToString();
    }
}
