using UnityEngine;

public enum handstate { none, grabLight, grabKey, grabAxe, grabCandle, grabStone, grabitem, grabfirewood };

public class drag : MonoBehaviour
{
    public static handstate hs = handstate.none;

    RaycastHit objhit;
    public Transform handlight;
    public Transform bullet;
    public GameObject lightball;
    public Transform firepoint;
    public Transform key;
    public Transform Axe;
    public GameObject Vase;
    public GameObject destroyedVersion;
    public Transform candle;
    public Transform stone;
    public GameObject bookUI;
    public Transform book;
    public GameObject table;
    public GameObject firewood;

    public GameObject quill;
    public GameObject jar;
    public GameObject bottle;
    Quaternion Axw_rot;
    Quaternion basic_rot;

    public float distance = 3f;
    int tablecount = 0;
    int keycount = 0;
    bool Vase_break=false;
    

    private GameObject player;
    private Transform lefthand;
    
    void Start()
    {
        player = GameObject.Find("Player");
        lefthand = GameObject.Find("lefthand").transform;
        
        Axw_rot= new Quaternion(-180,-90,90, 0);
        basic_rot = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (key.gameObject.activeSelf == true&&key.tag=="key")
        {
            //열쇠
            if (Vector3.Distance(key.transform.position, player.transform.position) <= distance)
            {
                print("열쇠 가까이에 있음");
                if (Input.GetMouseButtonDown(0) && hs == handstate.none)
                {
                    print("22");

                    hs = handstate.grabKey;
                    key.transform.GetComponent<Rigidbody>().useGravity = false;
                    key.GetComponent<BoxCollider>().isTrigger = true;
                    key.transform.SetParent(lefthand);
                    key.GetComponent<Rigidbody>().isKinematic = true;
                    key.transform.localPosition = Vector3.zero;
                    key.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));


                    //key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    //key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

                }
                if (hs == handstate.grabKey && Input.GetMouseButtonDown(1))
                {
                    print("key");

                    hs = handstate.none;
                    key.transform.GetComponent<Rigidbody>().useGravity = true;
                    key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    key.GetComponent<BoxCollider>().isTrigger = false;
                    key.GetComponent<Rigidbody>().isKinematic = false;
                    key.transform.SetParent(null);
                }

            }
        }
        if(key.gameObject.activeSelf==false&&keycount==0)
        {
            print("chestkey");
            hs = handstate.none;
            keycount = 1;
        }


        //도끼
        
        if (Vector3.Distance(Axe.transform.position, player.transform.position) <= distance)
        {
           
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
              
                hs = handstate.grabAxe;
                Axe.transform.GetComponent<Rigidbody>().useGravity = false;
               
                Axe.transform.SetParent(lefthand);
                Axe.GetComponent<Rigidbody>().isKinematic = true;
                Axe.transform.localPosition = Vector3.zero;
                Axe.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z+0.1f);
                Axe.transform.rotation = new Quaternion(0,0,0,0);
                

                    //transform.Rotate(Vector3(), Space.World);



            }
            if (hs == handstate.grabAxe && Input.GetMouseButtonDown(1))
            {

                hs = handstate.none;
                Axe.transform.GetComponent<Rigidbody>().useGravity = true;
                //key.GetComponent<BoxCollider>().isTrigger = false;
                Axe.GetComponent<Rigidbody>().isKinematic = false;
                Axe.transform.SetParent(null);
            }
          
        }



       
           
            if (hs == handstate.grabAxe && Input.GetMouseButtonDown(0)&&Vector3.Distance(Axe.transform.position, table.transform.position) <= distance && table.transform.tag != "none")
            {
                print("테이블치기");

                print(tablecount);
                tablecount++;

                if (tablecount >= 3)
                {
                    print("테이블부숴짐");
                    table.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                    table.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
                    table.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                    table.transform.GetChild(1).GetComponent<Rigidbody>().useGravity = true;
                    table.transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
                    table.transform.GetChild(2).GetComponent<Rigidbody>().useGravity = true;
                    table.transform.GetChild(3).GetComponent<BoxCollider>().enabled = true;
                    table.transform.GetChild(3).GetComponent<Rigidbody>().useGravity = true;
                    table.transform.GetChild(4).GetComponent<BoxCollider>().enabled = true;
                    table.transform.GetChild(4).GetComponent<Rigidbody>().useGravity = true;
                    table.transform.GetChild(4).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    table.transform.GetChild(0).transform.tag = "firewood";
                    print("책상다리잡을수있음");
                }
            }
            if (Vase_break == false)
            {
                if (hs == handstate.grabAxe && Input.GetMouseButtonDown(0)&&Vector3.Distance(Axe.transform.position, Vase.transform.position) <= distance)
                {
                    print("Vase치기");
                    Instantiate(destroyedVersion, Vase.transform.position, Vase.transform.rotation);
                    key.GetComponent<Rigidbody>().useGravity = true;
                    key.GetComponent<BoxCollider>().enabled = true;
                    Destroy(Vase);
                    Vase_break = true;
                    key.tag = "key";
                }
            }
        
       

        if (Vector3.Distance(firewood.transform.position, player.transform.position) <= distance&&(firewood.tag=="firewood"||firewood.tag=="firewood_fire"))
        {
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
                hs = handstate.grabfirewood;
                firewood.transform.GetComponent<Rigidbody>().useGravity = false;

                firewood.transform.SetParent(lefthand);
                firewood.GetComponent<Rigidbody>().isKinematic = true;
                firewood.transform.position = new Vector3(lefthand.position.x, lefthand.position.y-0.1f, lefthand.position.z+0.2f + 0.1f);
                firewood.transform.rotation = basic_rot;
            }

           
        }
        if (hs == handstate.grabfirewood && Input.GetMouseButtonDown(1))
        {

            hs = handstate.none;
            firewood.transform.GetComponent<Rigidbody>().useGravity = true;
            //key.GetComponent<BoxCollider>().isTrigger = false;
            firewood.GetComponent<Rigidbody>().isKinematic = false;
            firewood.transform.SetParent(null);
        }




        //양초
        if (Vector3.Distance(candle.position, player.transform.position) <= distance)
        {
            if(Input.GetMouseButtonDown(0)&&hs==handstate.none)
            {
                hs=handstate.grabCandle;
                candle.transform.SetParent(lefthand);
                candle.GetComponent<Rigidbody>().isKinematic = true;
                candle.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z);
            }
            if (hs == handstate.grabCandle && Input.GetMouseButtonDown(1))
            {
                hs = handstate.none;
                candle.transform.GetComponent<Rigidbody>().useGravity = true;
                candle.GetComponent<BoxCollider>().enabled = false;
                candle.GetComponent<MeshCollider>().enabled = true;
                candle.GetComponent<Rigidbody>().isKinematic = false;
                candle.transform.SetParent(null);
            }
        }

        //돌
        if (Vector3.Distance(stone.position, player.transform.position) <= distance)
        {
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
                hs = handstate.grabStone;
                stone.transform.SetParent(lefthand);
                stone.GetComponent<MeshCollider>().enabled = false;
                stone.GetComponent<Rigidbody>().isKinematic = true;
                stone.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z);
                print("돌을 잡았다");
            }
            if (hs == handstate.grabStone && Input.GetMouseButtonDown(1))
            {
                hs = handstate.none;
                stone.transform.GetComponent<Rigidbody>().useGravity = true;
                stone.GetComponent<MeshCollider>().enabled = true;
                stone.GetComponent<Rigidbody>().isKinematic = false;
                stone.transform.SetParent(null);
                print("돌을 놓았다");
            }
        }

        //책
        if(Vector3.Distance(book.position, player.transform.position) <= distance)
        {
            shoot_Book();
        }

        //아이템들
        
        if (Vector3.Distance(jar.transform.position, player.transform.position) <= distance&&jar.tag!="none")
        {
            print("jar접근");
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
                print("jar잡기");
                hs = handstate.grabitem;
                jar.transform.SetParent(lefthand);
                jar.GetComponent<BoxCollider>().enabled = false;
                jar.GetComponent<Rigidbody>().isKinematic = true;
                jar.transform.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z);
                jar.transform.rotation = basic_rot;

            }
            if (hs == handstate.grabitem && Input.GetMouseButtonDown(1))
            {
                print("jar 놓기");
                hs = handstate.none;
                jar.transform.GetComponent<Rigidbody>().useGravity = true;
                jar.GetComponent<BoxCollider>().enabled = true;
                jar.GetComponent<Rigidbody>().isKinematic = false;
                jar.transform.SetParent(null);

            }
        }
        if (Vector3.Distance(quill.transform.position, player.transform.position) <= distance&&quill.tag == "item")
        {
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
                print("qill잡기");
                hs = handstate.grabitem;
                quill.transform.SetParent(lefthand);
                quill.GetComponent<BoxCollider>().enabled = false;
                quill.GetComponent<Rigidbody>().isKinematic = true;
                quill.transform.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z);
                quill.transform.rotation = basic_rot;

            }
            if (hs == handstate.grabitem && Input.GetMouseButtonDown(1))
            {
                print("quill놓기");
                hs = handstate.none;
                quill.transform.GetComponent<Rigidbody>().useGravity = true;
                quill.GetComponent<BoxCollider>().enabled = true;
                quill.GetComponent<Rigidbody>().isKinematic = false;
                quill.transform.SetParent(null);

            }
        }
        if (Vector3.Distance(bottle.transform.position, player.transform.position) <= distance && bottle.tag == "item")
        {
           
            if (Input.GetMouseButtonDown(0) && hs == handstate.none)
            {
                print("bottle잡기");
                hs = handstate.grabitem;
                bottle.transform.SetParent(lefthand);
                bottle.GetComponent<BoxCollider>().enabled = false;
                bottle.GetComponent<Rigidbody>().isKinematic = true;
                bottle.transform.position = new Vector3(lefthand.position.x, lefthand.position.y, lefthand.position.z);
                bottle.transform.rotation = basic_rot;
                
            }
            if (hs == handstate.grabitem)
            if (Input.GetMouseButtonDown(1)) {

                print("GetMouseButtonDown1번");
                print("state: "+hs);
            }

            if (hs == handstate.grabitem && Input.GetMouseButtonDown(1))
            {
                print("bottle놓기");
                hs = handstate.none;
                bottle.transform.GetComponent<Rigidbody>().useGravity = true;
                bottle.GetComponent<BoxCollider>().enabled = true;
                bottle.GetComponent<Rigidbody>().isKinematic = false;
                bottle.transform.SetParent(null);

            }

        }
       


    }
    void shoot_Book()
    {
       
        if (Input.GetMouseButtonDown(0) == true)
        {
           
            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(touchray, out objhit) == true)
            {
            
                if (objhit.transform.tag == "book")
                {
            
                    bookUI.SetActive(true);
                }

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            bookUI.SetActive(false);
        }

    }


   
}
