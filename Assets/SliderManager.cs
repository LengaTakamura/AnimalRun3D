using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    CameraSwitch cameraSwitch;
   
    float defaultValue;
    float clonevalue;
    // Start is called before the first frame update
    void Start()
    {
        cameraSwitch = GameObject.Find("CameraSystem").GetComponent<CameraSwitch>();
    }


    //private void Update()
    //{
    //    if (!cameraSwitch.mainActive)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        this.gameObject.SetActive(true);
    //    }



    //}

    // Update is called once per frame
}

 