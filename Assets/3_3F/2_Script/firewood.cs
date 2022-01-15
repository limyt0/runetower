using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class firewood : MonoBehaviour
{
    public GameObject fire;
    void Start()
    {
        
    }

    int count = 0;
    
    void Update()
    {
        if(count==1)
        {
            fire.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.1f);
            fire.transform.SetParent(this.transform);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="candle")
        {
            if (count == 0)
            { fire=Instantiate(fire, this.transform.position, this.transform.rotation); }
            count = 1;
            this.transform.tag = "firewood_fire";
        }
    }
}
