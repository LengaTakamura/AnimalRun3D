using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadBackTitle()
    {
        StartCoroutine(SceneSystem.ForFadeTime("Title"));
    }

    public void LoadBackToPlay()
    {
        StartCoroutine(SceneSystem.ForFadeTime("InGame"));
    }

  
}
