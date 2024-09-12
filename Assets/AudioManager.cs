using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioListener main;
    [SerializeField] AudioListener sub;
    [SerializeField] CameraSwitch cameraSwitch;
   
    // Update is called once per frame
    void Update()
    {
        if (cameraSwitch.mainActive)
        {
            main.enabled = true;
            sub.enabled = false;
        }
        else 
        {
            main.enabled = false;
            sub.enabled = true;
        }
    }
}
