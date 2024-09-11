using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{

    TextMeshProUGUI scoreText;

    public static float score = 0f;

    [SerializeField] PlayerMove playerMove;
    [SerializeField] KangarooMove kangarooMove;
    public float finalScore;
    TextMeshProUGUI finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        try 
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        catch { }
        finalScoreText = GameObject.Find("FinalScore").GetComponent <TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        score += Time.deltaTime;
        scoreText.text = score.ToString("F2").PadLeft(6,'0');

    }

    public void ScoreCount()
    {
        scoreText.enabled = false;
         finalScore = score;
        StartCoroutine(nameof(ScoreActrive));
    }

    IEnumerator ScoreActrive()
    {
        yield return new WaitForSeconds(3);
        finalScoreText.color = Color.white;
        finalScoreText.DOFade(1f, 10f);
        finalScoreText.text = "Score"+ ":" + finalScore.ToString("F2").PadLeft(6, '0');
    }

    public void GameOver()
    {
        scoreText.enabled = false;
        finalScoreText.DOFade(1f, 10f);
        finalScoreText.text = "GameOver";
    }


    
}
