using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    public Building firstConnection, secondConnection;
    public BuildingManager buildingManager;
    private LineRenderer renderer;
	void Start () {
        renderer = GetComponent<LineRenderer>();
	}

    public void SetConnections(Building connectOne, Building connectTwo)
    {
        firstConnection = connectOne;
        secondConnection = connectTwo;

        firstConnection.connectors.Add(this);
        secondConnection.connectors.Add(this);

        firstConnection.CheckPower();
        secondConnection.CheckPower();
    }
    public void UpdateColour(Color Colorstart, Color Colorend)
    {
        renderer.startColor = Colorstart;
        renderer.endColor = Colorend;
    }
    public void ActivateRenderer(bool set)
    {
        renderer.enabled = set;
        if (set)
        {
            RenderLines();
        }
    }
    public void RenderLines()
    {
        Vector3[] temp = { firstConnection.gameObject.transform.position, secondConnection.gameObject.transform.position };
        renderer.SetPositions(temp);
    }
    public void DestroyMe()
    {
        firstConnection.connectors.Remove(this);
        secondConnection.connectors.Remove(this);
        buildingManager.connectors.Remove(this);
        Destroy(gameObject);
    }
}
