using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum rune_state2
{ //현재 룬을 얼마나 켰는지 체크하는 상태
    run_nothing, rune_1, rune_2, rune_3, rune_4
}

public class Stage2Logic : MonoBehaviour
{
    public Text n_rune; //
    public Text rune_order_check;
    public Transform F2portal;
    rune_state2 rus = rune_state2.run_nothing;
    RaycastHit objhit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                if (objhit.transform.tag == "rune1" && rus == rune_state2.run_nothing)
                {
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state2.rune_1;
                    n_rune.text = "첫번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune2" && rus == rune_state2.rune_1)
                {
                    //두번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state2.rune_2;
                    n_rune.text = "두번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                }
                else if (objhit.transform.tag == "rune3" && rus == rune_state2.rune_2)
                {
                    //세번째 룬 켜기
                    objhit.transform.GetChild(0).gameObject.SetActive(true);
                    rus = rune_state2.rune_3;
                    n_rune.text = "세번째 룬을 켰습니다.";
                    FadeOut_text(n_rune);
                    F2portal.gameObject.SetActive(true);
                }
                else if ((objhit.transform.tag == "rune1" && rus >= rune_state2.rune_1) ||
                         (objhit.transform.tag == "rune2" && rus >= rune_state2.rune_2) ||
                         (objhit.transform.tag == "rune3" && rus >= rune_state2.rune_3))
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



}
