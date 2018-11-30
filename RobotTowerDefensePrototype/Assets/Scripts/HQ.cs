using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : Building
{
    public Text healthtxt = null;
    public SceneSwitcher SceneManager = null;
    public GameObject particles;
    public List<GameObject> HQs;

    private int _allHQHealth;
    private DifficultyManager _difficultyManager;
    //update the UI health
    void Start()
    {
        UpdateHealth();
        _difficultyManager = (GameObject.FindGameObjectWithTag("DifficultyManager")).GetComponent<DifficultyManager>();
    }
    //check if the player has run out of lives then run the lose screen
    void Update()
    {
        if (health <= 0)
        {
            _difficultyManager.HQs.Remove(gameObject);
            BlackBoard.buildings.Remove(gameObject);
            for (int i = 0; i < HQs.Count; i++)
            {
                HQs[i].GetComponent<HQ>().HQs.Remove(gameObject);
            }
            Destroy(gameObject);
        }
        UpdateHealth();
    }
    private void OnTriggerEnter(Collider other)
    {
        FlyingSaucer tempSaucer = other.gameObject.GetComponent<FlyingSaucer>();
        if (tempSaucer == null)
        {
            return;
        }
        TakeDamage(1);
        //destroy the saucer that hit the HQ
        tempSaucer.DestroyMe();
    }
    public override void TakeDamage(int Damage)
    {
        health -= Damage;
        if (damageParticles != null)
        {
            Instantiate(damageParticles, transform.position, transform.rotation);
        }
        //update health on screen
        UpdateHealth();
    }
    void UpdateHealth()
    {
        _allHQHealth = health;
        for (int i = 0; i < HQs.Count; i++)
        {
            _allHQHealth += HQs[i].GetComponent<HQ>().health;
        }
        //update the UI with the current health value
        healthtxt.text = "Health: " + _allHQHealth.ToString();
    }
}
