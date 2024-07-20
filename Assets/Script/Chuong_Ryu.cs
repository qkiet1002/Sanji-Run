using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chuong_Ryu : MonoBehaviour
{
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
       
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isRight == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 5f);
             transform.localScale = new Vector2(-1f,1f); 
        }
        if (isRight == true)
        {
            transform.localScale = new Vector2(-1f, 1f);
            transform.Translate(Vector3.right * Time.deltaTime * 5f);
        }
       // transform.Translate((isRight ? Vector3.right : Vector3.left) * Time.deltaTime*5f);
        
     

    }
    public void setIsRight(bool isRight)
    {
        this.isRight = isRight; 
    }
}
