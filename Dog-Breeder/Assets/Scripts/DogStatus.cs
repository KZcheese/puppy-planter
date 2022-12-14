using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DogStatus : MonoBehaviour
{
    public string Name;
    public float HP;
    public bool IsPair = false;
    public string PairDogName;
    public int DogID;
    public bool DebuffEffected = false;
    public bool CheckStatusNow = false;
    public bool gender; // true for male, false for female
    public int birthday;
    public bool isAdult;
    private int isearModded = 0;
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
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
      
    }



    public string GetDescription()
    {
        string description = Name + " is a " + (GameManager.Instance.DayCount - birthday) + "-day old " + (gender ? "male" : "female") + " dog.\n\n";
        description += $"HP: {HP}\nDebuffs:\n";

        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        for (int i = 1; i < skin.sharedMesh.blendShapeCount; i++)
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
        for (int i = 1; i < skin.sharedMesh.blendShapeCount; i++)
        {
            description += skin.sharedMesh.GetBlendShapeName(i) + ": " + Mathf.Round(skin.GetBlendShapeWeight(i)) + "\n";
        }
        description += $"\nProfit if sell: {DogManager.Instance.CalculateProfit(DogID)}\n";

        return description;
    }

   /* public void Display()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        _doggyname.text = Name;
        _doggyage.text = (GameManager.Instance.DayCount - birthday).ToString();
        _doggygender.text = (gender ? "male" : "female");
        _earSize.text = Mathf.Round(skin.GetBlendShapeWeight(1)).ToString();
        _eyeSize.text = Mathf.Round(skin.GetBlendShapeWeight(2)).ToString();
        _muscleSize.text = Mathf.Round(skin.GetBlendShapeWeight(3)).ToString();
        _noseSize.text = Mathf.Round(skin.GetBlendShapeWeight(4)).ToString();
    }
*/
   public string Dog_Name()
    {
        return Name;
    }

    public string Dog_Age()
    {
        return (GameManager.Instance.DayCount - birthday).ToString();
    }
    public string Dog_Gen()
    {
        return (gender ? "male" : "female");
    }
    public string DogEar_Size()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        return (Mathf.Round(skin.GetBlendShapeWeight(1)).ToString());
    }
    public string DogEye_Size()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        return (Mathf.Round(skin.GetBlendShapeWeight(2)).ToString());
    }
    public string DogMuscle_Size()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        return (Mathf.Round(skin.GetBlendShapeWeight(3)).ToString());
    }
    public string DogNose_Size()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        return (Mathf.Round(skin.GetBlendShapeWeight(4)).ToString());
    }

    public void Mod_DogEarSize()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        if (isearModded < 5 && (GameManager.Instance.DayCount - birthday)<5)
        {
            skin.SetBlendShapeWeight(1, Mathf.Round(skin.GetBlendShapeWeight(1)) + 1);
            isearModded += 1;
        }
        else if (isearModded == 5)
        {
            return;
        }
    }


    void RandomPosition(bool isAdult)
    {
        if (isAdult)
        {
            float _randomX = Random.Range(-1.5f, 1.5f);
            float _randomZ = Random.Range(-1.5f, 1.5f);
            float _randomEulerY = Random.Range(0f, 360f);

            transform.position += new Vector3(_randomX, 0f, _randomZ);
            transform.rotation = Quaternion.Euler(new Vector3(0, _randomEulerY, 0));
        }
        else
        {
            float _randomX = Random.Range(-.2f, .2f);
            float _randomZ = Random.Range(-.5f, .5f);
            float _randomEulerY = Random.Range(0f, 360f);

            transform.position += new Vector3(_randomX, 0f, _randomZ);
            transform.rotation = Quaternion.Euler(new Vector3(0, _randomEulerY, 0));
        }
    }

    public void NewDay()
    {
        CheckAge();
        RandomPosition(isAdult);
        RollDebuffs();
    }

    void CheckAge()
    {
        if (GameManager.Instance.DayCount - birthday >= 5)
        {
            transform.position = GameManager.Instance.outSpot.position;
            isAdult = true;
        }
        else
        {
            transform.localScale += new Vector3(.1f, .1f, .1f);
        }
    }

    void RollDebuffs()
    {
        string _saveText = "";
        if (Debuffs != null)
        {


            SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
            {
                if (Debuffs[i] != null)
                {
                    for (int j = Debuffs[i].Count - 1; j >= 0; j--)
                    {
                        Debuffs[i][j].RollDamage(skin.GetBlendShapeWeight(i));
                        Debuffs[i][j].Effective(this);
                        if( (HP <= 0)&&(this.gameObject.activeSelf == true)) // If dog dead
                        {
                            Debug.Log(Name + " Dead");
                            GameManager.Instance.DogList.Remove(this);
                            this.gameObject.SetActive(false);
                        }
                        _saveText += "<color=#FF0000><size=18>" + Debuffs[i][j].DebuffName + "                     - " + Debuffs[i][j].damage + "</size></color>\n";
                        DebuffEffected = true;
                    }
                }

            }
        }

        if (DebuffEffected)
        {
            DaySwitchControl.Instance.DebuffText += "<color=#FF0000>" + Name + "                                          X</color>" + "\n";
            DaySwitchControl.Instance.DebuffText += _saveText;
        }
        else
        {
            DaySwitchControl.Instance.DebuffText += Name + "                                          OK\n";
        }

    }

}
