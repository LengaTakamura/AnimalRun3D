using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTitle : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void LoadBackTitle()
    {
        StartCoroutine(SceneSystem.ForFadeTime("Title"));
    }

    public void LoadBackToPlay()
    {
        StartCoroutine(SceneSystem.ForFadeTime("InGame"));
    }
}
