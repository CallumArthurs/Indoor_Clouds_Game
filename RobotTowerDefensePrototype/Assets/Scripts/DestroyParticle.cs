using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public List<ParticleSystem> p = new List<ParticleSystem>();

    private void Start()
    {
        p.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    void Update()
    {
        for (int i = 0; i < p.Count; i++)
        {
            if (!(p[i].IsAlive()))
            {
                ParticleSystem temp = p[i];
                p.Remove(p[i]);
                Destroy(temp.gameObject);

            }
        }
        if (p.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}

