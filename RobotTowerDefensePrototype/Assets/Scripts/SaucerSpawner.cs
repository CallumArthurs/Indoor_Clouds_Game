using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawner : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public GameObject[] saucers = null;
    public float timer = 5.0f;
    public List<GameObject> bBPath = null;
    public HQ headQuarters;

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
        GameObject curSaucer = Instantiate(saucers[saucerID], this.transform);
        FlyingSaucer tempSaucer = curSaucer.GetComponent<FlyingSaucer>();

        blackBoard.AddTarget(curSaucer);
        tempSaucer.destination = headQuarters;
        tempSaucer.path.AddRange(bBPath);
        tempSaucer.blackBoard = blackBoard;
        curSaucer = null;
        tempSaucer = null;
    }
}
