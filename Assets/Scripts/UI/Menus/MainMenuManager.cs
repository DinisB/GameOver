using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class MainMenuManager : MonoBehaviour
{
    public CinemachineCamera currentCamera;


    public void Start()
    {
        currentCamera.Priority++;
    }

  
    public void UpdateCamera(CinemachineCamera target)
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }
}
