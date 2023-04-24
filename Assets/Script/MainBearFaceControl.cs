using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
public class MainBearFaceControl : MonoBehaviour
{
    public GameObject bear;
    public Material[] materials;
    public SkinnedMeshRenderer b_rend;

    Animator b_anim;

    int cnt_face;
    bool set_face = false;
    // Start is called before the first frame update
    void Start()
    {
        cnt_face = 0;
        b_anim = bear.GetComponent<Animator>();
        b_rend = b_rend.GetComponent<SkinnedMeshRenderer>();
        b_rend.enabled = true;
    }

    // Update is called once per frame
    public void FaceChange()
    {
        if (set_face) return;
        cnt_face++;
        switch (cnt_face)
        {
            case 1:
                MasterAudio.PlaySound("merssc");
                FaceFunction("IsSmile",1);
                break;
            case 2:
                MasterAudio.PlaySound("embrass");
                FaceFunction("IsWeird",2);
                break;
            case 3:
                MasterAudio.PlaySound("mad");
                FaceFunction("IsMad",3);
                break;
        }
        
    }
    void FaceFunction(string anim, int i)
    {
        set_face = true;
        b_anim.SetTrigger(anim);
        b_rend.sharedMaterial = materials[i];
        StartCoroutine(ResetFace());
    }
    IEnumerator ResetFace()
    {
        b_anim.SetTrigger("IsIdle");
        if (cnt_face >= 3)
        {
            cnt_face = 0;
        }
        yield return new WaitForSeconds(2f);
        MasterAudio.StopBus("SFX");
        b_rend.sharedMaterial = materials[0];
        set_face = false;
    }
}
