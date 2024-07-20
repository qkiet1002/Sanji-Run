using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private AudioSource winnerSound;
    private bool levelCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            //winnerSound.Play();

            Invoke("CompleteLevel", 1f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                StartCoroutine(LoadNextLevel());
            }
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(1f);
            int currentindex = SceneManager.GetActiveScene().buildIndex;
            int nextindex = currentindex + 1;
            if(nextindex == SceneManager.sceneCountInBuildSettings)
            {
                nextindex = 0;

                SceneManager.LoadScene(nextindex);
            }
        }*/
}
