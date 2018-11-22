using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public static int[] electricityCost = {-10, 20, 0, 0};
    public static int[] cost = {50, 100, 20, 50};
    public List<Building> connections;
    public List<Connector> connectors = new List<Connector>();
    public List<PowerPlant> powersources;
    public int buildingID = 0, health = 20, avaliablePower = 0;
    public bool powered = false, powerSource = false;


    void Start () {
		
	}
	
	void Update () {
		
	}

    public void CheckPower()
    {
        if (powersources.Count > 0)
        {
            powered = true;
            powersources[0].connectedBuildings.Add(this);
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].powersources.Count == 0)
                {
                    connections[i].powersources.AddRange(powersources[0].powersources);
                    connections[i].CheckPower();
                }
            }
        }
        else
        {
            powered = false;
        }
    }

    public virtual void ChangePowered(bool value)
    {
        powered = value;
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
