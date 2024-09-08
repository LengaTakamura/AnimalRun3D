using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenFade : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup canvasGroup;
    public float fadeTime;
    void Start()
    {
        
    }

   public  void FadeOut()
    {
        canvasGroup = GameObject.Find("FadePanel").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeTime);
    }
}
