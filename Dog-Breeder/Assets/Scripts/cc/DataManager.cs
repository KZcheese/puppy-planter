using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {
        string path = Application.streamingAssetsPath + "/json/Dogs.json";
        string data = File.ReadAllText(path);
        Dog dog = JsonUtility.FromJson<Dog>(data);
        Debug.Log(dog.name);
        Debug.Log(dog.hp);
        Debug.Log(dog.father);
        Debug.Log(dog.mother);
    }
}
