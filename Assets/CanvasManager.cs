using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    CanvasGroup a;
    public void UIFade()
    {
        a = GetComponent<CanvasGroup>();
        a.DOFade(0f, 2f);
    }
}
