using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    private int zoomSensitivity = 10;
    [SerializeField] int maxZoomDistance = 50;
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        
    }

    public void Zoom()
    {
        if (vCam != null)
        {
            CinemachineComponentBase componentBase = vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is Cinemachine3rdPersonFollow)
            {
                if ((componentBase as Cinemachine3rdPersonFollow).CameraDistance > -maxZoomDistance)
                {
                    (componentBase as Cinemachine3rdPersonFollow).CameraDistance -= zoomSensitivity * Time.deltaTime;
                }
            }
        }
    }

    public void UnZoom()
    {
        if (vCam != null)
        {
            CinemachineComponentBase componentBase = vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is Cinemachine3rdPersonFollow)
            {
                if ((componentBase as Cinemachine3rdPersonFollow).CameraDistance < 0)
                {
                    (componentBase as Cinemachine3rdPersonFollow).CameraDistance += zoomSensitivity * Time.deltaTime;
                }
                
            }
        }
    }

}
