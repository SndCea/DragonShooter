using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMainUI : MonoBehaviour
{
    public GameObject panelSelectedData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SetCursorState(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().SetStop(true);
    }

    private void OnDisable()
    {
        SetCursorState(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().SetStop(false);
        panelSelectedData.SetActive(false);
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
