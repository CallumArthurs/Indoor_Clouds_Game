using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour {
    public int health = 50;
    public Text healthtxt = null;
    public SceneSwitcher SceneManager = null;
    public GameObject particles;
    //update the UI health
    void Start()
    {
        UpdateHealth();
    }
    //check if the player has run out of lives then run the lose screen
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LostTheGame();
        }
    }
    //if a collider enters this object if it's a flying saucer take damage otherwise return
    private void OnTriggerEnter(Collider other)
    {
        FlyingSaucer tempSaucer = other.gameObject.GetComponent<FlyingSaucer>();
        if (tempSaucer == null)
        {
            return;
        }

        health--;
        //update health on screen
        UpdateHealth();
        //destroy the saucer that hit the HQ
        tempSaucer.DestroyMe();
    }

    void UpdateHealth()
    {
        //update the UI with the current health value
        healthtxt.text = "Health: " + health.ToString();
    }
}
