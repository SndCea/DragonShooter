using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;
    [SerializeField] 

    private int numShots;
    public int NumShots { get => numShots; set => numShots = value; }

    [Header("Weapon Sounds")]
    public AudioClip reloadClip;
    public AudioClip emptyGunClip;
    public LayerMask TargetLayer;
    AudioSource audioSource;
    [SerializeField] bool dontShoot;

    public int bulletDamage;
    public int rayDistance;

    void Start()
    {
        InicializeDelegates();
        audioSource = GetComponent<AudioSource>();
        GameCanvasManager.GameManagerInstance.SetCanvasShots(numShots, bullet.numMaxShots, bullet.bulletSprite);
        dontShoot = false;

        OriginalRayRange();
        OriginalBulletDamage();
    }

    private void OnEnable()
    {
        InicializeDelegates();
    }
    private void InicializeDelegates ()
    {
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.GameOverReleased += DontShoot;
        }
    }
    private void OnDisable()
    {
        GameOverManager.Instance.GameOverReleased -= DontShoot;
    }

    void Update()
    {

    }

    public void Shoot()
    {
        if (!dontShoot)
        {
            if (numShots < bullet.numMaxShots)
            {
                Ray ray = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(UnityEngine.Input.mousePosition);

                RaycastHit hit;
                numShots++;

                audioSource.PlayOneShot(bullet.shotClip);
                GameCanvasManager.GameManagerInstance.SetCanvasShots(numShots, bullet.numMaxShots, bullet.bulletSprite);

                //Collision detection
                if (Physics.Raycast(ray.origin, ray.direction, out hit, rayDistance, TargetLayer))
                {
                    if (hit.collider.gameObject.GetComponent<Target>())
                    {
                        hit.collider.gameObject.GetComponent<Target>().triggerExplodeAnimation();

                        if (hit.collider.gameObject.GetComponent<Target>().TargetItem.type == XEntity.InventoryItemSystem.ItemType.PowerUp) 
                        {
                            Debug.Log("Power Upp");
                            InventoryManager.InventoryManagerInstance.AddItem(hit.collider.gameObject.GetComponent<Target>().TargetItem);
                        } else if (hit.collider.gameObject.GetComponent<Target>().TargetItem.type == XEntity.InventoryItemSystem.ItemType.MeteorShower)
                        {
                            VictoryManager.VictoryManagerInstance.SpawnMeteorShower();
                        }

                        
                    } else if (hit.collider.gameObject.transform.parent.transform.tag == "Dragon")
                    {
                        hit.collider.gameObject.transform.parent.GetComponent<DragonData>().ApplyDamage(bulletDamage);
                    }
                }
            }
            else
            {
                audioSource.PlayOneShot(emptyGunClip);
            }
        }
    }
    public void Reload ()
    {
        if (!dontShoot && numShots == bullet.numMaxShots)
        {
            numShots = 0;
            GetComponent<AudioSource>().PlayOneShot(reloadClip);
            GameCanvasManager.GameManagerInstance.SetCanvasShots(numShots, bullet.numMaxShots, bullet.bulletSprite);
        }
    }

    public void StopShoot(bool stop)
    {
        if (stop)
        {
            DontShoot();
        } else
        {
            CanShoot();
        }
    }
    public void DontShoot()
    {
        dontShoot = true;
    }

    public void CanShoot()
    {
        dontShoot = false;
    }

    public void IncreaseBulletDamage(int cant)
    {
        bulletDamage *= cant;
    }

    public void OriginalBulletDamage ()
    {
        bulletDamage = bullet.damage;
    }

    public void IncreaseRayRange(int newRayDistance)
    {
        rayDistance = newRayDistance;
    }
    public void OriginalRayRange()
    {
        rayDistance = bullet.rayDistance;
    }
}
