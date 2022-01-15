using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float i = 0f;

        if (this.transform.position.y <= 50)
        {
            i += 1f * Time.deltaTime;
            this.transform.Translate(new Vector3(0, i, 0));
        }
        if(this.transform.position.y==50)
        {
            Destroy(this.gameObject);
        }

       
    }
}
