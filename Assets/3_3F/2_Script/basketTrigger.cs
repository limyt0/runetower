using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class basketTrigger : MonoBehaviour
{
    public GameObject lune3;
    public Transform lune3Point;
    int count = 0;
    int one = 0;
    float time = 0;
    bool cube_true=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 3 && one == 0 && cube_true == true)
        {
            time += Time.deltaTime;
            if (time >= 3f)
            {
                Instantiate(lune3, lune3Point.position, lune3Point.rotation);
                one = 1;
            }
        }
    }

    

   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="item")
        {
            count++;
            other.tag = "none";
            
        }
        if(other.tag=="firewood_fire"&&count==3)
        {
            cube_true = true;
        }
    }
}


