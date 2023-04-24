using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class DataManager
{
    public static void BinarySerialize<T>(T t, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Debug.Log(filePath);
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, t);
        stream.Close();
    }

    public static T BinaryDeserialize<T>(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Debug.Log(filePath);
        FileStream stream = new FileStream(filePath, FileMode.Open);
        T t = (T)formatter.Deserialize(stream);
        stream.Close();

        return t;
    }
}
