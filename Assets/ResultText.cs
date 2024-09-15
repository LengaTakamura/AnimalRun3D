using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    ScoreManager scoreManager;
    TextMeshProUGUI resultText;
    float score;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPunchPosition(new Vector3(300, 0, 0), 3f, 5, 1f);
        resultText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(nameof(Result));
    }

    IEnumerator Result()
    {
        yield return new WaitForSeconds(2.5f);
        resultText.text = "Score:" + (1000 - ScoreManager.finalScore).ToString("F2").PadLeft(6, '0');
    }
   
}
