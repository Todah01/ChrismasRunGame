using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainData
{
    public static float sound_volume_sfx;
    public static float sound_volume_bgm;
    public static float loss_hp_amount;
    public static float block_move_speed;
    public static int stage_1_best_score;
    public static int stage_1_score;
    public static int stage_2_best_score;
    public static int stage_2_score;
    public static int stage_3_best_score;
    public static int stage_3_score;

    public static bool[] stage_clear_check = new bool[6] { false, false, false, false, false, false };
    public static bool[] stage_clear_checkREV = new bool[6] { true, true, true, true, true, true };
    public static bool[] stage_clear_friends = new bool[3] { false, false, false };
}

[Serializable]
public class SaveData
{
    public float sound_volume_sfx;
    public float sound_volume_bgm;

    public int stage_1_best;
    public int stage_2_best;
    public int stage_3_best;

    public bool[] clear_check = new bool[6] { false, false, false, false, false, false };
    public bool[] clear_checkREV = new bool[6] { true, true, true, true, true, true };
    public bool[] clear_friends = new bool[3] { false, false, false };
}

public class GameData : MonoBehaviour
{
    SaveData s_data;
    string path;
    private void Awake()
    {
        path = Application.persistentDataPath + "/save.dat";
    }
    public void Save()
    {
        print("SaveStart");
        s_data = new SaveData();
        s_data.sound_volume_bgm = MainData.sound_volume_bgm;
        s_data.sound_volume_sfx = MainData.sound_volume_sfx;
        s_data.stage_1_best = MainData.stage_1_best_score;
        s_data.stage_2_best = MainData.stage_2_best_score;
        s_data.stage_3_best = MainData.stage_3_best_score;
        for (int i = 0; i < 6; i++)
        {
            s_data.clear_check[i] = MainData.stage_clear_check[i];
        }
        for (int i = 0; i < 6; i++)
        {
            s_data.clear_checkREV[i] = MainData.stage_clear_checkREV[i];
        }
        for (int i = 0; i < 3; i++)
        {
            s_data.clear_friends[i] = MainData.stage_clear_friends[i];
        }
        DataManager.BinarySerialize<SaveData>(s_data, path);
        print("SaveSuccess");
    }
    public void Load()
    {
        print("LoadStart");
        if (!File.Exists(path)) return;
        s_data = DataManager.BinaryDeserialize<SaveData>(path);
        MainData.sound_volume_bgm = s_data.sound_volume_bgm;
        MainData.sound_volume_sfx = s_data.sound_volume_sfx;
        MainData.stage_1_best_score = s_data.stage_1_best;
        MainData.stage_2_best_score = s_data.stage_2_best;
        MainData.stage_3_best_score = s_data.stage_3_best;
        for (int i = 0; i < 6; i++)
        {
            MainData.stage_clear_check[i] = s_data.clear_check[i];
        }
        for (int i = 0; i < 6; i++)
        {
            MainData.stage_clear_checkREV[i] = s_data.clear_checkREV[i];
        }
        for (int i = 0; i < 3; i++)
        {
            MainData.stage_clear_friends[i] = s_data.clear_friends[i];
        }
        print("LoadSuccess");
    }
    public void Data_Reset()
    {
        print("ResetStart");
        s_data = new SaveData();
        s_data.sound_volume_bgm = 0.5f;
        s_data.sound_volume_sfx = 0.5f;
        s_data.stage_1_best = 0;
        s_data.stage_2_best = 0;
        s_data.stage_3_best = 0;
        for (int i = 0; i < 6; i++)
        {
            s_data.clear_check[i] = false;
        }
        for (int i = 0; i < 6; i++)
        {
            s_data.clear_checkREV[i] = true;
        }
        for (int i = 0; i < 3; i++)
        {
            s_data.clear_friends[i] = false;
        }
        DataManager.BinarySerialize<SaveData>(s_data, path);
        print("ResetSuccess");
        SceneManager.LoadScene("Title");
    }
}
