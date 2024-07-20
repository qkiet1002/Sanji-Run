using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitUser : MonoBehaviour
{
    public string apiUrl = "http://localhost:3000/api/saveUser";
    int sumLife;
    string username;
    // public Text usernameText;
    public GameSession gameSession; // Đối tượng GameSession
    //public LoginUser loginUser;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Scene1.scene1 != null && Scene1.scene1.username != null)
        {
            username = Scene1.scene1.username;
        }
        else
        {
            username = "kiet";
            Debug.LogWarning("Không có giá trị truyền vào cho username. Đã sử dụng giá trị mặc định 'kiet'.");
        }

       // Debug.Log("username của bạn là " + username);
    }

    public void saveUser()
    {
        
  
        // Lấy số mạng hiện tại từ GameSession
        sumLife = gameSession.CapNhatMang();

        // Trong phương thức của class khác
        Vector3 playerPosition = transform.position;

        // Lấy tọa độ x, y, z của playerPosition
        float playerPosX = playerPosition.x;
        float playerPosY = playerPosition.y;
        float playerPosZ = playerPosition.z;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;


        // lấy life



        string jsonData = "{\"username\":\"" + username + "\",\"x\":\"" + playerPosX + "\",\"y\":\"" + playerPosY + "\",\"level\":\"" + currentIndex + "\",\"hp\":\"" + sumLife + "\"}";
        StartCoroutine(PostRequest(jsonData));
        Debug.Log(jsonData);
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
            }
            request.Dispose();
        }
    }
}
