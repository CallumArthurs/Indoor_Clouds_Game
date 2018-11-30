using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charles : Building
{
    public float range = 20.0f;
    public float cooldown;

    private Animator animator;
    private BlackBoard blackboard;
    private float _curCooldown;
    private enum animations { Idle, Attack, selfDestruct }
    void Start()
    {
        animator = GetComponent<Animator>();
        blackboard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
        _curCooldown = cooldown;
    }

    void Update()
    {
        // if the turret is powered then active and shoot at their target
        if (powered && _curCooldown <= 0.0f)
        {
            Shoot();
        }
        //shooting cooldown
        _curCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        PlayAnimation((int)animations.Attack);

        //temperary list for the sacuers in range for stun effect
        List<FlyingSaucer> tempSaucers = new List<FlyingSaucer>();
        for (int i = 0; i < BlackBoard.saucerTargets.Count; i++)
        {
            if ((BlackBoard.saucerTargets[i].transform.position - gameObject.transform.position).magnitude < range)
            {
                tempSaucers.Add(BlackBoard.saucerTargets[i].GetComponent<FlyingSaucer>());
            }
        }

        if (tempSaucers.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < tempSaucers.Count; i++)
        {
            tempSaucers[i].GetStunned();
        }
        _curCooldown = cooldown;
        tempSaucers.Clear();
    }
    void PlayAnimation(int aniID)
    {
        animator.SetInteger("ActID", aniID);
    }

    public override void DestroyMe()
    {
        PlayAnimation((int)animations.selfDestruct);
        BlackBoard.buildings.Remove(gameObject);
        powered = false;
        for (int i = 0; i < connectors.Count; i++)
        {
            connectors[i].DestroyMe();
        }
    }

}
