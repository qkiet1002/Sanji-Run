using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int playerLive = 1;
    // Start is called before the first frame update
    public void Awake()
    {
        int numbersession = FindObjectsOfType<GameSession>().Length;
        if (numbersession > 1)
        {
            Destroy(gameObject); // đã tồn tại 
        }
        else
        {
            DontDestroyOnLoad(gameObject); // không tồn tại
        }
        
    }
    public void ReceiveLife(int life)
    {
        playerLive = life;
        Debug.Log(playerLive);
    }

    public  int CapNhatMang()
    {
        return this.playerLive;
    }
    //khi player chết
    public void PlayerDeath()
    {
        if(playerLive > playerLive)
        {
            TakeLife();
        }
        else
        {
            RestGameSession();
        }
    }

    private void RestGameSession()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    public void TakeLife()
    {
        //lấy index của scene hiện tại
        int currentsceneidex = SceneManager.GetActiveScene().buildIndex;
        //load lại scene hiện tại 
        SceneManager.LoadScene(currentsceneidex);
    }
  
}
