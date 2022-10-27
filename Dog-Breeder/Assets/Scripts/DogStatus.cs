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
    public bool gender; // true for male, false for female
    public int birthday;
    public List<Debuff> Debuffs = new List<Debuff>();
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.DogList.Add(this);
        DogID = GameManager.Instance.DogIDNow;
        GameManager.Instance.DogIDNow += 1;
        this.GetComponentInChildren<Text>().text = Name;
        if(GameManager.Instance.DayCount - birthday != 0)
            RandomPosition();
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



    public string GetDescription()
    {
        string description = Name + " is a " + (GameManager.Instance.DayCount - birthday) + "-day old " + (gender ? "male" : "female") + " dog.\n";
        description += $"HP: {HP}\n\n";
        SkinnedMeshRenderer skin = GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            description += skin.sharedMesh.GetBlendShapeName(i) + ": " + skin.GetBlendShapeWeight(i) + "\n";
        }
        description += $"\nProfit if sell: {DogManager.Instance.CalculateProfit(DogID)}\n";
        for(int i = 0;i < Debuffs.Count; i++)
        {
            description += (Debuffs[i].DebuffName + "\n");
        }
         
        return description;
    }


    public void RandomPosition()
    {
        float _randomX = Random.Range(-3.5f, 3.5f);
        float _randomZ = Random.Range(-3.5f, 3.5f);
        float _randomEulerY = Random.Range(0f, 360f);

        transform.position = new Vector3(_randomX, 0.36f, _randomZ);
        transform.rotation = Quaternion.Euler(new Vector3(0, _randomEulerY, 0));

    }
}
