using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour {
    private void Start() {
        var path = Application.streamingAssetsPath + "/json/Dogs.json";
        var data = File.ReadAllText(path);
        var dog = JsonUtility.FromJson<Dog>(data);
        Debug.Log(dog.name);
        Debug.Log(dog.hp);
        Debug.Log(dog.father);
        Debug.Log(dog.mother);
    }
}