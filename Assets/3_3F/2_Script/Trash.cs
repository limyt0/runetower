using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public GameObject lune1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print("Player가 들어왔다");
            lune1.SetActive(true);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "stone")
        {
            print("돌이 있다");
            lune1.SetActive(true);
       
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print("Player가 나갔다");
            lune1.SetActive(false);
        }

    }
}
