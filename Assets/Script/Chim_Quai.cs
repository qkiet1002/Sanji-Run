using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chim_Quai : MonoBehaviour
{
    public float start, end;
    private bool isRigth;
    private Animator animator;
    public GameObject player;
    private bool isAttacking;
    public float attackCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {

        var positionQuai = transform.position.x;
        if (player != null)
        {
            var positionPlayer = player.transform.position.x;
            if (positionPlayer > start && positionPlayer < end)
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackDelay());
                }
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
        if (positionQuai < start)
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
            animator.SetBool("isRungChim", true);
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
            scale.x = -1;
        }
        else
        {
            animator.SetBool("isRungChim", true);
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    IEnumerator AttackDelay()
    {
        isAttacking = true;


        yield return new WaitForSeconds(attackCooldown);

        GameObject gameObject = (GameObject)Instantiate(
            Resources.Load("QuaiVat/dan_Chim"),
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity
        );

        DanChim danChim = gameObject.GetComponent<DanChim>();
        if (danChim != null)
        {
            danChim.setIsRight(isRigth);
        }

        isAttacking = false;
    }
}
