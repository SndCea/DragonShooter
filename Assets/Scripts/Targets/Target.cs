using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEntity.InventoryItemSystem;

public class Target : MonoBehaviour
{
    public Item TargetItem;
    Animator animController;
    AudioSource audioSource;
    public AudioClip explosionClip;
    public GameObject explosionParticleSystem;


    private void Awake()
    {
        animController = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
       
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
    public void Explode()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }
        Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }
}
