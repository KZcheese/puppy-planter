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

    public List<List<Debuff>> Debuffs = new List<List<Debuff>> { null, null, null, null, null };

    private void Awake()
    {
        //Debuffs = new List<List<Debuff>>(5);
    }


    void Start()
    {
        GameManager.Instance.DogList.Add(this);
        DogID = GameManager.Instance.DogIDNow;
        GameManager.Instance.DogIDNow += 1;
        this.GetComponentInChildren<Text>().text = Name;
        if (GameManager.Instance.DayCount - birthday != 0)
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
        string description = Name + " is a " + (GameManager.Instance.DayCount - birthday) + "-day old " + (gender ? "male" : "female") + " dog.\n\n";
        description += $"HP: {HP}\nDebuffs:\n";
        SkinnedMeshRenderer skin = GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            if (Debuffs[i] != null)
            {
                for (int j = Debuffs[i].Count - 1; j >= 0; j--)
                {
                    description += Debuffs[i][j].DebuffName + " ";
                }
                description += "\n";
            }
        }
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            description += skin.sharedMesh.GetBlendShapeName(i) + ": " + skin.GetBlendShapeWeight(i) + "\n";
        }
        description += $"\nProfit if sell: {DogManager.Instance.CalculateProfit(DogID)}\n";

        return description;
    }

    void RandomPosition()
    {
        float _randomX = Random.Range(-3.5f, 3.5f);
        float _randomZ = Random.Range(-3.5f, 3.5f);
        float _randomEulerY = Random.Range(0f, 360f);

        transform.position = new Vector3(_randomX, 0.36f, _randomZ);
        transform.rotation = Quaternion.Euler(new Vector3(0, _randomEulerY, 0));
    }

    public void NewDay()
    {
        RandomPosition();
        RollDebuffs();
    }

    void RollDebuffs()
    {
        if (Debuffs != null)
        {
            SkinnedMeshRenderer skin = GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
            {
                if (Debuffs[i] != null)
                {
                    for (int j = Debuffs[i].Count - 1; j >= 0; j--)
                    {
                        int res = Debuffs[i][j].Roll((int)skin.GetBlendShapeWeight(i) / 10);
                        if (res == 1) Debuffs[i][j].Effective(this);
                        else if (res == -1)
                        {
                            Debug.Log(Name + "'s " + Debuffs[i][j] + " cured");
                            Debuffs[i].RemoveAt(j);
                        }
                    }
                }
            }
        }
    }

}
