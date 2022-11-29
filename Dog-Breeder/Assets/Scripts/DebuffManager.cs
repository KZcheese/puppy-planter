using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    public string DebuffName;
    public string DebuffDescription;
    public bool due22big;
    public float damage = 0;

    public Debuff(string DebuffName, string DebuffDescription, bool due22big)
    {
        this.DebuffName = DebuffName;
        this.DebuffDescription = DebuffDescription;
        this.due22big = due22big;
    }

    public float ComputeSeverity(float traitVal)
    {
        float severity = Mathf.Abs(traitVal - 50);
        return severity * severity / 2500;
    }

    public abstract void RollDamage(float traitVal);

    public void Effective(DogStatus dog)
    {
        if (damage > 0)
        {
            dog.HP -= damage;
            Debug.Log(dog.Name + " got " + DebuffName);
        }
    }
}

public class PermanentDebuff : Debuff
{
    public PermanentDebuff(string DebuffName, string DebuffDescription, bool due22big) : base(DebuffName, DebuffDescription, due22big) { }

    public override void RollDamage(float traitVal)
    {
        float severity = ComputeSeverity(traitVal);
        if (severity > 0.5) damage = Mathf.Round(severity * 5);
        else damage = 0;
    }
}

public class RiskyDebuff : Debuff
{
    public RiskyDebuff(string DebuffName, string DebuffDescription, bool due22big) : base(DebuffName, DebuffDescription, due22big) { }

    public override void RollDamage(float traitVal)
    {
        float random = Random.value;
        float severity = ComputeSeverity(traitVal);
        if (damage > 0) damage = !(random > severity) ? 10 : 0;
        else damage = random < severity / 2 ? 10 : 0;
    }
}

public class RandomDebuff : Debuff
{
    public RandomDebuff(string DebuffName, string DebuffDescription, bool due22big) : base(DebuffName, DebuffDescription, due22big) { }

    public override void RollDamage(float traitVal)
    {
        float random = Random.value;
        float severity = ComputeSeverity(traitVal);
        if (severity > .25f) damage = severity > random ? Mathf.Round(random * 15) : .0f;
        else damage = 0;
    }
}

public class DebuffManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<KeyValuePair<List<Debuff>, List<Debuff>>> debuffList = new List<KeyValuePair<List<Debuff>, List<Debuff>>>();

    public static DebuffManager Instance;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }
    void Start()
    {


        //Cheek
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>());

        //Ear TODO
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>());

        //Eye
        PermanentDebuff blindness = new PermanentDebuff("Blindness", "", false);
        RiskyDebuff cherryEyes = new RiskyDebuff("Cherry Eyes", "Eye becomes inflamed and swollen", false);
        RandomDebuff ulceration_cornea = new RandomDebuff("Ulceration of cornea", "Leading to trouble seeing", true);
        RiskyDebuff dryEyes = new RiskyDebuff("Dry eyes", "Leading to conjunctivitis", true);
        List<Debuff> eyeDebuffs_small = new List<Debuff> { blindness, cherryEyes };
        List<Debuff> eyeDebuffs_big = new List<Debuff> { ulceration_cornea, dryEyes };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(eyeDebuffs_small, eyeDebuffs_big));

        //Muscle
        RandomDebuff difficultyEating = new RandomDebuff("DifficultyEating", "Due to pain in gums, teeth etc therefore gums bleed sometimes while dog is eating", false);
        PermanentDebuff thyroidDisease = new PermanentDebuff("Thyroid disease", "", false);
        PermanentDebuff limitedMobility = new PermanentDebuff("Limited mobility", "Hip dysplasia", true);
        List<Debuff> muscleDebuffs_small = new List<Debuff>() { difficultyEating, thyroidDisease };
        List<Debuff> muscleDebuffs_big = new List<Debuff>() { limitedMobility };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(muscleDebuffs_small, muscleDebuffs_big));

        //Nose
        RiskyDebuff skinInfection = new RiskyDebuff("Skin infection", "Due to more skin folds", false);
        PermanentDebuff dentalCrowdingNMisallignment = new PermanentDebuff("Dental crowding", "Dental crowding and misalignment of teeth leads to hurting dog’s mouth when they eat", false);
        RandomDebuff nasalMites = new RandomDebuff("Nasal mites", "", true);
        RiskyDebuff fungalInfections = new RiskyDebuff("Fungal infections", "", true);
        List<Debuff> noseDebuffs_small = new List<Debuff> { skinInfection, dentalCrowdingNMisallignment };
        List<Debuff> noseDebuffs_big = new List<Debuff> { nasalMites, fungalInfections };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(noseDebuffs_small, noseDebuffs_big));
    }

    public void InitializeDebuffs(GameObject newDog)
    {
        DogStatus status = newDog.GetComponent<DogStatus>();
        SkinnedMeshRenderer skin = newDog.GetComponentInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            int val = (int)skin.GetBlendShapeWeight(i) / 10;
            if (val <= 2) status.Debuffs[i] = debuffList[i].Key;
            else if (val >= 8) status.Debuffs[i] = debuffList[i].Value;
        }
    }
}
