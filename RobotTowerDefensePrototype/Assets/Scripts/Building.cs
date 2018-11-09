using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public static int[] electricityCost = {-10, 20, 0};
    public static int[] cost = {50, 100, 20};
    public List<Building> connections;
    public List<Connector> connectors = new List<Connector>();
    public int buildingID = 0, health = 20, avaliablePower = 0;
    public bool powered = false;


    void Start () {
		
	}
	
	void Update () {
		
	}
    public virtual void ChangePowered(bool value)
    {
        powered = value;
        UpdatePower();
    }

    private void UpdatePower()
    {
        for (int i = 0; i < connections.Count - 1; i++)
        {
            connections[i].ChangePowered(true);
        }
    }
    public void CheckPower()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (connections[i].powered)
            {
                powered = true;
                UpdatePower();
                return;
            }
            
        }
    }
    public virtual void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health < 0)
        {
            DestroyMe();
        }
    }

    public virtual void DestroyMe()
    {
        BlackBoard.buildings.Remove(gameObject);
        for(int i = 0; i < connectors.Count; i++)
        {
            connectors[i].DestroyMe();
        }
        Destroy(gameObject);
    }

}
