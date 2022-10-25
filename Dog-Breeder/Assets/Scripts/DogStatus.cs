using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogStatus : MonoBehaviour
{
    public string Name;
    public int HP;
    public bool IsPair = false;
    public bool IsBabe = true;
    public string PairDogName;
    public int DogID;
    public bool CanBeChecked = false;
    public bool CheckStatusNow = false;
    public GameObject CheckText;
    public Dictionary<string, float> traits = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.DogList.Add(this);
        DogID = GameManager.Instance.DogIDNow;
        GameManager.Instance.DogIDNow += 1;
        this.GetComponentInChildren<Text>().text = Name;

        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanBeChecked)
            CheckText.SetActive(true);
        else
            CheckText.SetActive(false);

        if (CanBeChecked && GameManager.Instance.IsStatusActive)
            CheckStatusNow = true;
        else
            CheckStatusNow = false;
    }
}
