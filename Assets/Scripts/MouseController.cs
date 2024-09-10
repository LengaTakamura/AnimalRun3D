using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]Camera came;
    [SerializeField] GameObject horse;
    [SerializeField]GameObject bullet;
    Vector3 vect;
    public float speed;
    public bool _isGameClea;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 pos = came.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5f));
        transform.position = pos;
       

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "kangaroo" )
        {
            if (Input.GetMouseButtonDown(0))
            {
               _isGameClea = true;
                Debug.Log("clear");
            }
        }
    }
}
