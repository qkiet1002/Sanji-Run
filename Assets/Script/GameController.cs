using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int count = 3;
    public GameObject player;
    [SerializeField]private float start_enemy, end_enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count -- > 0)
        {
            float random_quai = Random.Range(start_enemy,end_enemy);
            GameObject qv = (GameObject) Instantiate(Resources.Load("QuaiVat/Quai"),
                new Vector3(random_quai, 1, 0), Quaternion.identity);
            qv.GetComponent<DC_Quai>().SetStart(random_quai -5);
            qv.GetComponent<DC_Quai>().SetEnd(random_quai +5);
            qv.GetComponent<DC_Quai>().SetPlayer(player);
        }
        
    }
    
 
}
