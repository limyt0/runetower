using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCubeControl : MonoBehaviour
{
    public Transform player;

    Vector3 pos; //현재위치

    float delta = 11.0f; // 좌(우)로 이동가능한 (x)최대값

    float speed = 1.0f; // 이동속도

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 v = pos;

        v.z += delta * Mathf.Sin(Time.time * speed);

        // 좌우 이동의 최대치 및 반전 처리

        this.transform.position = v;
        
    }
    
}
