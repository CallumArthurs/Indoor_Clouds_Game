using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawner : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public GameObject[] saucers = null;
    public List<Path> bBPath = null;
    public HQ headQuarters = null;
    public float timer = 5.0f;

    private float _curTime;
	void Start () {
        _curTime = timer;
    }
	
	void Update () {
        _curTime -= Time.deltaTime;

        if (_curTime < 0.0f)
        {
            _curTime = timer;
            Spawn(0);
        }
	}

    public void Spawn(int saucerID)
    {
        int RandomNumber = Random.Range(0, bBPath.Count - 1);
        Path selectedPath = bBPath[RandomNumber];

        GameObject curSaucer = Instantiate(saucers[saucerID], this.transform);
        FlyingSaucer tempSaucer = curSaucer.GetComponent<FlyingSaucer>();

        blackBoard.AddTarget(curSaucer);
        tempSaucer.destination = headQuarters;
        tempSaucer.path.AddRange(selectedPath.pathNodes);
        tempSaucer.blackBoard = blackBoard;
        curSaucer = null;
        tempSaucer = null;
    }
}
