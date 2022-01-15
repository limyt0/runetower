using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControl : MonoBehaviour
{
    public Light light;
    public GameObject btn;

    
    
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
        if(this.transform.tag=="button1"||other.transform.tag=="Player")
        {
            light.intensity = 1;
            btn.transform.position = new Vector3(0, -0.2f, -20);
        }
        if(other.transform.tag=="object")
        {
            light.intensity = 1;
            btn.transform.position = new Vector3(0, -0.2f, -20);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            light.intensity = 0.1f;
            btn.transform.position = new Vector3(0, 0, -20);
        }
        if (other.transform.tag == "object")
        {
            light.intensity = 0.1f;
            btn.transform.position = new Vector3(0, 0, -20);
        }
    }
}
