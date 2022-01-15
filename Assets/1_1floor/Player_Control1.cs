using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
enum rune_state1
{ //현재 룬을 얼마나 켰는지 체크하는 상태
    run_nothing, rune_1, rune_2, rune_3, rune_4
}

enum jump_state1 { jump, ground }



public class Player_Control1 : MonoBehaviour
{
    AudioSource walk_sound;
    public Text Guide1;
    public Text Guide2;
    public Text n_rune; //
    public Text rune_order_check;
    public Camera maincamera;
    public GameObject player_head;
    public GameObject settings;
    bool pause = false;
    bool door_bool = false;
    float Mov_x, Mov_z; //wasd
    float Movrate; //wasd 속도
    float Rotrate; //회전속도
    float MouseX; //마우스 좌우
    rune_state1 rus = rune_state1.run_nothing;
    public Transform Effect;
    public Transform Effect1;
    public Transform Effect2;
    public Transform Effect3;
    public Transform goal_Effect;
    bool state_door = false;
    float door_L_angle = -90;
    float door_R_angle = 90;
    public Transform Door_L;
    public Transform Door_R;
    bool guide_check = true;
    //public float Max_rotation = 90; //머리 회전 최대각
    //public float Min_rotation = 0f;//머리 회전 최소각
    public float upDownRange = 90; //머리 회전 각범위+-
    private float verticalRotation = 0f;//머리 기본 각도
    
    float jump_power = 500f; //점프 정도
    RaycastHit objhit;

    jump_state1 js = jump_state1.ground;

    private void Start()
    {
        walk_sound = this.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(door_bool == true)
        {
            Door_L1();
            Door_R1();
            Guide1.text = "룬 세개를 무지개색 순서대로 맞추세요.";
        }

        player_jump();
        shoot_Rune();
        mouse_screen_lock();
        exitmenu();
        

    }

    void Door_L1()
    {

        if (state_door == true && door_L_angle >= -210)
        {

            door_L_angle -= Time.deltaTime * 30;
            Door_L.transform.parent.rotation = Quaternion.Euler(0, door_L_angle, 0);
            //print(door_angle);
        }


    }
    void Door_R1()
    {


        if (state_door == true && door_R_angle <= 210)
        {
            door_R_angle += Time.deltaTime * 30;
            Door_R.transform.parent.rotation = Quaternion.Euler(0, door_R_angle, 0);

        }

    }
    void exitmenu()  //esc 누르면 나오는 창
    {
        if (Input.GetKeyDown("escape"))
            pause = !pause;
        if (pause == true)
        {
            settings.SetActive(true);

        }
        if (pause == false)
        {
            settings.SetActive(false);

        }

    }
    private void FixedUpdate()
    {
        move_controll();
    }
    void move_controll() {
        //player 이동
        Mov_z = Input.GetAxis("Horizontal");
        Mov_x = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        //MouseY = -Input.GetAxis("Mouse Y");

        Rotrate = 350f * Time.deltaTime;
        Movrate = 10f * Time.deltaTime;

        if (Mov_x == 0 && Mov_z == 0)
        {
            print("mov 0");
            walk_sound.Stop();
        }
        else {
            if (!walk_sound.isPlaying)
            {
                walk_sound.Play();
            }
        }

        if (pause == false)
        {
            transform.Translate(Vector3.forward * Mov_x * Movrate );
        transform.Translate(Vector3.right * Mov_z * Movrate);
        transform.Rotate(Vector3.up * Rotrate * MouseX);
        head_controll();
        
        }
    }

    void head_controll()
    {
        
        
        verticalRotation -= Input.GetAxis("Mouse Y")*5f;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange-20);
        player_head.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        

        //if ((player_head.transform.rotation.x < Max_rotation && player_head.transform.rotation.x > Min_rotation)) {
        //머리만 따로 회전시키기
        // print("rotationtrue"+ player_head.transform.rotation.x);
        //Vector3 v3 = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
        //player_head.transform.Rotate(Rotrate * v3);
        //}


    }

    void player_jump() {
        
        if (Input.GetKeyDown(KeyCode.Space) == true  && js == jump_state1.ground)
        {
            print("jump");
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jump_power);
            js = jump_state1.jump;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (js != jump_state1.ground) js = jump_state1.ground;
        print("지금상태는: "+ js);


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Effect1")
        {
            print("1");
            Effect1.gameObject.SetActive(false);
            Effect2.gameObject.SetActive(true);
            Guide2.text = "SPACE : 점프";
        }
        if(other.transform.tag == "Effect2")
        {
            print("2");
            Effect2.gameObject.SetActive(false);
            Effect3.gameObject.SetActive(true);
            GuideOut_text(Guide2);
        }
        if(other.transform.tag == "Effect3")
        {
            print("3");
            Effect3.gameObject.SetActive(false);
            state_door = true;
            door_bool = true;

           
            
            
            

        }
        if (other.transform.tag == "goal_effect")
        {
            print("goal");
            SceneManager.LoadScene("TowerRoom");
        }
    }


    void shoot_Rune()
    {
        //룬 순서대로 켜기
        if (Input.GetMouseButtonDown(0) == true)
        {
            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(touchray, out objhit) == true)
            {
                //첫번째 룬 켜기
                if (objhit.transform.tag == "rune1" && rus == rune_state1.run_nothing)
                {
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state1.rune_1;
                    n_rune.text = "첫번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune2" && rus == rune_state1.rune_1)
                {
                    //두번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state1.rune_2;
                    n_rune.text = "두번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune3" && rus == rune_state1.rune_2)
                {
                    //세번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state1.rune_3;
                    n_rune.text = "세번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                    Effect.gameObject.SetActive(true);
                    GuideOut_text(Guide1);
                    goal_Effect.gameObject.SetActive(true);
                }
                else if ((objhit.transform.tag == "rune1" && rus >= rune_state1.rune_1) ||
                         (objhit.transform.tag == "rune2" && rus >= rune_state1.rune_2) ||
                         (objhit.transform.tag == "rune3" && rus >= rune_state1.rune_3))
                {
                    rune_order_check.text = "이미 켜진 룬 입니다.";
                    FadeOut_text(rune_order_check);

                }
                else if (objhit.transform.tag == "rune1" ||
                        objhit.transform.tag == "rune2" ||
                        objhit.transform.tag == "rune3")
                {
                    rune_order_check.text = "룬을 순서대로 켜야 합니다.";
                    FadeOut_text(rune_order_check);
                }



            }

        }

    }


    public void FadeOut_text(Text text)
    {
        StartCoroutine(FadeOutCR(text));
    }

    private IEnumerator FadeOutCR(Text text)
    {
        float duration = 3f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(3f, 0f, currentTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void GuideOut_text(Text text)
    {
        StartCoroutine(GuideOutCR(text));
    }

    private IEnumerator GuideOutCR(Text text)
    {
        float duration = 0.5f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(3f, 0f, currentTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }




    void mouse_screen_lock()
    {
        //화면의 마우스를 고정
        if (pause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (pause == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene("floor1F");
    }
    public void Game_Exit()
    {
        Application.Quit();
    }
}
