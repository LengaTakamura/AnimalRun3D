using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaText : MonoBehaviour
{

    [SerializeField] Slider slider; 
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Stamina:" + slider.value.ToString("F0").PadLeft(4, '0');
    }
}
