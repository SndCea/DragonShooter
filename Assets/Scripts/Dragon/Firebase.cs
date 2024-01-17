using DigitalRuby.PyroParticles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    [Tooltip("Optional audio source to play once when the script starts.")]
    public AudioSource AudioSource;

    [Tooltip("How long the script takes to fully start. This is used to fade in animations and sounds, etc.")]
    public float StartTime = 1.0f;

    [Tooltip("How long the script takes to fully stop. This is used to fade out animations and sounds, etc.")]
    public float StopTime = 3.0f;

    [Tooltip("How long the effect lasts. Once the duration ends, the script lives for StopTime and then the object is destroyed.")]
    public float Duration = 2.0f;

    [Tooltip("How much force to create at the center (explosion), 0 for none.")]
    public float ForceAmount;

    [Tooltip("The radius of the force, 0 for none.")]
    public float ForceRadius;

    [Tooltip("A hint to users of the script that your object is a projectile and is meant to be shot out from a person or trap, etc.")]
    public bool IsProjectile;

    [Tooltip("Particle systems that must be manually started and will not be played on start.")]
    public ParticleSystem[] ManualParticleSystems;

    public int damage;

    private float startTimeMultiplier;
    private float startTimeIncrement;

    private float stopTimeMultiplier;
    private float stopTimeIncrement;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioSource != null)
        {
            AudioSource.Play();
        }

        // precalculate so we can multiply instead of divide every frame
        stopTimeMultiplier = 1.0f / StopTime;
        startTimeMultiplier = 1.0f / StartTime;

        // start any particle system that is not in the list of manual start particle systems
        StartParticleSystems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void Awake()
    {
        Starting = true;
        int fireLayer = UnityEngine.LayerMask.NameToLayer("FireLayer");
        UnityEngine.Physics.IgnoreLayerCollision(fireLayer, fireLayer);
    }


    private void StartParticleSystems()
    {
        foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            if (ManualParticleSystems == null || ManualParticleSystems.Length == 0 ||
                System.Array.IndexOf(ManualParticleSystems, p) < 0)
            {
                if (p.main.startDelay.constant == 0.0f)
                {
                    // wait until next frame because the transform may change
                    var m = p.main;
                    var d = p.main.startDelay;
                    d.constant = 0.01f;
                    m.startDelay = d;
                }
                p.Play();
            }
        }
    }

    public virtual void Stop()
    {
        if (Stopping)
        {
            return;
        }
        Stopping = true;

        // cleanup particle systems
        foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }

        StartCoroutine(CleanupEverythingCoRoutine());
    }

    private IEnumerator CleanupEverythingCoRoutine()
    {
        // 2 extra seconds just to make sure animation and graphics have finished ending
        yield return new WaitForSeconds(StopTime + 2.0f);

        GameObject.Destroy(gameObject);
    }

    public bool Starting
    {
        get;
        private set;
    }

    public float StartPercent
    {
        get;
        private set;
    }

    public bool Stopping
    {
        get;
        private set;
    }

    public float StopPercent
    {
        get;
        private set;
    }
}
