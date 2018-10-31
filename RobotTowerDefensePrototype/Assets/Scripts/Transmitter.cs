using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {
    public List<Building> connections;
    public int maxConnections = 2;

	void Start () {
		
	}
	
	void Update () {
		if (connections.Count < maxConnections)
        {
            SearchForConnections();
        }
	}

    void SearchForConnections()
    {

    }

    void DestroyMe()
    {
        for (int i = 0; i < maxConnections; i++)
        {
            connections[i].ChangePowered(true);
        }
    }
}
