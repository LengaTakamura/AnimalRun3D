using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPowerKangaroo : MonoBehaviour
{
    [SerializeField]KangarooMove kangarooMove;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();    
    }
    // Start is called before the first frame update
    public void SetTurnPower()
    {
        kangarooMove.turnPower = slider.value;
    }
}
