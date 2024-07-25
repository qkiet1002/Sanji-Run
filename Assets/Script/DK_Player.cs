using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class DK_Player : MonoBehaviour
{

    // âm thanh
    public AudioSource source;
    public AudioClip Chet,Resert,ChieuQ,ChieuW,ChieuE;
    // hiệu ứng hồi máu
    public GameObject player;
   // public GameObject hieuUng_HoiMau;
    //skill 2
   // public GameObject skill2;
    // chạy nhẩy
    private Rigidbody2D rb;
    private float tocDo = 7f;
    private float lucNhay = 7f;
    private Animator animator;
    private bool isNen;
    private float gia_tri_float_nhay = 0;
    public ParticleSystem psBui; // hiệu ứng bụi
    public GameObject menu; //menu
    private bool isPlaying = true;
    // thanh máu
    public ThanhMau thanhMau;
    public float mauHienTai;
    public float mauToiDa = 10;
    // mang hs
    public TextMeshProUGUI textscore;
    int sumLife;
    // co thể đk
    private bool coTheDie = true;

    // thiết lập đối tượng gamesession đẻ nhận mạng
    public GameSession gameSession; // Đối tượng GameSession
    //lấy vị trí player
    public static DK_Player Instance;

    // kiểm tra nếu qua màng thì set lại vị trí
    int ckeckLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        mauHienTai = mauToiDa;
        thanhMau.CapNhatThanhMau(mauHienTai,mauToiDa);
        // Thiết lập đối tượng GameSession
        gameSession = FindObjectOfType<GameSession>();

        sumLife = (int) gameSession.CapNhatMang();

            if (DataManager.Instance != null)
            {
                float playerPositionX = DataManager.Instance.playerPositionX;
                float playerPositionY = DataManager.Instance.playerPositionY;
            // Thiết lập vị trí ban đầu cho player
            if (!float.IsNaN(playerPositionX) && !float.IsNaN(playerPositionY))
                {
                    transform.position = new Vector3(playerPositionX, playerPositionY+3f, transform.position.z);
                }
                else
                {
                    // Sử dụng giá trị mặc định nếu không có giá trị nào được trả về từ DataManager
                    transform.position = new Vector3(-11f, -0.6f, transform.position.z);
                    Debug.LogWarning("Player position data not found. Using default position.");
                }
            }
            else
            {
                Debug.Log("DataManager.Instance is null.");
            }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!coTheDie)
        {
            // Ngăn chặn xử lý đầu vào khi nhân vật đã chết
            return;
        }

        // Xử lý đầu vào điều khiển nhân vật
        DieuKhienPlayer();
        if (player != null)
        {
            // Cập nhật vị trí của hoimau theo vị trí của nhân vật (rb)
            player.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y);
        }


    }
    private void DieuKhienPlayer()
    {
        // ql bụi
        Quaternion quaternion = psBui.transform.localRotation;

        // quay mặt 
        Vector2 scale = transform.localScale;
        // animation
        animator.SetBool("isRunning", false);
        animator.SetFloat("isNhay", gia_tri_float_nhay);
       

        // di chuyen
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Translate(Vector3.right*tocDo*Time.deltaTime);
            // quay mặt 
            scale.x = 0.5f;
         
            // animation

            if (!isNen)
            {
                animator.SetBool("isRunning", false);
                animator.SetFloat("isNhay", gia_tri_float_nhay + 1);
              
            }
            else
            {
                // hiệu ứng bụi
                quaternion.y = 180;
                psBui.transform.localRotation = quaternion;
                psBui.Play();
                // hiệu ứng chạy
                animator.SetBool("isRunning", true);
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Translate(Vector3.left*tocDo*Time.deltaTime);
            // quay mặt 
            scale.x = -0.5f;

            // animation
            if (!isNen)
            {
                animator.SetBool("isRunning", false);
                animator.SetFloat("isNhay", gia_tri_float_nhay + 1);
               
            }
            else
            {
                // hiệu ứng bụi
                quaternion.y = 0;
                psBui.transform.localRotation = quaternion;
                psBui.Play();
                // hiệu ứng chạy
                animator.SetBool("isRunning", true);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetFloat("isNhay", gia_tri_float_nhay +1);
            animator.SetBool("isRunning", false);
            if (isNen)
            {
                rb.velocity = new Vector2(rb.velocity.x, lucNhay);
                isNen = false;
               
            }
        }
        // 3 loại getkey 
        //Getkey : nhấn giữ hoạt động
        //GetKeyDown : nhấn 1 lần
        //GetKeyUp : nhấn xong r mới hoạt động
        // show menu dừng game
        if (Input.GetKeyDown(KeyCode.P))
        {
          
            ShowMenu();
            
        }
       

        // skill 1
        if (Input.GetKeyDown(KeyCode.Q))
        {
            source.PlayOneShot(ChieuQ);
            animator.SetTrigger("isSKill1");
            if(scale.x > 0)
            {
                rb.velocity = new Vector2(5f, rb.velocity.y);
                //
                GameObject skill_1 = (GameObject)Instantiate(
                 Resources.Load("QuaiVat/skill1"),
                 new Vector2(rb.transform.position.x, rb.transform.position.y),
                 Quaternion.identity
                 );
                Quaternion quaternion1 = skill_1.transform.localRotation;
                quaternion.y = 0;
                skill_1.transform.localRotation = quaternion;
                Destroy(skill_1, 1);
            }
            if(scale.x < 0)
            {
                rb.velocity = new Vector2(-5f, rb.velocity.y);
                //
                GameObject skill_1 = (GameObject)Instantiate(
              Resources.Load("QuaiVat/skill1"),
              new Vector2(rb.transform.position.x, rb.transform.position.y),
              Quaternion.identity
              );
                Quaternion quaternion1 = skill_1.transform.localRotation;
                quaternion.y = 180;
                skill_1.transform.localRotation = quaternion;
                Destroy(skill_1, 1);

            }

            // quay mặt 


        }
        // skill 2
        
        if (Input.GetKeyDown(KeyCode.W))
        {

            source.PlayOneShot(ChieuW);
            animator.SetTrigger("isSkill2");
            if (scale.x > 0)
            {

                GameObject skill_2 = (GameObject)Instantiate(
                   Resources.Load("QuaiVat/Electro slash"),
                   new Vector2(rb.transform.position.x, rb.transform.position.y),
                   Quaternion.identity
                   );
                Quaternion quaternion1 = skill_2.transform.localRotation;
                quaternion.y = 0;
                skill_2.transform.localRotation = quaternion;
                Destroy(skill_2, 1);

            }
            if (scale.x < 0)
            {

                GameObject skill_2 = (GameObject)Instantiate(
                Resources.Load("QuaiVat/Electro slash"),
                new Vector2(rb.transform.position.x, rb.transform.position.y),
                Quaternion.identity
                );
                Quaternion quaternion1 = skill_2.transform.localRotation;
                quaternion.y = 180;
                skill_2.transform.localRotation = quaternion;
                Destroy(skill_2, 1);


            }

            // quay mặt 
        }
        // skill 3
        if (Input.GetKeyDown(KeyCode.E))
        {
            source.PlayOneShot(ChieuE);
            animator.SetTrigger("isSkill3");
            if (scale.x > 0)
            {

                //
                GameObject skill_3 = (GameObject)Instantiate(
                 Resources.Load("QuaiVat/Red energy explosion Variant"),
                 new Vector2(rb.transform.position.x, rb.transform.position.y),
                 Quaternion.identity
                 );
                Quaternion quaternion1 = skill_3.transform.localRotation;
                quaternion.y = 0;
                skill_3.transform.localRotation = quaternion;
                Destroy(skill_3, 2);
            }
            if (scale.x < 0)
            {

                //
                GameObject skill_3 = (GameObject)Instantiate(
              Resources.Load("QuaiVat/Red energy explosion Variant"),
              new Vector2(rb.transform.position.x, rb.transform.position.y),
              Quaternion.identity
              );
                Quaternion quaternion1 = skill_3.transform.localRotation;
                quaternion.y = 180;
                skill_3.transform.localRotation = quaternion;
                Destroy(skill_3, 2);

            }

            // quay mặt 


        }
        // quay mặt 
        transform.localScale = scale;
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "nen")
        {
            isNen = true;
            textscore.text = "Life: " + sumLife.ToString();
        }
        if(collision.gameObject.tag == "quai")
        {
            TruMau();
        }
        if (collision.gameObject.tag == "Chuong_Ryu")
        {
            Destroy(collision.gameObject);
            TruMau();
        }
        if (collision.gameObject.CompareTag("heart"))
        {
            Destroy(collision.gameObject);
            CongLaiMau();
            player = (GameObject)Instantiate(
              Resources.Load("QuaiVat/Buff Variant"),
               new Vector2(rb.transform.position.x, rb.transform.position.y),
              Quaternion.identity
          );

            Destroy(player, 1);

        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            TruMau();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem trigger có chạm vào nhân vật không (kiểm tra qua tag hoặc layer)
        if (other.CompareTag("dan"))
        {
            // Gọi hàm xử lý sát thương trên nhân vật
            TruMau();
            Destroy(other.gameObject);
        }
        if ((other.CompareTag("life")))
        {
            string name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            congMang(1);

        }
        if (other.gameObject.CompareTag("Trap"))
        {
            TruMau();
        }
        // check Next level
        if (other.gameObject.CompareTag("nextLevel"))
        {
            
        }
    }
    // Hồi Sinh
    public void ChoiLai()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ShowMenu();
    }
    // thoát 
    public void Exit()
    {
        SceneManager.LoadScene(0);
        ShowMenu();
    }
    // show menu
    public void ShowMenu()
    {
        
        // nhấn p dừng
        if (isPlaying)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            isPlaying = false;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            isPlaying = true;
        }
    }
    public void TruMau()
    {
        if (!coTheDie)
        {
            return; // Ngăn chặn xử lý khi nhân vật không thể chết
        }

        mauHienTai -= 2;
        thanhMau.CapNhatThanhMau(mauHienTai, mauToiDa);

        if (mauHienTai <= 0)
        {
            if(sumLife >= 0)
            {
               
                if(sumLife == 0)
                {
                    sumLife -= 2;
                }
                else
                {
                    sumLife -= 1;
                    mauHienTai += 10f;
                    thanhMau.CapNhatThanhMau(mauHienTai, mauToiDa);
                }
            }
            if(sumLife < 0)
            {
                Die();
            }

            // Truyền giá trị sumLife cho GameSession
            gameSession.ReceiveLife(sumLife);
            
        }
        textscore.text = "Life: " + sumLife.ToString();
    }

    public void congMang(int mang)
    {

        sumLife += mang;
        gameSession.ReceiveLife(sumLife);
        textscore.text = "Life: " + sumLife.ToString();
    }
    public void Die()
    {
   
            coTheDie = false; // Ngăn chặn người chơi điều khiển khi nhân vật đã chết
            animator.SetTrigger("isDeath");
            // Các hành động khác khi nhân vật chết
            ShowMenu();
    }
    public void CongLaiMau()
    {

 
        if(mauHienTai < mauToiDa)
        {
       mauHienTai += 2;
        thanhMau.CapNhatThanhMau(mauHienTai, mauToiDa);
        }


    }

}

