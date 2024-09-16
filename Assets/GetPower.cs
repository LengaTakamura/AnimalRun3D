using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPower : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    Slider powerSlider;

    private void Start()
    {
        powerSlider = GetComponent<Slider>();
    }
    
    public void SetTurnPower()
    {
        playerMove.turnPower = powerSlider.value;
    }
}
