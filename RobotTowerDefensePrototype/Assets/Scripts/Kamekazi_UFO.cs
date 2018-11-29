using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamekazi_UFO : FlyingSaucer {

    void Start()
    {

    }

    void Update()
    {
        if (!Stunned)
        {
            if (target != null)
            {
                KameKazi();
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

    void KameKazi()
    {
        transform.Translate((target.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
    }
}
