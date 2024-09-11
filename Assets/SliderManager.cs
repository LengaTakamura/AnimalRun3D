using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    CameraSwitch cameraSwitch;
   
   
    // Start is called before the first frame update
    void Start()
    {
        cameraSwitch = GameObject.Find("CameraSystem").GetComponent<CameraSwitch>();
        this.gameObject.SetActive(true);
    }


   
  
}

