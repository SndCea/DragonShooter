using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStateUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnEnable()
    {
        SetCursorState(false);
    }

    private void OnDisable()
    {
        SetCursorState(true);
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
