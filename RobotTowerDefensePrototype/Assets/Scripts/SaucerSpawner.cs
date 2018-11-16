using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawner : MonoBehaviour {
    public BlackBoard blackBoard = null;
    public GameObject[] saucers = null;
    public List<Path> bBPath = null;
    public HQ headQuarters = null;
    public float timer = 5.0f;
    public bool active = false;

    private float _curTime;
	void Start () {
        _curTime = timer;
    }
	
	void Update () {
        _curTime -= Time.deltaTime;

        if (_curTime < 0.0f && active)
        {
            _curTime = timer;
            int RandomNumber = Random.Range(0, saucers.Length);
            Spawn(RandomNumber);
        }
	}

    public void Spawn(int saucerID)
    {
        GameObject curSaucer = Instantiate(saucers[saucerID], transform);
        FlyingSaucer tempSaucer = curSaucer.GetComponent<FlyingSaucer>();

        if (bBPath.Count > 0)
        {
            int RandomNumber = Random.Range(0, bBPath.Count);
            Path selectedPath = bBPath[RandomNumber];
            tempSaucer.path.AddRange(selectedPath.pathNodes);
        }

        if (saucerID == 1)
        {
            tempSaucer.GetComponentInChildren<SaucerSpawner>().headQuarters = headQuarters;
        }
        blackBoard.AddTarget(curSaucer);
        tempSaucer.destination = headQuarters;
        tempSaucer.blackBoard = blackBoard;
    }
}
