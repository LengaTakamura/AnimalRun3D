using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour
{
    public void LoadInGame()
    {
        StartCoroutine(ForFadeTime("InGame"));
    }
    public static IEnumerator ForFadeTime(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
