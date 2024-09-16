using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public  class ScoreManager : MonoBehaviour
{

    TextMeshProUGUI scoreText;

    public static float score = 0f;

    [SerializeField] PlayerMove playerMove;
    [SerializeField] KangarooMove kangarooMove;
    public static float finalScore;
    TextMeshProUGUI finalScoreText;
    public bool isGameOver;
    [SerializeField] AudioSource bgm;
    [SerializeField] Slider[] sliders;
    [SerializeField]Pauser pauser;
    float score2;
    // Start is called before the first frame update
    void Start()
    {
        
        score = 0f;
        isGameOver = false;
        try 
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        catch { }
        finalScoreText = GameObject.Find("FinalScore").GetComponent <TextMeshProUGUI>();
    }
    private void FixedUpdate()
    {
        if (!pauser.isPause)
        {
            score += Time.deltaTime;
             score2 = score;
        }
        else
        {
            score2 = score;
        }
       
        scoreText.text = score.ToString("F2").PadLeft(6,'0');

    }

    public void ScoreCount()
    {
        scoreText.enabled = false;
         finalScore = score;
        //StartCoroutine(nameof(ScoreActive));
    }

    IEnumerator ScoreActive()
    {
        yield return new WaitForSeconds(3);
        finalScoreText.color = Color.white;
        finalScoreText.DOFade(1f, 10f);
        finalScoreText.text = "Score"+ ":" + finalScore.ToString("F2").PadLeft(6, '0');
    }

    public void GameOver()
    {
        isGameOver = true;
        sliders[0].gameObject.SetActive(false);
        sliders[1].gameObject.SetActive(false);
        kangarooMove.enabled = false;
        playerMove.enabled = false;
        bgm.Pause();    
        scoreText.enabled = false;
        finalScoreText.DOFade(1f, 10f);
        finalScoreText.text = "GameOver";
        StartCoroutine(SceneSystem.ForFadeTime("Title"));
    }

    public void Clear()
    {
        sliders[0].gameObject.SetActive(false);
        sliders[1].gameObject.SetActive(false);
        kangarooMove.enabled = false;
        playerMove.enabled = false;
    }

    

    
}
