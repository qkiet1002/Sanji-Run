using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
public class RegisterUser : MonoBehaviour
{
    public TMP_InputField edtUser;
    public TMP_InputField edtPassword;
    public TMP_InputField edtPassword1;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem eventSystem;
    public Button btnLogin;
    public bool checklogin = false;
    public string apiUrl = "http://localhost:3000/api/add-user";
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
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
    public void CheckRegister()
    {
        var user = edtUser.text;
        var password = edtPassword.text;
        var password1 = edtPassword1.text;

        // gọi api
        if(user == "" || password == "" || password1 == "")
        {
            txtError.text = "Phải điền đầy đủ thông tin";
        }else if(password != password1)
        {
            txtError.text = "Mật khẩu không trùng";
        }else
        {
            var u = edtUser.text;
            var p = edtPassword.text;
            string jsonData = "{\"username\":\"" + u + "\",\"password\":\"" + p + "\"}";
            StartCoroutine(PostRequest(jsonData));
            Debug.Log(jsonData);
        }

   
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

                ResponseDataRegister res_data = JsonUtility.FromJson<ResponseDataRegister>(json);
                if (res_data.status == 400)
                {
                    Debug.Log(res_data.message);
                    txtError.text = res_data.message;
                }
                else if (res_data.status == 200)
                {
                    txtError.text = res_data.message;
                }
            }
            request.Dispose();
        }
    }


}


class ResponseDataRegister
{
    public int status;
    public string message;
    public UserData[] data;
}
