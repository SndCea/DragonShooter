using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticlePointer : MonoBehaviour
{
    public static ReticlePointer ReticlePointerInstance { get; private set; }

    private RectTransform reticle;

    private float restingSize;
    private float maxSize;
    private float speed;
    private float currentSize;
    public LayerMask TargetLayer;
    public int rayDistance;
    private void Awake()
    {
        if (ReticlePointerInstance != null && ReticlePointerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            ReticlePointerInstance = this;
        }
    }
    void Start()
    {
        reticle = GetComponent<RectTransform>();
        restingSize = 75;
        maxSize = 150;
        speed = 5;

        UpdateRay();
    }

    void Update()
    {
        if (seeingTarget)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }
        reticle.sizeDelta = new Vector2 (currentSize, currentSize);
    }

    bool seeingTarget
    {
        get
        {
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (FindObjectsOfType<Weapon>() != null && FindObjectsOfType<Weapon>().Length < 2)
            {
                rayDistance = FindObjectOfType<Weapon>().rayDistance;
            }

            if (Physics.Raycast(ray.origin, ray.direction, out hit, rayDistance, TargetLayer))
            {
                return true;

            } else
            {
                return false;
            }
        }
    }

    public void UpdateRay()
    {
        rayDistance = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>().rayDistance;
    }
}
