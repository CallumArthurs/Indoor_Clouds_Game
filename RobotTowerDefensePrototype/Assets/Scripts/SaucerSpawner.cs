using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawner : MonoBehaviour {
    public BlackBoard blackBoard;
    public GameObject[] saucers;
	void Start () {

	}
	
	void Update () {
		
	}

    public void Spawn(int saucerID)
    {
        blackBoard.AddTarget(Instantiate(saucers[saucerID]));
    }
}
