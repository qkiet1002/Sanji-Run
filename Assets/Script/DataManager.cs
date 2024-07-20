using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // Dữ liệu bạn muốn chia sẻ giữa các scene
    public float playerPositionX;
    public float playerPositionY;
    public string playerUserName;
    public int playerHP;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
