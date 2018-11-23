using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamakazi_UFO : FlyingSaucer {

	void Start () {
		
	}
	
	void Update () {
        if (!Stunned)
        {
            if (target != null)
            {
                KamaKazi();
            }
            else if (path.Count != 0)
            {
                FollowPath();
            }
            else
            {
                path.Add(destination.gameObject);
                FindTargets();
            }
            //shooting cooldown
            _curCooldown -= Time.deltaTime;
        }

    }

    void KamaKazi()
    {
        transform.Translate((target.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
    }
}
