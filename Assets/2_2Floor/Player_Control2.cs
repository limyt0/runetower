using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


enum jump_state2 { jump, ground }



public class Player_Control2 : MonoBehaviour
{
    AudioSource walk_sound;
    public Camera maincamera;
    public GameObject player_head;
    public GameObject settings;
    bool pause = false;
    float Mov_x, Mov_z; //wasd
    float Movrate; //wasd 속도
    float Rotrate; //회전속도
    float MouseX; //마우스 좌우
    
    
                  //public float Max_rotation = 90; //머리 회전 최대각
                  //public float Min_rotation = 0f;//머리 회전 최소각
    float upDownRange = 90; //머리 회전 각범위+-
    private float verticalRotation = 0f;//머리 기본 각도
    
    float jump_power = 500f; //점프 정도
    RaycastHit objhit;

    jump_state2 js = jump_state2.ground;

    private void Start()
    {
        walk_sound = this.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

        player_jump();
        mouse_screen_lock();
        exitmenu();
        

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
        else
        {
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
        
        if (Input.GetKeyDown(KeyCode.Space) == true  && js == jump_state2.ground)
        {
            print("jump");
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jump_power);
            js = jump_state2.jump;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (js != jump_state2.ground) js = jump_state2.ground;
        print("지금상태는: "+ js);
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
        SceneManager.LoadScene("TowerRoom");
    }
    public void Game_Exit()
    {
        Application.Quit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "goal_effect")
        {
            print("goal");
            SceneManager.LoadScene("Stage3");
        }
    }

}

