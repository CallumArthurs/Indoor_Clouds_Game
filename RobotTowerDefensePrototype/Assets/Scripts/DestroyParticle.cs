using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticle : MonoBehaviour {
    private ParticleSystem p = null;

    private void Start()
    {
        p = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
		if (!(p.IsAlive()))
        {
            Destroy(gameObject);
        }
	}
}
