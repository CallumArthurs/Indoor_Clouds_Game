using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : Building {
    public PowerPlant ConnectedPlant;
	void Start () {
		
	}
	
	void Update () {

	}
    public override void DestroyMe()
    {
        for (int i = 0; i < connections.Count - 1; i++)
        {
            connections[i].ChangePowered(false);
        }
        BlackBoard.buildings.Remove(gameObject);
        Destroy(gameObject);
    }

    public void Connection(Building connectTo)
    {
        connections.Add(connectTo);
        connectTo.connections.Add(this);
        if (powered)
        {
            connectTo.powered = true;
        }
        else if (connectTo.powered)
        {
            powered = true;
            if (connectTo.GetComponent<PowerPlant>() != null)
            {
                ConnectedPlant = connectTo.GetComponent<PowerPlant>();
                avaliablePower = ConnectedPlant.availablePower;
            }
        }
    }

    public override void ChangePowered(bool value)
    {
        powered = value;
        for (int i = 0; i < connections.Count - 1; i++)
        {
            if (!connections[i].powered)
            {
                connections[i].ChangePowered(true);
            }
        }
    }    
}
