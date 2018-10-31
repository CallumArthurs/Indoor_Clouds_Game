using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    public static List<GameObject> saucerTargets = new List<GameObject>();
    public static List<GameObject> buildings = new List<GameObject>();

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
        for (int i = 0; i < saucerTargets.Count; i++)
        {
            if (((saucerTargets[i].transform.position) - HQ.transform.position).magnitude < _closestEnemyDist)
            {
                _closestEnemy = saucerTargets[i];
                _closestEnemyDist = ((saucerTargets[i].transform.position) - HQ.transform.position).magnitude;
            }
        }

        obj.target = _closestEnemy;
    }
    public void AddTarget(GameObject obj)
    {
        saucerTargets.Add(obj);
    }
}
