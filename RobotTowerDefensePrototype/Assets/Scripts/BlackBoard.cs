using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public static List<GameObject> saucerTargets = new List<GameObject>();
    public static List<GameObject> buildings = new List<GameObject>();
    public GameObject[] HQs = null;
    public ResourceManager resourceManager = null;

    private GameObject _closestEnemy;
    private float _closestEnemyDist = 100;

    void Start()
    {
        HQs = GameObject.FindGameObjectsWithTag("HeadQuaters");
        buildings.Clear();
        saucerTargets.Clear();
        buildings.AddRange(HQs);
    }
    void Update()
    {

    }

    //enemies in the static list are cycled through and the one closest to the current turret object is set as the turret's target
    public void RequestTargets(Turret obj)
    {
        if (saucerTargets.Count == 0)
        {
            return;
        }
        //set the _closestenemyDist to being the turret's range as to not give it a target out of it's range
        _closestEnemyDist = obj.range;
        for (int i = 0; i < saucerTargets.Count; i++)
        {
            if (((saucerTargets[i].transform.position) - obj.transform.position).magnitude < _closestEnemyDist)
            {
                if (saucerTargets[i] == null)
                {
                    return;
                }
                _closestEnemy = saucerTargets[i];
                _closestEnemyDist = ((saucerTargets[i].transform.position) - obj.transform.position).magnitude;
            }
        }
        obj.target = _closestEnemy;
        _closestEnemy = null;
    }
    //buildings in the static list are cycled through and the one closest to the current saucer object is set as the saucer's target
    public void RequestBuildingTargets(FlyingSaucer obj)
    {
        if (buildings.Count == 0)
        {
            return;
        }
        //set the _closestenemyDist to being the saucer's range as to not give it a target out of it's range
        _closestEnemyDist = obj.range;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (((buildings[i].transform.position) - obj.transform.position).magnitude < _closestEnemyDist)
            {
                _closestEnemy = buildings[i];
                _closestEnemyDist = ((buildings[i].transform.position) - obj.transform.position).magnitude;
            }
        }
        obj.target = _closestEnemy;
        _closestEnemy = null;
    }
    //add a saucer to the saucer list (outdated since it's now a static list)
    public void AddTarget(GameObject obj)
    {
        saucerTargets.Add(obj);
    }
}
