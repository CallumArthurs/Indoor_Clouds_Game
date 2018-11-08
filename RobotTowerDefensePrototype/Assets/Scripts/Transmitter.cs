using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : Building {
    public int ConnectedPower = 0;

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
            //connectTo.GetComponent<PowerPlant>().generatedPower;
        }
    }

    public override void ChangePowered(bool value)
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
}
