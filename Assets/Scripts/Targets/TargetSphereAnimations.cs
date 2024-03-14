using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphereAnimations : MonoBehaviour
{
    Animator animController;
    public GameObject explosionParticleSystem;
    public AudioClip explosionClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    { 
        
    }
    public void triggerExplodeAnimation()
    {
        if (animController != null)
        {
            animController.SetTrigger("explode");
        }
    }
    public void Explode ()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }
        Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }

    
}
