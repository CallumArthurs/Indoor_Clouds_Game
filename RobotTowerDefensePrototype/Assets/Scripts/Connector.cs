using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    public Building firstConnection, secondConnection;
    public BuildingManager buildingManager;
    private LineRenderer renderer;
    //get the line renderer componenet from the empty game object
	void Start () {
        renderer = GetComponent<LineRenderer>();
	}
    //make the connections
    public void SetConnections(Building connectOne, Building connectTwo)
    {
        //assign the connector two connections
        firstConnection = connectOne;
        secondConnection = connectTwo;
        //add the connectors to the buildings themselves
        firstConnection.connectors.Add(this);
        secondConnection.connectors.Add(this);
        CheckPowerSources(firstConnection, secondConnection);
        //check if the buildings now have access to power
        firstConnection.CheckPower();
        secondConnection.CheckPower();
    }
    //change the colour of the renderer the start and end blends in the middle
    public void UpdateColour(Color Colorstart, Color Colorend)
    {
        renderer.startColor = Colorstart;
        renderer.endColor = Colorend;
    }
    //turn the line renderer on and off
    public void ActivateRenderer(bool set)
    {
        renderer.enabled = set;
        if (set)
        {
            RenderLines();
        }
    }
    //make the starting and end points of the line renderer
    public void RenderLines()
    {
        Vector3[] temp = { firstConnection.gameObject.transform.position, secondConnection.gameObject.transform.position };
        renderer.SetPositions(temp);
    }
    //remove the connector from the buildings connections and from the building manager then destroy the object
    public void DestroyMe()
    {
        firstConnection.connectors.Remove(this);
        secondConnection.connectors.Remove(this);
        buildingManager.connectors.Remove(this);
        Destroy(gameObject);
    }
    //checks the buildings for their powerSources
    public void CheckPowerSources(Building building1, Building building2)
    {
        if (building1.connections.Count > 0 || building2.connections.Count > 0)
        {
            for(int i = 0; i < building1.connections.Count; i++)
            {
                building1.connections[i].CheckPower();
            }
            for (int i = 0; i < building2.connections.Count; i++)
            {
                building2.connections[i].CheckPower();
            }

        }

        if (building1.powersources.Count > 0)
        {
            building2.powersources.AddRange(building1.powersources);
            building2.CheckPower();
        }
        else if (building2.powersources.Count > 0)
        {
            building1.powersources.AddRange(building2.powersources);
            building2.CheckPower();

        }
    }
}
