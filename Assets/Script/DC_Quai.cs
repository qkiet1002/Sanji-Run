using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_Quai : MonoBehaviour
{   
   
    public float start, end;
    private bool isRigth; // if = true di chuyen ben phai ngược lai d.ch trái
    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var positionQuai = transform.position.x;

 
        // đuổi theo player

        if(player !=null)
        {
            var positionPlayer = player.transform.position.x;
            if (positionPlayer > start && positionPlayer < end)
            {
                if (positionPlayer < positionQuai)
                {
                    isRigth = false;
                }
                if (positionPlayer > positionQuai)
                {
                    isRigth = true;
                }

            }
        }

        // di chuyển
        
        if (positionQuai <start) 
        {
            isRigth = true;
        }
        if (positionQuai > end)
        {
            isRigth = false;
        }

        Vector2 scale = transform.localScale;
        //
        if (isRigth) 
        {
            transform.Translate(Vector3.right*2f*Time.deltaTime);
            scale.x = -1;
        }
        else
        {
            transform.Translate(Vector3.left*2f*Time.deltaTime);
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trai")
        {
            isRigth = isRigth ? false : true;
        }
    }

    public void SetStart(float start)
    {
        this.start = start;
    }

    public void SetEnd(float end)
    {
        this.end = end;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
