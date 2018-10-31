using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building {
    public ResourceManager resourceManager = null;
    public List<Transmitter> connections = null;
    public int generatedPower = 10, pubCost = 50;

    void Start () {

	}
	
	void Update () {
		
	}
}
