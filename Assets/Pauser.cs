using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour
{
    [SerializeField] GameObject playerMoveObj;
    [SerializeField] GameObject kangarooMoveObj;
    [SerializeField]CanvasGroup canvasGroup;
  
    public bool isPause;


    public void Pause()
    {
        isPause = true; 
     
        canvasGroup.gameObject.SetActive(true);
        Debug.Log("PAuse");

        

    }

    public void ReStart()
    {
        isPause=false;

        canvasGroup.gameObject.SetActive(false);

    }
}
