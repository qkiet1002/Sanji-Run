using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuaiVatEch : MonoBehaviour
{
    private MauQuaiVatEch thanhMau;
    public float mauHienTai;
    public float mauToiDa = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*
    public void TruMau()
    {
        mauHienTai -= 2;
        thanhMau.CapNhatThanhMau(mauHienTai, mauToiDa);
        if (mauHienTai < 0)
        {
             Destroy(this.gameObject);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "Player")
        {
            TruMau();
           
        }
      
    }
*/

}
