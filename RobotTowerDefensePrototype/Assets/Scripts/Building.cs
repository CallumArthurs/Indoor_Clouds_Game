using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public static int[] electricityCost = {-10, 20, 0};
    public static int[] cost = {50, 100, 20};
    public List<Building> connections;
    public int buildingID = 0;
    public bool powered = false;
    void Start () {
		
	}
	
	void Update () {
		
	}
    public virtual void ChangePowered(bool value)
    {
        powered = value;
    }

}
