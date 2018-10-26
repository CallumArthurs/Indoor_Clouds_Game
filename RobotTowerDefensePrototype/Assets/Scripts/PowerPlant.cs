using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : MonoBehaviour {
    public ResourceManager resourceManager = null;
    public List<Transmitter> connections = null;
    public int generatedPower = 10, pubCost = 50;
    public static int cost = 50;

    void Start () {

	}
	
	void Update () {
		
	}
}
