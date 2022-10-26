using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    public string DebuffName;
    public string DebuffFromAttribute;
    public int AttributeNumber;
    public int DebuffEffectHP;

    public abstract void Effective(DogStatus NewDog);
    


}

public class PermanentDebuff : Debuff
{
    public PermanentDebuff(string DebuffName, string DebuffFromAttribute, int AttributeNumber, int DebuffEffectHP)
    {
        this.DebuffName = DebuffName;
        this.DebuffFromAttribute = DebuffFromAttribute;
        this.AttributeNumber = AttributeNumber;
        this.DebuffEffectHP = DebuffEffectHP;
    }

    public override void Effective(DogStatus NewDog)
    {
        NewDog.HP -= this.DebuffEffectHP;
        Debug.Log("PermentDamaged");
    }
}

public class RiskyDebuff : Debuff
{
    public bool TurnOn = false;
    public bool IsCure = false;
    public RiskyDebuff(string DebuffName, string DebuffFromAttribute, int AttributeNumber, int DebuffEffectHP)
    {
        this.DebuffName = DebuffName;
        this.DebuffFromAttribute = DebuffFromAttribute;
        this.AttributeNumber = AttributeNumber;
        this.DebuffEffectHP = DebuffEffectHP;
    }

    public override void Effective(DogStatus NewDog)
    {
        
        int succed = Random.Range(0, 4);
        SkinnedMeshRenderer dogSkin = NewDog.GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < dogSkin.sharedMesh.blendShapeCount; i++)
        {
            if (dogSkin.sharedMesh.GetBlendShapeName(i) == this.DebuffFromAttribute)
            {
                if (((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 2) || ((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 9))// change
                {
                    Debug.Log("Risky");
                    if (succed == 1)
                    {
                        Debug.Log("RiskyTurnOn");
                        TurnOn = true;
                    }
                    else if(succed == 2)
                    {
                        Debug.Log("RiskyTurnOff");
                        TurnOn = false;
                    }
                    else
                    {
                        Debug.Log("Cure");
                        IsCure = true;

                    }
                }
                else if (((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 1) || ((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 10))
                {
                    if (succed <= 1)
                    {
                        Debug.Log("RiskyTurnOn");
                        TurnOn = true;
                    }
                    else if(succed == 2)
                    {
                        Debug.Log("RiskyTurnOff");
                        TurnOn = false;
                    }
                    else
                    {
                        Debug.Log("Cure");
                        IsCure = true;
                    }
                }
            }
        }
        if (IsCure) //What would do if Cure;
        {
            for (int z = 0; z < NewDog.Debuffs.Count; z++)
            {
                if (NewDog.Debuffs[z].DebuffName == this.DebuffName)
                {
                    NewDog.Debuffs.Remove(NewDog.Debuffs[z]);
                    break;
                }

            }
        }

        if (TurnOn) // What would do if turn on;
        {
            NewDog.HP -= this.DebuffEffectHP;
        }
        else
        {
            //What would do if turn off
        }

    }
}

public class RandomDebuff : Debuff
{
    public bool TurnOn = false;
    public RandomDebuff(string DebuffName, string DebuffFromAttribute, int AttributeNumber, int DebuffEffectHP)
    {
        this.DebuffName = DebuffName;
        this.DebuffFromAttribute = DebuffFromAttribute;
        this.AttributeNumber = AttributeNumber;
        this.DebuffEffectHP = DebuffEffectHP;
    }

    public override void Effective(DogStatus NewDog)
    {
        int succed = Random.Range(0, 4);

        SkinnedMeshRenderer dogSkin = NewDog.GetComponent<SkinnedMeshRenderer>();
        for (int i =0;i< dogSkin.sharedMesh.blendShapeCount; i++)
        {
            
            if (dogSkin.sharedMesh.GetBlendShapeName(i) == this.DebuffFromAttribute)
            {
                if (((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 2) || ((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 9))
                {
                    if (succed == 1)
                    {
                        Debug.Log("RandomDamagedSucceed");
                        NewDog.HP -= this.DebuffEffectHP;
                    }
                    else
                    {
                        Debug.Log("RandomDamagedFailed");
                    }
                }else if (((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 1) || ((int)dogSkin.GetBlendShapeWeight(i) / 10 % 10 == 10))
                {
                    if (succed <= 1)
                    {
                        Debug.Log("RandomDamagedSucceed");
                        NewDog.HP -= this.DebuffEffectHP;
                    }
                    else
                    {
                        Debug.Log("RandomDamagedFailed");
                    }
                }
            }
        }

           
    }
}

public class DebuffManager : MonoBehaviour
{
    // Start is called before the first frame update


    public List<Debuff> DebuffList = new List<Debuff>();

    public static DebuffManager Instance;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }
    void Start()
    {
        //Eye
        DebuffList.Add(new PermanentDebuff("Blindness", "eye_small", 9, 1));
        DebuffList.Add(new RiskyDebuff("Cherry Eye", "eye_small", 10, 1));

        DebuffList.Add(new RandomDebuff("Ulceration of cornea", "eye_big", 9, 1));
        DebuffList.Add(new RiskyDebuff("Dry eyes", "eye_big", 10, 1));
        
        //Nose
        DebuffList.Add(new RiskyDebuff("Skin infection", "nose_small", 9, 1));
        DebuffList.Add(new PermanentDebuff("Dental crowding", "nose_small", 10, 1));

        DebuffList.Add(new RandomDebuff("Nasal mites", "nose_big", 9, 1));
        DebuffList.Add(new RiskyDebuff("Fungal infections", "nose_big", 10, 1));

        //Muscle
        DebuffList.Add(new RandomDebuff("DifficultyEating", "muscle_small", 9, 1));
        DebuffList.Add(new PermanentDebuff("Thyroid disease", "muscle_small", 10, 1));

        DebuffList.Add(new PermanentDebuff("Limited mobility", "muscle_big", 9, 1));

        //Leg
        DebuffList.Add(new RiskyDebuff("Foot infections", "leg_shorter", 9, 1));
        DebuffList.Add(new PermanentDebuff("Crippling back pain", "leg_shorter", 10, 1));

        DebuffList.Add(new RiskyDebuff("Arthritis", "leg_longer",9, 1));
        DebuffList.Add(new PermanentDebuff("Legs break easily", "leg_longer", 10, 1));

        //Ear
        //DebuffList.Add(new RiskyDebuff(" ", "ear_droopy", 9, 1));
        //DebuffList.Add(new PermanentDebuff(" ", "ear_pointy", 9, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
