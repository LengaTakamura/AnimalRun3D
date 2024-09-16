using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

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

        StartCoroutine(nameof(Ranking));
    }

    IEnumerator Ranking()
    {
       yield return new WaitForSeconds(2.5f);

        scores[5] = 1000 - ScoreManager.finalScore;

        Array.Sort(scores);

        Array.Reverse(scores);

        GetRanking();

    }

    void GetRanking()
    {
        for (int i = 0; i < scores.Length - 1; i++)
        {
            texts[i].text = scores[i].ToString("F0").PadLeft(5, '0');

        }

    }

    void MovingUi()
    {
        RectTransform rect;

        rect = GetComponent<RectTransform>();

        rect.DOAnchorPos((vect), 3f).SetEase(Ease.OutBounce);
    }

}