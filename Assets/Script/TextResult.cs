using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;
public class TextResult : MonoBehaviour
{
    public static bool ResultComplete = false;

    public Image img_Result;
    public Text text_Result;
    public GameObject btn_home;
    public GameObject btn_next;
    public GameObject btn_retry;
    public Sprite[] over_sprite;

    float time;
    float speed = 0.5f;
    private void Start()
    {
        ResultComplete = true;
        if (StageManager.state_Result == 1)
        {
            Invoke("PlayDefeat", 1.5f);
            img_Result.sprite = over_sprite[1];
            btn_retry.SetActive(true);
        }
        else if(StageManager.state_Result == 2)
        {
            Invoke("PlayComplete",1.5f); 
            img_Result.sprite = over_sprite[0];
            if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
            {
                btn_next.SetActive(true);
                btn_next.GetComponent<Button>().interactable = false;
            }
            else
            {
                btn_next.SetActive(true);
            }
        }
        check_clear();

        this.GetComponent<GameData>().Save();
    }
    void PlayComplete()
    {
        MasterAudio.PlaySound("mixkit-game-level-completed-2059");
    }
    void PlayDefeat()
    {
        MasterAudio.PlaySound("mixkit-sad-game-over-trombone-471");
    }
    void check_clear()
    {
        if (SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            MainData.stage_clear_check[0] = true;
            MainData.stage_clear_checkREV[0] = false;
            MainData.stage_clear_friends[0] = true;

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            MainData.stage_clear_check[1] = true;
            MainData.stage_clear_checkREV[1] = false;
            MainData.stage_clear_friends[1] = true;

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            MainData.stage_clear_check[2] = true;
            MainData.stage_clear_checkREV[2] = false;
            MainData.stage_clear_friends[2] = true;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        time += Time.deltaTime * speed;

        text_Result.text = string.Format("{0:N0}",Mathf.Lerp(0f, MainData.stage_1_score, time));
    }
    
}
