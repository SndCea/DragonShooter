using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Target", menuName = "Target")]
public class ScriptableTargets : ScriptableObject
{
    public int points;
    public Material material;
    public Sprite mapIcon;
    public GameObject explosionParticleSystem;
    public AudioClip explosionClip;
    public string description;
}
