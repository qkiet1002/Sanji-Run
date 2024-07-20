using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene2 : MonoBehaviour
{
    public TextMeshProUGUI display_player_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        display_player_name.text = Scene1.scene1.username;
    }
}
