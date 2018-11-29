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
        _curTime = 5.0f;
    }
	
	void Update () {
        if (active)
        {
            _curTime -= Time.deltaTime;

            if (_curTime < 0.0f)
            {
                //spawn a random saucer if the cooldown is zero
                _curTime = timer - DifficultyManager.DifficultyScore;
                if (_curTime < 0)
                {
                    _curTime = 1;
                }
                int RandomNumber = Random.Range(0, saucers.Length);
                Spawn(RandomNumber);
            }
        }
    }

    public void Spawn(int saucerID)
    {
        //spawn the saucer in the world at the position of the spawner
        GameObject curSaucer = Instantiate(saucers[saucerID], gameObject.transform.position, gameObject.transform.rotation);
        FlyingSaucer tempSaucer = curSaucer.GetComponent<FlyingSaucer>();
        //if the spawner has paths to give to the saucer it will do so
        if (bBPath.Count > 0)
        {
            //select a random path from the list of paths
            int RandomNumber = Random.Range(0, bBPath.Count);
            Path selectedPath = bBPath[RandomNumber];
            tempSaucer.path.AddRange(selectedPath.pathNodes);
        }

        if (saucerID == 1)
        {
            tempSaucer.GetComponentInChildren<SaucerSpawner>().headQuarters = headQuarters;
            tempSaucer.GetComponentInChildren<SaucerSpawner>().blackBoard = blackBoard;
        }
        blackBoard.AddTarget(curSaucer);
        tempSaucer.destination = headQuarters;
        tempSaucer.blackBoard = blackBoard;

        tempSaucer = null;
        curSaucer = null;
    }
}
