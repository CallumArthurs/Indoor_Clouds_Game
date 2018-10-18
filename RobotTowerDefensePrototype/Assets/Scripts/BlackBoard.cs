using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    public List <GameObject> blackBoardTargets = new List<GameObject>();
    void Start() {
       
    }

    void Update() {

    }
    
    public void RequestTargets(Turret obj)
    {
        obj.targets = blackBoardTargets.ToArray();
    }
    public void AddTarget(GameObject obj)
    {
        blackBoardTargets.Add(obj);
    }
}
