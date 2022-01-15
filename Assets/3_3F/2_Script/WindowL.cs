using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowL : MonoBehaviour
{
    //bool state_door = false;
    float door_angle = 0;
    public Transform goal_effect;
    //public GameObject DoorBox;
    //public Transform key;
    // Start is called before the first frame update
    void Start()
    {

    }
    

    void Update()
    {
        //if (Vector3.Distance(this.transform.position, key.position) <= 1)
        // {
        if (goal_effect.gameObject.activeSelf == true)
        {
            if (door_angle <= 100)
            {
                
                
                door_angle += Time.deltaTime * 30;
                this.transform.parent.rotation = Quaternion.Euler(0, door_angle, 0);
                //this.transform.GetComponent<MeshCollider>().isTrigger = true;
                
                //print(door_angle);
            }
        }

    }


}
