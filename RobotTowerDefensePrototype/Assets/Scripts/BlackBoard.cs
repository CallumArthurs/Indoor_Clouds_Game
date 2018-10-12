using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    public GameObject[] blackBoardTargets;
    void Start() {

    }

    void Update() {

    }
    
    public void RequestTargets(Turret obj)
    {
        obj.Targets = blackBoardTargets;
    }
}
