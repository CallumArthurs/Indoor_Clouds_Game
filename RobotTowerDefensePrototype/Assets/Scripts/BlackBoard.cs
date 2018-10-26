using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    public List<GameObject> blackBoardTargets;
    public GameObject HQ = null;
    public ResourceManager resourceManager = null;

    private GameObject _closestEnemy;
    private float _closestEnemyDist = 100;

    void Start() {
       
    }
    void Update() {

    }
    
    public void RequestTargets(Turret obj)
    {
        _closestEnemyDist = obj.range;
        for (int i = 0; i < blackBoardTargets.Count; i++)
        {
            if (((blackBoardTargets[i].transform.position) - HQ.transform.position).magnitude < _closestEnemyDist)
            {
                _closestEnemy = blackBoardTargets[i];
                _closestEnemyDist = ((blackBoardTargets[i].transform.position) - HQ.transform.position).magnitude;
            }
        }

        obj.target = _closestEnemy;
    }
    public void AddTarget(GameObject obj)
    {
        blackBoardTargets.Add(obj);
    }
}
