using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    public List <GameObject> blackBoardTargets = new List<GameObject>();
    public GameObject HQ;
    public ResourceManager resourceManager;

    private GameObject closestEnemy;
    private float closestEnemyDist = 100;
    void Start() {
       
    }

    void Update() {

    }
    
    public void RequestTargets(Turret obj)
    {
        closestEnemyDist = obj.range;
        for (int i = 0; i < blackBoardTargets.Count; i++)
        {
            if (((blackBoardTargets[i].transform.position) - HQ.transform.position).magnitude < closestEnemyDist)
            {
                closestEnemy = blackBoardTargets[i];
                closestEnemyDist = ((blackBoardTargets[i].transform.position) - HQ.transform.position).magnitude;
            }
        }

        obj.target = closestEnemy;
    }
    public void AddTarget(GameObject obj)
    {
        blackBoardTargets.Add(obj);
    }
}
