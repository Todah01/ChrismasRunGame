using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;

public class Collection : MonoBehaviour
{
    public GameObject[] Ornament;
    public GameObject[] GiftBox;
    public GameObject[] UnGiftBox;
    public GameObject[] friends;
    public GameObject transfer_ui;
    public GameObject transfer_in;
    public GameObject transfer_out;

    // Start is called before the first frame update
    private void Awake()
    {
        if (MainData.stage_clear_check[4])
        {
            MainData.stage_clear_check[5] = true;
            MainData.stage_clear_checkREV[5] = false;
        }
        if (MainData.stage_1_best_score >= 50)
        {
            MainData.stage_clear_check[3] = true;
            MainData.stage_clear_checkREV[3] = false;
        }
        if (MainData.stage_3_best_score >= 45)
        {
            MainData.stage_clear_check[4] = true;
            MainData.stage_clear_checkREV[4] = false;
        }
    }
    void Start()
    {
        StartCoroutine(Enable_transfer());
        for (int i = 0; i < 6; i++)
        {
            active_Ornament(i, MainData.stage_clear_check[i]);
            active_GiftBox(i, MainData.stage_clear_checkREV[i]);
        }

        for (int j = 0; j < 3; j++)
        {
            active_friends(j, MainData.stage_clear_friends[j]);
        }
    }
    IEnumerator Enable_transfer()
    {
        yield return new WaitForSecondsRealtime(1f);
        MasterAudio.PlaySound("collection");
        MasterAudio.PlaySound("bonfire");
        transfer_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transfer_ui.SetActive(true);
            transfer_out.SetActive(true);
            StartCoroutine(TransferScene("Main"));
            IEnumerator TransferScene(string scene)
            {
                MasterAudio.FadeOutOldBusVoices("BGM", 0f, 1f);
                yield return new WaitForSecondsRealtime(1f);
                SceneManager.LoadScene(scene);
            }
        }
    }
    public void Btn_Main()
    {
        MasterAudio.PlaySound("Drop");
        transfer_ui.SetActive(true);
        transfer_out.SetActive(true);
        StartCoroutine(TransferScene("Main"));
    }
    IEnumerator TransferScene(string scene)
    {
        MasterAudio.FadeOutOldBusVoices("BGM", 0f, 1f);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(scene);
    }
   
    void active_Ornament(int index, bool check)
    {
        UnGiftBox[index].SetActive(check);
        Ornament[index].SetActive(check);
    }
    void active_GiftBox(int index, bool check)
    {
        GiftBox[index].SetActive(check);
    }
    void active_friends(int index, bool check)
    {
        friends[index].SetActive(check);
    }

}
