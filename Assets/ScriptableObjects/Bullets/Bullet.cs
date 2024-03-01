using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public BulletType type;
    public int damage;
    public int numMaxShots;
    public int numStartAmmo;
    public int rayDistance;

    //public GameObject explosion;
    public AudioClip shotClip;
    public Sprite bulletSprite;
    public Sprite ammoSprite;

}
