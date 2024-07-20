using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changePassword : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField edtUser;
    public TMP_InputField edtPassword;
    public TMP_InputField edtPasswordNew;
    public TMP_InputField edtPasswordNew1;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem eventSystem;
    public Button btnLogin;
    public bool checklogin = false;
    public string apiUrl = "http://localhost:3000/api/doimatkhau";
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
        if (Input.GetKey(KeyCode.Return))
        {
            btnLogin.onClick.Invoke();
        }
        // tab 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem
                .currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnDown();
            if (next != null)
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

    public void checkPasswordNew()
    {
        var user = edtUser.text;
        var password = edtPassword.text;

        // gọi api
        var u = edtUser.text;
        var p = edtPassword.text;
        var pNew = edtPasswordNew.text;
        var pNew1 = edtPasswordNew1.text;   
        if(u == "" || p == "" || pNew == "" || pNew1 == "")
        {
            txtError.text = "Vui lòng không để trống";
        }else if (pNew != pNew1)
        {
            txtError.text = "Mật khẩu mới và nhập lại phải trùng nhau";
        }
        else
        {
            string jsonData = "{\"username\":\"" + u + "\",\"password\":\"" + p + "\",\"passwordNew\":\"" + pNew + "\"}";
            StartCoroutine(PostRequest(jsonData));
            Debug.Log(jsonData);
        }
       
        
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
                //Debug.LogError("Error: " + request.error);
                txtError.text = "Đổi mật khẩu không thành công";
            }
            else
            {
                // Xử lý phản hồi thành công
                Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;

                ResponseDataNewPass res_data = JsonUtility.FromJson<ResponseDataNewPass>(json);
                if (res_data.status == 400)
                {
                    Debug.Log(res_data.message);
                    txtError.text = res_data.message;
                }
                if(res_data.status == 200)
                {
                    txtError.text = "Đổi mật khẩu thành công";
                }
            }
            request.Dispose();
        }
    }


}
[System.Serializable]
class ResponseDataNewPass
{
    public int status;
    public string message;
    public UserDataNewPass[] data;
}

[System.Serializable]
class UserDataNewPass
{
    public string _id, username, password, createdAt, updatedAt;
    public int score, x, y, level, hp, __v;
}
