using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DotweenUI : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();

        if(ScoreManager.finalScore <= 100)
        {
            text.DOText("S", 4, scrambleMode: ScrambleMode.Uppercase).SetEase(Ease.Linear);

        }
        else if(ScoreManager.finalScore <= 200)
        {
            text.DOText("A", 4, scrambleMode: ScrambleMode.Uppercase).SetEase(Ease.Linear);
        }
        else if (ScoreManager.finalScore <=300)
        {
            text.DOText("B", 4, scrambleMode: ScrambleMode.Uppercase).SetEase(Ease.Linear);
        }
        else
        {
            text.DOText("C", 4, scrambleMode: ScrambleMode.Uppercase).SetEase(Ease.Linear);
        }
    }
}
