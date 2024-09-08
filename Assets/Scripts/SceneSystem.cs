using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    Image  fadePanel;
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
        fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
    }

    // Update is called once per frame
    public void LoadScene()
    {
        StartCoroutine(nameof(FadeOut));
    }
    IEnumerator FadeOut()
    {
        fadePanel.enabled = true;
        yield return new WaitForSeconds(3F);
        SceneManager.LoadScene("InGame");
    }

}
