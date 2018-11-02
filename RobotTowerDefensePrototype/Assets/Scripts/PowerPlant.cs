using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building {
    public ResourceManager resourceManager = null;
    public int generatedPower = 10, pubCost = 50;

    void Start () {
        powered = true;
	}
	
	void Update () {
		
	}
}

