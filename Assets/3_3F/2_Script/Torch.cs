using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //양초가 근처에 다가오면 파티클&라이트 활성화   
        if (other.transform.tag=="candle")
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
