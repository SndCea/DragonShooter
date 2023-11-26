using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] GameObject CanvasPointer;
    [SerializeField] GameObject CanvasGame;
    FirstPersonController FPController;
    Weapon weapon;
    
    void Start()
    {
        FPController = FindObjectOfType<FirstPersonController>();
        weapon = FindObjectOfType<Weapon>();

        if (GameCanvasManager.GameManagerInstance != null)
        {
            GameCanvasManager.GameManagerInstance.VisibleCanvas(false);
        }
        if (FPController != null)
        {
            FPController.SetStop(true);
        }
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (GameCanvasManager.GameManagerInstance != null)
        {
            GameCanvasManager.GameManagerInstance.VisibleCanvas(false);
        }
        if (FPController != null)
        {
            FPController.SetStop(true);
        }
    }
    private void OnDisable()
    {
        GameCanvasManager.GameManagerInstance.VisibleCanvas(true);
        FPController.SetStop(false);
    }
}
