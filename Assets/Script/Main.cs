using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;

public class Main : MonoBehaviour
{
    public GameObject StageUI; //스테이지UI
    public GameObject OptionUI; //옵션UI
    public GameObject QuitUI; //나가기UI
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject box_text;
    public GameObject img_transfer;
    public GameObject img_tranfer_out;
    public Text text_curscore;
    public Camera MainCamera;
    public Transform MainCamera_pos;

    Vector3 vel = Vector3.zero;
    float MoveSpeed = 0.5f;
    bool collection_down = false;
    public bool option_stage_ui_onoff = false;
    // Start is called before the first frame update
    private void Start()
    {
        MasterAudio.PlaySound("0-main_Deck the Halls");
        StartCoroutine(Enable_transfer());
        this.GetComponent<GameData>().Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(option_stage_ui_onoff== true)
            {
                option_stage_ui_onoff = false;
                StageUI.gameObject.SetActive(false);
                OptionUI.gameObject.SetActive(false);
            }
            else if(option_stage_ui_onoff == false)
            { QuitUI.gameObject.SetActive(true);}
        }
    }
    IEnumerator Enable_transfer()
    {
        yield return new WaitForSecondsRealtime(1f);
        img_transfer.SetActive(false);
    }
    private void FixedUpdate()
    {
        if(collection_down)
        {
            MoveCamera();
        }
    }
    void MoveCamera()
    {
        MainCamera.transform.position = Vector3.SmoothDamp(MainCamera.transform.position, MainCamera_pos.transform.position, ref vel, MoveSpeed);
    }
    public void Btn_Start()
    {
        option_stage_ui_onoff = true;
        MasterAudio.PlaySound("Drop");
        StageUI.gameObject.SetActive(true);
    }
    public void Btn_Stage_Exit()
    {
        MasterAudio.PlaySound("Drop");
        box_text.SetActive(false);
        StageUI.gameObject.SetActive(false);
    }
    public void Btn_1()
    {
        MasterAudio.PlaySound("Drop");
        box_text.SetActive(true);
        text_curscore.text = MainData.stage_1_best_score.ToString();
        Stage1.gameObject.SetActive(true);
        Stage2.gameObject.SetActive(false);
        Stage3.gameObject.SetActive(false);
    }
    public void Btn_2()
    {
        MasterAudio.PlaySound("Drop");
        box_text.SetActive(true);
        text_curscore.text = MainData.stage_2_best_score.ToString();
        Stage1.gameObject.SetActive(false);
        Stage2.gameObject.SetActive(true);
        Stage3.gameObject.SetActive(false);
    }
    public void Btn_3()
    {
        MasterAudio.PlaySound("Drop");
        box_text.SetActive(true);
        text_curscore.text = MainData.stage_3_best_score.ToString();
        Stage1.gameObject.SetActive(false);
        Stage2.gameObject.SetActive(false);
        Stage3.gameObject.SetActive(true);
    }
    public void Btn_Stage1()
    {
        MasterAudio.PlaySound("Drop");
        StartCoroutine(TransferScene("Stage_1"));
    }
    public void Btn_Stage2()
    {
        MasterAudio.PlaySound("Drop");
        StartCoroutine(TransferScene("Stage_2"));
    }
    public void Btn_Stage3()
    {
        MasterAudio.PlaySound("Drop");
        StartCoroutine(TransferScene("Stage_3"));
    }
    public void Btn_Collection()
    {
        MasterAudio.PlaySound("10_Door_mixkit-creaky-door-open-195");
        StartCoroutine(TransferCollection("Collection"));
    }
    public void Btn_Option()
    {
        option_stage_ui_onoff = true;
        MasterAudio.PlaySound("Drop");
        OptionUI.gameObject.SetActive(true);
    }
    public void Btn_Option_Exit()
    {
        MasterAudio.PlaySound("Drop");
        this.GetComponent<GameData>().Save();
        OptionUI.gameObject.SetActive(false);
    }

    public void Btn_Quit_Yes()
    {
        MasterAudio.PlaySound("Drop");
        this.GetComponent<GameData>().Save();
        Application.Quit();
    }
    public void Btn_Quit_No()
    {
        MasterAudio.PlaySound("Drop");
        QuitUI.gameObject.SetActive(false);
    }
    IEnumerator TransferCollection(string SceneName)
    {
        MasterAudio.FadeOutOldBusVoices("BGM", 0f, 1.5f);
        collection_down = true;
        img_transfer.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        img_tranfer_out.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneName);
    }
    IEnumerator TransferScene(string SceneName)
    {
        MasterAudio.FadeOutOldBusVoices("BGM", 0f, 1f);
        img_transfer.SetActive(true);
        img_tranfer_out.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneName);
    }
}
