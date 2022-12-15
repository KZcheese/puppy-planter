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
    
    private int earModded = 0;
    private int eyeModded = 0;
    private int muscleModded = 0;
    private int noseModded = 0;
    
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

        return "Profit if sell: "+DogManager.Instance.CalculateProfit(DogID);
    }


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

   public void Mod_DogEarSizeInc()
    {
        if (earModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (earModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(1, Mathf.Round(skin.GetBlendShapeWeight(1)) + 10 > 100 ? 100 : Mathf.Round(skin.GetBlendShapeWeight(1)) + 10);
                earModded++;
            }
        }
    }
    public void Mod_DogEarSizeDec()
    {
        if (earModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (earModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(1, Mathf.Round(skin.GetBlendShapeWeight(1)) - 10 < 0 ? 0 : Mathf.Round(skin.GetBlendShapeWeight(1)) - 10);
                earModded++;
            }
        }
    }
    public void Mod_DogEyeSizeInc()
    {
        if (eyeModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (eyeModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(2, Mathf.Round(skin.GetBlendShapeWeight(2)) + 10 > 100 ? 100 : Mathf.Round(skin.GetBlendShapeWeight(2)) + 10);
                eyeModded++;
            }
        }
    }
    public void Mod_DogEyeSizeDec()
    {
        if (eyeModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (eyeModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(2, Mathf.Round(skin.GetBlendShapeWeight(2)) - 10 < 0 ? 0 : Mathf.Round(skin.GetBlendShapeWeight(2)) - 10);
                eyeModded++;
            }
        }
    }
    public void Mod_DogMuscleSizeInc()
    {
        if (muscleModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (muscleModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(3, Mathf.Round(skin.GetBlendShapeWeight(3)) + 10 > 100 ? 100 : Mathf.Round(skin.GetBlendShapeWeight(3)) + 10);
                muscleModded++;
            }
        }
    }
    public void Mod_DogMuscleSizeDec()
    {
        if (muscleModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (muscleModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(3, Mathf.Round(skin.GetBlendShapeWeight(3)) - 10 < 0 ? 0 : Mathf.Round(skin.GetBlendShapeWeight(3)) - 10);
                muscleModded++;
            }
        }
    }
    public void Mod_DogNoseSizeInc()
    {
        if (noseModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (noseModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(4, Mathf.Round(skin.GetBlendShapeWeight(4)) + 10 > 100 ? 100 : Mathf.Round(skin.GetBlendShapeWeight(4)) + 10);
                noseModded++;
            }
        }
    }
    public void Mod_DogNoseSizeDec()
    {
        if (noseModded < 5 && (GameManager.Instance.DayCount - birthday) < 5)
        {
            if (noseModded == (GameManager.Instance.DayCount - birthday))
            {
                SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
                skin.SetBlendShapeWeight(4, Mathf.Round(skin.GetBlendShapeWeight(4)) - 10 < 0 ? 0 : Mathf.Round(skin.GetBlendShapeWeight(4)) - 10);
                noseModded++;
            }
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
