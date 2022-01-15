using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap_controll : MonoBehaviour
{
    public Transform player1; //플레이어의 포지션을 가져오기 위한 플레이어 객체정보
    float x_val;
    float y_val;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x_val = 0.34f + (player1.position.x * (0.855f / 363.0f));
        y_val = -0.46f + (player1.position.z * (0.9f / 382.0f));

        this.transform.localPosition = new Vector3(x_val, y_val, -2.6f);
        //print(x_val+","+ y_val);
    }
}
