using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove PlayerMove;
    [SerializeField] GameObject obj;
    Text text;
    void Start()
    {
        PlayerMove = obj.GetComponent<PlayerMove>();
        text = GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "speed " + PlayerMove.speed  ;
        
    }
}
