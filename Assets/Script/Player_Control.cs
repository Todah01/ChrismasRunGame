using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;
public enum PlayerState { none, run, jump, d_jump, slide, damaged, death, clear, over }

public class Player_Control : MonoBehaviour
{
    public Slider player_HP;
    public CapsuleCollider Stand_col;
    public CapsuleCollider Slide_col;

    public static PlayerState p_state = PlayerState.none;

    Rigidbody p_rg;
    Animator p_anim;

    //sint JumpCnt = 0;
    float JumpPower = 305f;
    float HP_Recovery_value = 0.11f;
    bool IsReaction = false;
    bool IsSliding = false;
    bool IsDamaged = false;

    public Transform effectpos_Player;
    public Transform effectpos_PlayerHead;
    public Transform Jump_eff;
    public Transform Slide_eff;
    public Transform HP_eff;
    public Transform Coin_eff;



    private void Awake()
    {
        init_Data();
    }
    void init_Data()
    {
        player_HP.value = 1f;
        p_rg = this.GetComponent<Rigidbody>();
        p_anim = this.GetComponent<Animator>();

        if(SceneManager.GetActiveScene().name.ToString() == "Stage_1")
        {
            MainData.loss_hp_amount = 0.03f;
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_2")
        {

            MainData.loss_hp_amount = 0.04f;
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "Stage_3")
        {
            MainData.loss_hp_amount = 0.05f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        p_state = PlayerState.run;
        p_anim.SetTrigger("IsRun");
    }

    // Update is called once per frame
    void Update()
    {
        loss_HP();
        check_HP();
        check_Result();
    }
    void check_Result()
    {
        if ((TextResult.ResultComplete == true) && (IsReaction == false))
        {
            IsReaction = true;
            if (StageManager.state_Result == 1)
            {
                p_anim.SetTrigger("IsLose");
                p_state = PlayerState.over;
            }
            else if(StageManager.state_Result == 2)
            {
                p_state = PlayerState.clear;
                p_anim.SetTrigger("IsWin");
            }
        }
    }
    
    void loss_HP()
    {
        player_HP.value -= Time.deltaTime * MainData.loss_hp_amount;
    }
    void check_HP()
    {
        if (player_HP.value <= 0)
        {

            StageManager.g_state = GameState.over;
            p_state = PlayerState.death;
            p_anim.SetTrigger("IsWalk");
            p_anim.SetTrigger("IsDie");
            Invoke("PlayAnim", 2f);
        }
    }
    void PlayAnim()
    {
        p_anim.SetTrigger("IsIdle"); 
    }
    public void JumpClick()
    {
        if(p_state != PlayerState.death)
        {
            if(p_state == PlayerState.run)
            {
                MasterAudio.PlaySound("4_Jump_Score_swap_1_a");
                p_anim.SetTrigger("IsJump");
                Jump();
            }
            else if (p_state == PlayerState.jump)
            {
                MasterAudio.PlaySound("4_Jump_Score_swap_1_a");
                p_anim.SetTrigger("IsJump");
                DoubleJump();
            }
        }
    }
    void Run()
    {
        p_state = PlayerState.run;
    }
    void Jump()
    {
        Instantiate(Jump_eff, effectpos_Player.transform.position, effectpos_Player.transform.rotation);
        p_anim.SetBool("IsWalk", false);
        p_state = PlayerState.jump;
        p_rg.velocity = Vector3.zero;
        p_rg.AddForce(Vector3.up * JumpPower);
    }
    void DoubleJump()
    {
        Instantiate(Jump_eff, effectpos_Player.transform.position, effectpos_Player.transform.rotation);
        p_state = PlayerState.d_jump;
        p_rg.velocity = Vector3.zero;
        p_rg.AddForce(Vector3.up * JumpPower);
    }
    public void Slide()
    {
        Instantiate(Slide_eff, effectpos_Player.transform.position, effectpos_Player.transform.rotation);
        p_state = PlayerState.slide;
        IsSliding = true;
        p_anim.SetBool("IsWalk", false);
        p_anim.SetTrigger("IsSit");
        Slide_col.enabled = true;
        Stand_col.enabled = false;
    }
    public void SildeCancel()
    {
        p_state = PlayerState.run;
        IsSliding = false;
        Stand_col.enabled = true;
        Slide_col.enabled = false;
    }
    void Damaged()
    {
        IsDamaged = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HP")
        {
            MasterAudio.PlaySound("6_Hp_magic_01");
            Instantiate(HP_eff, effectpos_PlayerHead.transform.position, effectpos_PlayerHead.transform.rotation);
            Destroy(other.gameObject);
            player_HP.value += HP_Recovery_value;
        }
        else if(other.gameObject.tag == "Coin")
        {
            MasterAudio.PlaySound("5_coin_1_a");
            Instantiate(Coin_eff, effectpos_PlayerHead.transform.position, effectpos_PlayerHead.transform.rotation);
            Destroy(other.gameObject);
            MainData.stage_1_score++;
            MainData.stage_2_score++;
            MainData.stage_3_score++;
        }
        else if(other.gameObject.tag == "OverZone")
        {
            StageManager.g_state = GameState.over;
            p_state = PlayerState.death;
            p_anim.SetTrigger("IsWalk");
            p_anim.SetTrigger("IsDie");
            Invoke("PlayAnim", 2.5f);
        }
        else if(other.gameObject.tag == "Destination")
        {
            StageManager.g_state = GameState.clear;
            p_anim.SetTrigger("IsWalk");
            p_anim.SetTrigger("IsClear");
            Invoke("PlayAnim", 2.5f);
        }
        else if (other.gameObject.tag == "Gimmick_1")
        {
            if (IsDamaged == false)
            {
                p_state = PlayerState.damaged;
                //Debug.Log("Damaged");
                IsDamaged = true;
                player_HP.value -= 0.1f;
                p_anim.SetTrigger("IsDamage");
                MasterAudio.PlaySound("damaged");
                Invoke("Damaged", 0.5f);
            }
        }
        else if(other.gameObject.tag == "Fire")
        {
            StageManager.g_state = GameState.over;
            p_state = PlayerState.death;
            p_anim.SetTrigger("IsWalk");
            p_anim.SetTrigger("IsDie");
            Invoke("PlayAnim", 2.5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Box")
        {
            if(IsSliding == false)
            {
                p_anim.SetBool("IsWalk", true);
            }
        }

        if(p_state != PlayerState.run && p_state != PlayerState.death)
        {
            Run();
        }
    }
}
