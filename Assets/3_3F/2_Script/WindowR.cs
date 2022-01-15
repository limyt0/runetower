using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class WindowR : MonoBehaviour
{
    bool state_door = false;
    float door_angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state_door == true && door_angle >= -120)
        {
            door_angle -= Time.deltaTime * 30;
            this.transform.parent.rotation = Quaternion.Euler(0, door_angle, 0);
            print(door_angle);
        }
        if (state_door == false && door_angle <= 0)
        {
            door_angle += Time.deltaTime * 30;
            this.transform.parent.rotation = Quaternion.Euler(0, door_angle, 0);
            print(door_angle);
        }

    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" )
        {
            state_door = !state_door;
           
        }
     

    }
}
