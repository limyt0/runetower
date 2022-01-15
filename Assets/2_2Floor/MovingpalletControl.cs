using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingpalletControl : MonoBehaviour
{
    public Transform player;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    //위에 올라갔을때 같이 움직이게 하기
    private void OnCollisionEnter(Collision collision)
    {
        //Vector3 playerz = new Vector3();
        if (collision.transform == player) {
           collision.transform.SetParent(transform);
        }
        

    }
    //내려갔을때 따로 움직이게 하기
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == player)
        {        
            collision.transform.SetParent(null);
        }
    }
}
