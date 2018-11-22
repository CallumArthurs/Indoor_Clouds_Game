using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{
    public ResourceManager resourceManager = null;
    public int availablePower = 10, pubCost = 50;
    public List<Building> connectedBuildings;
    //set the powerplant to being a powersource and being powered
    void Start()
    {
        powered = true;
        powerSource = true;
        powersources.Add(this);
    }

    void Update()
    {

    }

    public override void DestroyMe()
    {
        for (int i = 0; i < connectedBuildings.Count; i++)
        {
            connectedBuildings[i].powersources.Remove(this);
            connectedBuildings[i].CheckPower();
        }
        BlackBoard.buildings.Remove(gameObject);
        for (int i = 0; i < connectors.Count; i++)
        {
            connectors[i].DestroyMe();
        }
        Destroy(gameObject);
    }
}
