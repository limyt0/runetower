using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class slender_controll : MonoBehaviour
{
    float after_attack_timeSpan1; //괴물 공격후 경직시간
    float after_attack_timeSpan2; //괴물 공격후 로드까지 시간
    public Transform slender_attacked_sight;//공격 받을 때 맞춰지는 기준 시야
    public Camera main_camera;
    public Image image_attacked;
    public Text testText;
    public Animator slender_ani;
    public Transform maze_ui; 
    Vector3 origin; //초기위치
    public Transform player; //플레이어
    NavMeshAgent navagent;
    // Start is called before the first frame update
    void Start()
    {
        after_attack_timeSpan1 = 0;
        after_attack_timeSpan2 = 0;
        origin = this.transform.position; //맨처음 만들어진 위치
        navagent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        slender_distance_check();

    }


    void slender_distance_check() { //슬렌더 가까이 오는 것 여부 체크
        //두 물체좌표 사이의 거리
        float distance = Vector3.Distance(this.transform.position, player.position);
        //print("거리"+distance);
        if (distance < 80 && distance >= 30)
        {
            testText.text = "무언가가 가까이에 다가오고 있습니다.";
            FadeOut();
            navagent.speed = 6;
            navagent.stoppingDistance = 6;
            navagent.destination = player.position;
            slender_ani.SetTrigger("player_around");
        } else if (distance < 30 && distance >=7) {
            testText.text = "무언가가 매우 가까이에 다가오고 있습니다.";
            
        }
        else if (distance < 7)
        {
            main_camera.transform.LookAt(slender_attacked_sight.transform.position);//공격받으면 괴물을 쳐다봄
            testText.text = "괴물에게 공격 받고 있습니다.";
            slender_ani.SetTrigger("attack_on");
            //print("attacked");
            image_attacked.gameObject.SetActive(true);
            maze_ui.gameObject.SetActive(false);
            attacked_player_stop();
            

            after_attack_timeSpan2 += Time.deltaTime;  // 경과 시간을 계속 등록
            if (after_attack_timeSpan2 > 5.0f)
            {

                after_attack_timeSpan2 = 0;
                

                SceneManager.LoadScene("stage4Scene");
            }

            //괴물한테 공격받아서 못움직임.
            

        }
        else {
            navagent.stoppingDistance = 0;
            navagent.destination = origin;
            slender_ani.SetTrigger("player_far");
            //testText.text = "";
            //멀리있는 상태일때 setTrigger로 멈추는 animation 나오도록 함.
        }
    }


    

    //text 페이드 아웃
    public void FadeOut()
    {
        StartCoroutine(FadeOutCR());
        
    }

    private IEnumerator FadeOutCR()
    {
        float duration = 3f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(3f, 0f, currentTime / duration);
            testText.color = new Color(testText.color.r, testText.color.g, testText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
            
        }
        yield break;
    }

    
    void attacked_player_stop()
    { //괴물한테 닿아서 못 움직이는 상태
        
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        after_attack_timeSpan1 += Time.deltaTime;  // 경과 시간을 계속 등록
        if (after_attack_timeSpan1 > 2.0f)
        {
            
            after_attack_timeSpan1 = 0;
            FadeIN_UI();
        }


    }


    //UI image 페이드 인
    public void FadeIN_UI()
    {
        StartCoroutine(FadeINCR_UI());
    }




    private IEnumerator FadeINCR_UI()
    {
        float duration = 3.0f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 3.0f, currentTime / duration);
            image_attacked.color = new Color(image_attacked.color.r, 0, 0, alpha);
            currentTime += Time.deltaTime;
            yield return null;
            
            
        }
        yield break;
        
    }
}
