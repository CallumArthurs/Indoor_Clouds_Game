using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : Building
{

    void Start()
    {

    }

    void Update()
    {

    }
    //destroy the transmitter and make all connections check if they still are conected to a powersource
    public override void DestroyMe()
    {
        //remove this building from the blackboard
        BlackBoard.buildings.Remove(gameObject);
        for (int i = 0; i < connectors.Count;)
        {
            connectors[i].DestroyMe();
        }
        for (int i = 0; i < connections.Count - 1; i++)
        {
            connections[i].ChangePowered(false);
        }
        Destroy(gameObject);
    }

    public void Connection(Building connectTo)
    {
        //add connection to each of the building objects and make them check if they are connected to a powersource
        connections.Add(connectTo);
        connectTo.connections.Add(this);
        if (powered)
        {
            connectTo.powered = true;
        }
        else if (connectTo.powered)
        {
            powered = true;
            if (connectTo.powerSource)
            {
                avaliablePower = connectTo.GetComponent<PowerPlant>().availablePower;
            }
        }
    }

    public override void ChangePowered(bool value)
    {
        //change it's powered bool and make all buildings around it check too
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
