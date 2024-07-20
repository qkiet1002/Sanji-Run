using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;



public class LoginUser : MonoBehaviour
{
    public TMP_InputField edtUser;
    public TMP_InputField edtPassword;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem eventSystem;
    public Button btnLogin;
    public bool checklogin = false;
    public string apiUrl = "http://localhost:3000/api/checklogin";

    // gửi username cho exit 

    // Start is called before the first frame update
    void Start()
    {
        // chọn th đầu tiên
        eventSystem = EventSystem.current;
        first.Select();

    }

    // Update is called once per frame
    void Update()
    {
        // enter login
        if(Input.GetKey(KeyCode.Return)) 
        {
        btnLogin.onClick.Invoke();
        }
        // tab 
        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            Selectable next = eventSystem
                .currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
        }
        // shifit 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = eventSystem
                .currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
    }

    public void CheckLogin()
    {

        // gọi api
        // set cứng
        if (checklogin == true)
        {
            SceneManager.LoadScene(1);
        }

        var u = edtUser.text;
        var p = edtPassword.text;
        string jsonData = "{\"username\":\"" + u + "\",\"password\":\"" + p + "\"}";
        StartCoroutine(PostRequest(jsonData));
        Debug.Log(jsonData);

    }

    public void Check()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator PostRequest(string jsonData)
    {
        // Tạo request
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            // Gửi dữ liệu JSON
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Đợi phản hồi từ server
            yield return request.SendWebRequest();

            // Kiểm tra lỗi
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Xử lý phản hồi thành công
                Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;

                ResponseData res_data = JsonUtility.FromJson<ResponseData>(json);
                if (res_data.status == 400)
                {
                    Debug.Log(res_data.message);
                    txtError.text = res_data.message;
                    checklogin = false;
                }else if(res_data.status == 200)
                {
                    checklogin = true;  
                    UserData[] manguser = res_data.data;
                    UserData user = manguser[0];
                    Debug.Log(user.username);
                    Debug.Log(user.password);
                    Debug.Log(user.x);
                    Debug.Log(user.y);
                    Debug.Log(user.hp);
                    Debug.Log(user.level);

                    // Sau khi nhận dữ liệu từ server
                    DataManager.Instance.playerPositionX = user.x;
                    DataManager.Instance.playerPositionY = user.y;
                    DataManager.Instance.playerUserName = user.username;
                    DataManager.Instance.playerHP = user.hp;

                    // Chuyển sang scene mới
                    SceneManager.LoadScene("Scene2");
                }
            }
            request.Dispose();
        }
    }

}

[System.Serializable]
class ResponseData
{
    public int status;
    public string message;
    public UserData[] data;
}

[System.Serializable]
class UserData
{
    public string _id, username, password, createdAt, updatedAt;
    public int score, level, hp, __v;
    public float x, y;
}