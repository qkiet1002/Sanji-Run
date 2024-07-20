using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Enemy : MonoBehaviour
{
    public float mauHienTai;
    public float mauToiDa = 20;
    public HPFollowerQuai Healthbar;

    void Start()
    {
        Healthbar.SetHealth(mauHienTai, mauToiDa);
    }


    public void TruMau()
    {
        mauHienTai -= 2;

        Healthbar.SetHealth(mauHienTai, mauToiDa);
        if (mauHienTai < 0)
        {
            Destroy(this.gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "skill2")
        {
            TruMau();
        }
    }
}

