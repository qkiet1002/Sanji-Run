using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APICaller : MonoBehaviour
{
    private const string apiUrl = "https://example.com/api"; // Thay thế URL của API của bạn ở đây

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
            }
        }

    }

    IEnumerator GetRequest()
    {
        // Tạo request
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
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
            }
        }
    }

    IEnumerator PostRequest2(string url)
    {
        // Create a new form
        WWWForm form = new WWWForm();
        // Add your parameters to the form if needed
        form.AddField("username", "khoa");
        form.AddField("password", "123");
        // Create a UnityWebRequest POST object
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Print response body
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }

    void Start()
    {
        ///////Goi kieu POST//////////
        // Dữ liệu JSON bạn muốn gửi
        string jsonData = "{\"key\":\"value\"}";

        // Gọi hàm PostRequest với dữ liệu JSON
        StartCoroutine(PostRequest(jsonData));

        /////Goi kieu GET/////////////
        StartCoroutine(GetRequest());

        ///Post kieu 2
        StartCoroutine(PostRequest2("dia chi api"));

    }
}
