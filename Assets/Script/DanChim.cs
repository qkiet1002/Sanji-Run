using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DanChim : MonoBehaviour
{
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 0f);
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (isRight == true)
        {
            transform.localScale = new Vector2(-1f, 1f);
            transform.Translate(Vector3.right * Time.deltaTime * 0f);
        }
    }
    public void setIsRight(bool isRight)
    {
        this.isRight = isRight;
    }
}
