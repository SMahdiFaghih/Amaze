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

    public void SetMoveParticleRotation(Vector3 velocityDirection)
    {
        velocityDirection = velocityDirection.normalized;
        switch (velocityDirection.z)
        {
            case -1:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }
        switch (velocityDirection.x)
        {
            case -1:
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case 1:
                transform.eulerAngles = new Vector3(0, -90, 0);
                break;
        }
    }

    public void PlayParticle()
    {
        ParticleSystemRenderer.material = PlayerMaterial;
        ParticleSystem.Play();
    }

    public void StopParticle()
    {
        ParticleSystem.Stop();
    }
}
