using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] CameraSwitch cameraSwitch;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && cameraSwitch.mainActive)
        {
            ResetPlayerRotating();
        }
    }


    public void ResetPlayerRotating()
    {
       transform.rotation = Quaternion.identity;
        mainCam.transform.rotation = Quaternion.identity;
    }
}
