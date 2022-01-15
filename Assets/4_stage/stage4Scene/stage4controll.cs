using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
enum rune_state4
{ //현재 룬을 얼마나 켰는지 체크하는 상태
    run_nothing, rune_1, rune_2, rune_3, rune_4
}

public class stage4controll : MonoBehaviour
{
    public Transform goal_Effect;
    public Text n_rune; //
    public Text rune_order_check;
    rune_state4 rus = rune_state4.run_nothing;//초기값으로 룬이 아무것도 안 켜짐.
    RaycastHit objhit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_cheat();
        shoot_Rune();
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
                if (objhit.transform.tag == "rune1" && rus == rune_state4.run_nothing)
                {
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state4.rune_1;
                    n_rune.text = "첫번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune2" && rus == rune_state4.rune_1)
                {
                    //두번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state4.rune_2;
                    n_rune.text = "두번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune3" && rus == rune_state4.rune_2)
                {
                    //세번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state4.rune_3;
                    n_rune.text = "세번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune4" && rus == rune_state4.rune_3)
                {
                    //네번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state4.rune_4;
                    n_rune.text = "네번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                    goal_Effect.gameObject.SetActive(true);

                }
                else if ((objhit.transform.tag == "rune1" && rus >= rune_state4.rune_1) ||
                         (objhit.transform.tag == "rune2" && rus >= rune_state4.rune_2) ||
                         (objhit.transform.tag == "rune3" && rus >= rune_state4.rune_3) ||
                         (objhit.transform.tag == "rune4" && rus >= rune_state4.rune_4))
                {
                    rune_order_check.text = "이미 켜진 룬 입니다.";
                    FadeOut_text(rune_order_check);

                }
                else if (objhit.transform.tag == "rune1" ||
                        objhit.transform.tag == "rune2" ||
                        objhit.transform.tag == "rune3" ||
                        objhit.transform.tag == "rune4")
                {
                    rune_order_check.text = "룬을 순서대로 켜야 합니다.";
                    FadeOut_text(rune_order_check);
                }



            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "goal_effect")
        {
            print("goal");
            SceneManager.LoadScene("EndingScene");
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

    void player_cheat() //치트키
    {

        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            rus = rune_state4.rune_3; //마지막꺼만 맞추면 되게 바꿈.
            print("치트키");
            this.transform.localPosition = new Vector3(-358f, -5.46f, 12.1f);
        }

    }



}
