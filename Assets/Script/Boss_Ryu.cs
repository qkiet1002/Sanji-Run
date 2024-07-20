using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Ryu : MonoBehaviour
{
    public float start, end;
    private bool isRigth; // if = true di chuyen ben phai ngược lai d.ch trái
    public GameObject player;
    private Animator animator;
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

        // di chuyển

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
            isRigth = true;
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
            scale.x = -1;
            animator.SetBool("diChuyen", true);
        }
        else
        {
            isRigth= false;
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
            scale.x = 1;
            animator.SetBool("diChuyen", true);
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
IEnumerator AttackDelay()
{
    isAttacking = true;
    animator.SetTrigger("attack");

    yield return new WaitForSeconds(attackCooldown);

    GameObject gameObject = (GameObject)Instantiate(
        Resources.Load("QuaiVat/Bomb"),
        new Vector3(transform.position.x, transform.position.y, transform.position.z),
        Quaternion.identity
    );

    Chuong_Ryu chuongRyu = gameObject.GetComponent<Chuong_Ryu>();
    if (chuongRyu != null)
    {
        chuongRyu.setIsRight(isRigth);
    }

    isAttacking = false;
}
}
