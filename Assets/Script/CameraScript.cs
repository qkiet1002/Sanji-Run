using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float startX, endX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // lấy đc vị trí trục x của player
        var playerX = player.transform.position.x;
        var camX = transform.position.x;
        if (playerX > startX && playerX < endX)
        {
            camX = playerX;

        }
        else
        {
            if (playerX < startX)
            {
                camX = startX;
            }
            if (playerX > endX)
            {
                camX = endX;
            }
        }
        transform.position = new Vector3(camX, player.transform.position.y, -10);
    } 
       
}
