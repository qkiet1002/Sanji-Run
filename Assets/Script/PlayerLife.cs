using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour

{


    private Rigidbody2D rb;
    private Animator animator;
   
 
    // Start is called before the first frame update
    void Start()
    {
     
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
   
        if ((collision.gameObject.tag == "vungDie"))
        { 
          
            animator.SetTrigger("vungDie");
            float animationDuration = 0.4f;// Thời gian của animation "vungDie"
            StartCoroutine(DelayedSceneLoad(animationDuration));

        }

    }
    // bộ điếm tg 
    private IEnumerator DelayedSceneLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
