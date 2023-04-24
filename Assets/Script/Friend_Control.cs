using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_Control : MonoBehaviour
{
    Animator f_anim;
    // Start is called before the first frame update
    private void Awake()
    {
        f_anim = this.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check_clear();
    }
    void check_clear()
    {
        if(StageManager.g_state == GameState.result)
        {
            Debug.Log("ok");
            f_anim.SetTrigger("IsRun");
            f_anim.SetTrigger("IsJump");
        }
    }
}
