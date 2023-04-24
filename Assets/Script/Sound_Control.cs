using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using UnityEngine.UI;

public class Sound_Control : MonoBehaviour
{
    public Slider sound_bgm;
    public Slider sound_sfx;

    private void Start()
    {
        SetSoundVolume();
    }
    void SetSoundVolume()
    {
        MainData.sound_volume_bgm = sound_bgm.value;
        MainData.sound_volume_sfx = sound_sfx.value;
    }
    void Update()
    {
        MasterAudio.SetBusVolumeByName("BGM", sound_bgm.value);
        MainData.sound_volume_bgm = sound_bgm.value;
        MasterAudio.SetBusVolumeByName("SFX", sound_sfx.value);
        MainData.sound_volume_sfx = sound_sfx.value;
    }
}
