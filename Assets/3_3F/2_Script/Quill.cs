﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "box")
        {
            print("깃털잡을수있음");
            this.transform.tag = "item";
        }
    }
}
