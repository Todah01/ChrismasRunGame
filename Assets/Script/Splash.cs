using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;
public class Splash : MonoBehaviour
{
    float SplashTime = 0;
    bool sound = false;
    private void Start()
    {
        
    }

    void Update()
    {
        SplashTime += Time.deltaTime;
        //print(string.Format("{0:f0}", SplashTime));

        if ((SplashTime >= 2f) && (sound == false))
        {
            sound = true;
            MasterAudio.PlaySound("Ma Sfx Sys Enter 5 by Dneproman Id-334890");
        }
        if(SplashTime >=5f)
        {
            SplashTime = 0;
            SceneManager.LoadScene("Title");
        }
    }
}
