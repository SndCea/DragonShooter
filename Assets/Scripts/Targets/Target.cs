using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEntity.InventoryItemSystem;

public enum TargetType { blue, green, red }
public class Target : MonoBehaviour
{
    public TargetType TargetType;
    public ScriptableTargets[] scriptableTargets;
    public Item TargetItem;
    [SerializeField] Image minimapIcon;
    Animator animController;
    AudioSource audioSource;
    MeshRenderer meshRenderer;
    int points;
    AudioClip explosionClip;
    GameObject explosionParticleSystem;


    private void Awake()
    {
        animController = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        int index = 0;
        if (TargetType == TargetType.blue)
        {
            index = 0;
        }
        if (TargetType == TargetType.green)
        {
            index = 1;
        }
        if (TargetType == TargetType.red)
        {
            index = 2;
        }
        meshRenderer.material = scriptableTargets[index].material;
        this.points = scriptableTargets[index].points;
        minimapIcon.sprite = scriptableTargets[index].mapIcon;
        explosionClip = scriptableTargets[index].explosionClip;
        explosionParticleSystem = scriptableTargets[index].explosionParticleSystem;
    }

    // Update is called once per frame
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

    public int GetPoints ()
    {
        return points;
    }
}
