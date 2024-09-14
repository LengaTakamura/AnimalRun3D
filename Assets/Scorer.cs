using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using DG.Tweening;

public class Scorer : MonoBehaviour
{
    private static float[] scores = new float[6];

    [SerializeField] TextMeshProUGUI[] texts;

    ScoreManager scoreManager;

    TextMeshProUGUI rankingTitle;

    [SerializeField] Vector3 vect;
    void Start()
    {
        MovingUi();

       rankingTitle = GetComponent<TextMeshProUGUI>();


        if (ScoreManager.finalScore == 0)
        {

            scores[5] = ScoreManager.finalScore;

            Array.Sort(scores);

            GetRanking();

        }
        else
        {

            scores[5] = ScoreManager.finalScore;

            Array.Sort(scores);

            GetRanking();



            Debug.Log("スコアを計算" + ScoreManager.finalScore);
        }
    }
    void GetRanking()
    {
        for (int i = 0; i < scores.Length - 1; i++)
        {
            texts[i].text = "Score " + scores[i];

        }

    }

    void MovingUi()
    {
        RectTransform rect;

        rect = GetComponent<RectTransform>();

        rect.DOAnchorPos((vect), 3f).SetEase(Ease.OutBounce);
    }

}