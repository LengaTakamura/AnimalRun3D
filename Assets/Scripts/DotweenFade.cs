using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DotweenFade : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;

    public float fadeTime = 3f;
    void Start()
    {
        image = GetComponent<Image>();
        
    }

    public void FadeOut()
    {

    
        DOTween.ToAlpha(

            () => image.color,
            color => image.color = color,
            1f,
            fadeTime
        );
    }
}
