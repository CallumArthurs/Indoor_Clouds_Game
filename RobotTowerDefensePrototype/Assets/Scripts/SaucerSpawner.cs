using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawner : MonoBehaviour {
    public BlackBoard blackBoard;
    public GameObject[] saucers;

    public float timer = 5.0f;
    private float curTime;
	void Start () {
        curTime = timer;
    }
	
	void Update () {
        curTime -= Time.deltaTime;

        if (curTime < 0.0f)
        {
            curTime = timer;
            Spawn(0);
        }
	}

    public void Spawn(int saucerID)
    {
        blackBoard.AddTarget(Instantiate(saucers[saucerID],this.transform));
    }
}
