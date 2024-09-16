using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour
{
    [SerializeField] GameObject playerMoveObj;
    [SerializeField] GameObject kangarooMoveObj;
    [SerializeField]CanvasGroup canvasGroup;
    [SerializeField]GetPower getPower;
    [SerializeField]GetPowerKangaroo getPowerKangaroo;
    public bool isPause;
    public int count = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           
            count++;

            if(count == 1)
            {
                Pause();
            }
            else if(count == 2)
            {
                ReStart();
                getPower.SetTurnPower();
                getPowerKangaroo.SetTurnPower();
            }
            else if(count % 2 ==0)
            {
                ReStart();
                getPower.SetTurnPower();
                getPowerKangaroo.SetTurnPower();

            }
            else if(count % 2 == 1)
                {
                Pause();
            }
        }

        if (!isPause)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible=true ;
            Cursor.lockState = CursorLockMode.None;
        }
    }

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
