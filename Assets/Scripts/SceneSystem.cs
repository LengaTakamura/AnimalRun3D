using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    Image  fadePanel;
    Animator fadeAnimator;
    static public SceneSystem instance;
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void FadeOut()
    {
        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
        fadeAnimator = GameObject.Find("FadePanel").GetComponent<Animator>();
        fadePanel.enabled = true;
        fadeAnimator.enabled = true;
    }

    // Update is called once per frame
    public void LoadScene()
    {
        StartCoroutine(nameof(ForFadeTime));
    }
    IEnumerator ForFadeTime()
    {
        yield return new WaitForSeconds(3F);
        SceneManager.LoadScene("InGame");
    }

}
