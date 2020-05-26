using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    ParticleSystemRenderer ParticleSystemRenderer;
    Material PlayerMaterial;

    void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystemRenderer = ParticleSystem.GetComponent<ParticleSystemRenderer>();
        PlayerMaterial = GetComponentInParent<MeshRenderer>().material;
    }

    public void PlayParticle()
    {
        ParticleSystemRenderer.material = PlayerMaterial;
        ParticleSystem.Play();
    }
}
