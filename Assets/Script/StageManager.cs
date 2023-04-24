using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;
public enum GameState { none, play, stop, over, clear, result }

public class StageManager : MonoBehaviour
{
    public static GameState g_state = GameState.none;
    public static int state_Result = 0;

    public GameObject camera_GameEnd;
    public GameObject player;
    public GameObject ui_over;
    public GameObject ui_clear;
    public GameObject ui_play;
    public GameObject ui_result;
    public GameObject ui_pause;
    public GameObject ui_cover;
    public GameObject ui_start;
    public GameObject transfer_ui;
    public GameObject transfer_out;
    public Transform player_End;
    public Transform player_Victory;
    public Text text_Score;
    public Text text_ready;
    public Text text_start;
    public Text text_stage;
    public Text text_stage_num;

    bool IsEnd = false;
    private void Awake()
    {
        init_Data();
        Wait_3f();
    }
    void init_Data()
    {
        state_Result = 0;
        g_state = GameState.play;

        MainData.stage_1_score = 0;
        MainData.stage_2_score = 0;
        MainData.stage_3_score = 0;
        if (SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            MainData.block_move_speed = 3.8f;

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            MainData.block_move_speed = 4.3f;

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            MainData.block_move_speed = 5f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enable_transfer());
        StartCoroutine(Enable_Stage());
        StartCoroutine(Enable_Stage_num());
        StartCoroutine(Enable_ui_Stage());
        StartCoroutine(Enable_ready());
        StartCoroutine(Enable_start());
    }
    IEnumerator Enable_transfer()
    {
        yield return new WaitForSecondsRealtime(1f);
        transfer_ui.SetActive(false);
    }
    IEnumerator Enable_Stage()
    {
        yield return new WaitForSecondsRealtime(1f);
        text_stage.gameObject.SetActive(true);
        
    }
    IEnumerator Enable_Stage_num()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        text_stage_num.gameObject.SetActive(true);
    }
    IEnumerator Enable_ui_Stage()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        text_stage.gameObject.SetActive(false);
        text_stage_num.gameObject.SetActive(false);
        text_ready.gameObject.SetActive(true);
    }
    IEnumerator Enable_ready()
    {
        yield return new WaitForSecondsRealtime(4f);
        text_ready.gameObject.SetActive(false);
        text_start.gameObject.SetActive(true);
    }
    IEnumerator Enable_start()
    {
        yield return new WaitForSecondsRealtime(5f);
        ui_start.SetActive(false);
    }
    void Wait_3f()
    {
        Time.timeScale = 0f;
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSecondsRealtime(5f);
        ui_cover.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            MasterAudio.PlaySound("1_St1_JumpyGame");

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            MasterAudio.PlaySound("2_st2_bgm");

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            MasterAudio.PlaySound("3_St3_BGM_01");
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkScore();
        GamePlay();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(g_state == GameState.play)
            {
                g_state = GameState.stop;
            }
            else if(g_state == GameState.stop)
            {
                UnPause();
            }
            
        }
    }
    void Cry()
    {
        MasterAudio.PlaySound("cry");
    }
    void GamePlay()
    {
        switch (g_state)
        {
            case GameState.play:
                break;
            case GameState.stop:
                Pause();
                break;
            case GameState.over:
                if(g_state == GameState.over && IsEnd == false)
                {
                    MasterAudio.StopBus("BGM");
                    MasterAudio.PlaySound("7_Over_mixkit-little-piano-game-over-1944");
                    Invoke("Cry", 0.75f);
                    state_Result = 1;
                    GameEnd(ui_over);
                }
                break;
            case GameState.clear:
                if(g_state == GameState.clear && IsEnd == false)
                {
                    MasterAudio.StopBus("BGM");
                    MasterAudio.PlaySound("8_Clear_mixkit-game-bonus-reached-2065");
                    state_Result = 2;
                    GameEnd(ui_clear);
                }
                break;
            case GameState.result:
                if (SceneManager.GetActiveScene().name.ToString() == "Stage_1")
                {
                    if (int.Parse(text_Score.text) > MainData.stage_1_best_score)
                    {
                        MainData.stage_1_best_score = int.Parse(text_Score.text);
                    }

                }
                else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
                {
                    if (int.Parse(text_Score.text) > MainData.stage_2_best_score)
                    {
                        MainData.stage_2_best_score = int.Parse(text_Score.text);
                    }

                }
                else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
                {
                    if (int.Parse(text_Score.text) > MainData.stage_3_best_score)
                    {
                        MainData.stage_3_best_score = int.Parse(text_Score.text);
                    }
                }
                break;
        }
    }

    void GameEnd(GameObject obj)
    {
        IsEnd = true;
        g_state = GameState.result;
        ui_play.SetActive(false);
        obj.SetActive(true);
        SceneStop(obj);
    }
    void SceneStop(GameObject ui)
    {
        MainData.block_move_speed = 0f;
        StartCoroutine(Active_Obj(ui));
    }
    IEnumerator Active_Obj(GameObject obj)
    {
        yield return new WaitForSeconds(2.5f);
        if (obj.name.ToString() == "ui_Over")
        {
            player.transform.position = player_End.position;
            player.transform.rotation = player_End.rotation;
            player.transform.localScale = new Vector3(8, 8, 7.5f);
            obj.SetActive(false);
            camera_GameEnd.SetActive(true);
            ui_result.SetActive(true);
        }
        else if(obj.name.ToString() == "ui_Clear")
        {
            player.transform.position = player_Victory.position;
            player.transform.rotation = player_Victory.rotation;
            player.transform.localScale = new Vector3(8, 8, 7.5f);
            obj.SetActive(false);
            camera_GameEnd.SetActive(true);
            ui_result.SetActive(true);
        }
        
    }
    void checkScore()
    {
        if(SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            text_Score.text = string.Format("{0:N0}", MainData.stage_1_score);

        } else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            text_Score.text = string.Format("{0:N0}", MainData.stage_2_score);

        } else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            text_Score.text = string.Format("{0:N0}", MainData.stage_3_score);
        }

    }
    public void Pause()
    {
        //MasterAudio.PlaySound("Drop");
        g_state = GameState.stop;
        MasterAudio.StopBus("BGM");
        ui_pause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        g_state = GameState.play;
        MasterAudio.PlaySound("Drop");
        if (SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            MasterAudio.PlaySound("1_St1_JumpyGame");

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            MasterAudio.PlaySound("2_St2_BGM_02");

        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            MasterAudio.PlaySound("3_St3_BGM_01");
        }
        ui_pause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BackHome()
    {
        MasterAudio.PlaySound("Drop");
        StartCoroutine(TransferScene("Main"));
    }
    public void Retry()
    {
        MasterAudio.PlaySound("Drop");
        StartCoroutine(TransferScene(SceneManager.GetActiveScene().name.ToString()));
    }
    IEnumerator TransferScene(string SceneName)
    {
        transfer_ui.SetActive(true);
        transfer_out.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneName);
    }
    public void NextStage()
    {
        if(SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            StartCoroutine(TransferScene("Stage_2"));
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {
            StartCoroutine(TransferScene("Stage_3"));
        }
    }
}
