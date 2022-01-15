using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform key;
    public Transform lune2Point;
    public GameObject lune2;
    public Transform bottle;
    private bool open = false;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, key.position) < 2f&&count==0)
        {
            this.GetComponent<Animation>().Play("ChestAnim");
            count = 1;
            Invoke("Ins_lune2", 3f);
            bottle.tag = "item";
            bottle.GetComponent<Rigidbody>().useGravity = true;
            bottle.GetComponent<BoxCollider>().enabled = true;
            key.gameObject.SetActive(false);
            //drag.hs= handstate.none;
            //hs=handstate.none;
            
        }
    }

    void Ins_lune2()
    {
        Instantiate(lune2, lune2Point.position, lune2Point.rotation);
       // lune2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
