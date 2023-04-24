using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;
using DG.Tweening;

public class Title : MonoBehaviour
{
    public GameObject Word_title;
    public GameObject Tap;
    public GameObject santa;
    public GameObject Txt_tap;
    public ParticleSystem snow_1;
    public ParticleSystem snow_2;
    public ParticleSystem snow_3;
    public Transform santa_pos;

    Animator santa_anim;
    DOTweenAnimation t_anim;
    DOTweenAnimation w_anim;

    float MoveSpeed = 1f;
    bool isArrive = false;
    bool isDone = false;
    private void Awake()
    {
        santa_anim = santa.GetComponent<Animator>();
    }
    private void Start()
    {
        w_anim = Word_title.GetComponent<DOTweenAnimation>();
        t_anim = Txt_tap.GetComponent<DOTweenAnimation>();
        MasterAudio.PlaySound("3.Running");
        santa_anim.SetTrigger("IsRun");
    }
    private void Update()
    {
        SantaMove();
        CheckPos();
    }
    void CheckPos()
    {
        if((santa.transform.position.x >= -2.5f) && (isDone == false))
        {
            isArrive = true;
            isDone = true;
        }
        if(isArrive)
        {
            Invoke("BackGroundMusic", 0.75f);
            isArrive = false;
            MasterAudio.StopBus("SFX");
            MasterAudio.PlaySound("sit");
            santa_anim.SetTrigger("IsIdle");
            santa_anim.SetTrigger("IsDown");
            StartCoroutine(TitleDown());
        }
    }
    void BackGroundMusic()
    {
        MasterAudio.PlaySound("Xmas_Christmas_Song_Loop");
    }
    IEnumerator TitleDown()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("TitleDown");
        w_anim.DOPlay();
        StartCoroutine(TapOn());
    }
    IEnumerator TapOn()
    {
        yield return new WaitForSeconds(1f);
        Tap.gameObject.SetActive(true);
    }
    void SantaMove()
    {
        santa.transform.position = Vector3.MoveTowards(santa.transform.position, santa_pos.position, Time.deltaTime * MoveSpeed);
    }
    public void Btn_Tap()
    {
        t_anim.DOKill();
        Txt_tap.SetActive(false);
        MasterAudio.FadeOutOldBusVoices("BGM", 0f, 1.5f);
        Invoke("NextScene", 0.5f);
    }
    void NextScene()
    {
        SceneManager.LoadScene("Main");
    }
}
