using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    //electricity and cost go in this order (turret1,powerplant,connector,Charles)
    public static int[] electricityCost = {-10, 20, 0, 0};
    public static int[] cost = {50, 100, 20, 50};
    public List<Building> connections;
    public List<Connector> connectors = new List<Connector>();
    public List<PowerPlant> powersources;
    public int buildingID = 0, health = 20, avaliablePower = 0;
    public bool powered = false, powerSource = false;
    public GameObject damageParticles;

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
        if(damageParticles != null)
        {
            Instantiate(damageParticles, transform.position, transform.rotation);
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

    //if a collider enters this object if it's a flying saucer take damage otherwise return
    private void OnTriggerEnter(Collider other)
    {
        FlyingSaucer tempSaucer = other.gameObject.GetComponent<FlyingSaucer>();
        if (tempSaucer == null)
        {
            return;
        }
        TakeDamage(tempSaucer.damage);
        //destroy the saucer that hit the HQ
        tempSaucer.DestroyMe();
    }

}
