using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoiMau : MonoBehaviour
{
    // Start is called before the first frame update
    public ThanhMau thanhMau;
    public float mauHienTai; 
    public float mauToiDa;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heart"))
        {     
            Destroy(collision.gameObject);
            CongLaiMau();


        }
    }
    public void CongLaiMau()
    {
 

        mauHienTai += 2;
        thanhMau.CapNhatThanhMau(mauHienTai, mauToiDa);
    }
}
