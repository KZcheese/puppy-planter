using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogStatus : MonoBehaviour
{
    public string Name;
    public int HP;
    public bool IsPair = false;
    public string PairDogName;
    public int DogID;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.DogList.Add(this);
        DogID = GameManager.Instance.DogIDNow;
        GameManager.Instance.DogIDNow += 1;
        this.GetComponentInChildren<Text>().text = Name;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPair)
            this.GetComponentInChildren<Image>().color = Color.red;
        else
            this.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
    }
}
