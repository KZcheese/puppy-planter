using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    public string DebuffName;
    public string DebuffDescription;
    public int DebuffEffectHP;
    public bool due22big;

    public Debuff(string DebuffName, string DebuffDescription, int DebuffEffectHP, bool due22big)
    {
        this.DebuffName = DebuffName;
        this.DebuffDescription = DebuffDescription;
        this.DebuffEffectHP = DebuffEffectHP;
        this.due22big = due22big;
    }

    //1 for sickness, 0 for common, -1 for cure
    public abstract int Roll(int traitVal);

    public void Effective(DogStatus dog)
    {
        dog.HP -= DebuffEffectHP;
        Debug.Log(dog.Name + " got " + DebuffName);
    }
}

public class PermanentDebuff : Debuff
{
    public PermanentDebuff(string DebuffName, string DebuffDescription, int DebuffEffectHP, bool due22big) : base(DebuffName, DebuffDescription, DebuffEffectHP, due22big)
    {
    }

    public override int Roll(int traitVal)
    {
        return 1;
    }
}

public class RiskyDebuff : Debuff
{
    public RiskyDebuff(string DebuffName, string DebuffDescription, int DebuffEffectHP, bool due22big) : base(DebuffName, DebuffDescription, DebuffEffectHP, due22big)
    {
    }

    public override int Roll(int traitVal)
    {
        if (traitVal <= 1 || traitVal >= 9) return Random.value > .5f ? 1 : RollCure(traitVal);
        else if (traitVal <= 2 || traitVal >= 8) return Random.value > .75f ? 1 : RollCure(traitVal);
        else return 0;
    }

    public int RollCure(int traitVal)
    {
        if (traitVal <= 1 || traitVal >= 9) return Random.value > .75f ? -1 : 0;
        else if (traitVal <= 2 || traitVal >= 8) return Random.value > .5f ? -1 : 0;
        else return -1;
    }
}

public class RandomDebuff : Debuff
{
    public RandomDebuff(string DebuffName, string DebuffDescription, int DebuffEffectHP, bool due22big) : base(DebuffName, DebuffDescription, DebuffEffectHP, due22big)
    {
    }

    public override int Roll(int traitVal)
    {
        if (traitVal <= 1 || traitVal >= 9) return Random.value > .5f ? 1 : 0;
        else if (traitVal <= 2 || traitVal >= 8) return Random.value > .75f ? 1 : 0;
        else return 0;
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
        // Keep the same order as blendershape

        //Leg
        RiskyDebuff footInfections = new RiskyDebuff("Foot infections", "Due to more skin folds", 1, false);
        PermanentDebuff cripplingBackPain = new PermanentDebuff("Crippling back pain", "", 1, false);
        RiskyDebuff arthritis = new RiskyDebuff("Arthritis", "", 1, true);
        PermanentDebuff legsBreakEasily = new PermanentDebuff("Legs break easily", "", 1, true);
        List<Debuff> legDebuffs_small = new List<Debuff> { footInfections, cripplingBackPain};
        List<Debuff> legDebuffs_big = new List<Debuff> { arthritis, legsBreakEasily };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(legDebuffs_small, legDebuffs_big));

        //Muscle
        RandomDebuff difficultyEating = new RandomDebuff("DifficultyEating", "Due to pain in gums, teeth etc therefore gums bleed sometimes while dog is eating", 1, false);
        PermanentDebuff thyroidDisease = new PermanentDebuff("Thyroid disease", "", 1, false);
        PermanentDebuff limitedMobility = new PermanentDebuff("Limited mobility", "Hip dysplasia", 1, true);
        List<Debuff> muscleDebuffs_small = new List<Debuff>() { difficultyEating, thyroidDisease };
        List<Debuff> muscleDebuffs_big = new List<Debuff>() { limitedMobility };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(muscleDebuffs_small, muscleDebuffs_big));

        //Ear TODO
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>());

        //Eye
        PermanentDebuff blindness = new PermanentDebuff("Blindness", "", 1, false);
        RiskyDebuff cherryEyes = new RiskyDebuff("Cherry Eyes", "Eye becomes inflamed and swollen", 1, false);
        RandomDebuff ulceration_cornea = new RandomDebuff("Ulceration of cornea", "Leading to trouble seeing", 1, true);
        RiskyDebuff dryEyes = new RiskyDebuff("Dry eyes", "Leading to conjunctivitis", 1, true);
        List<Debuff> eyeDebuffs_small = new List<Debuff> { blindness, cherryEyes };
        List<Debuff> eyeDebuffs_big = new List<Debuff> { ulceration_cornea, dryEyes };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(eyeDebuffs_small, eyeDebuffs_big));

        //Nose
        RiskyDebuff skinInfection = new RiskyDebuff("Skin infection", "Due to more skin folds", 1, false);
        PermanentDebuff dentalCrowdingNMisallignment = new PermanentDebuff("Dental crowding", "Dental crowding and misalignment of teeth leads to hurting dog’s mouth when they eat", 1, false);
        RandomDebuff nasalMites = new RandomDebuff("Nasal mites", "", 1, true);
        RiskyDebuff fungalInfections = new RiskyDebuff("Fungal infections", "", 1, true);
        List<Debuff> noseDebuffs_small = new List<Debuff> { skinInfection, dentalCrowdingNMisallignment };
        List<Debuff> noseDebuffs_big = new List<Debuff> { nasalMites, fungalInfections };
        debuffList.Add(new KeyValuePair<List<Debuff>, List<Debuff>>(noseDebuffs_small, noseDebuffs_big));
    }

    public void InitializeDebuffs(GameObject newDog)
    {
        DogStatus status = newDog.GetComponent<DogStatus>();
        SkinnedMeshRenderer skin = newDog.GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            int val = (int)skin.GetBlendShapeWeight(i) / 10;
            if (val <= 2) status.Debuffs[i] = debuffList[i].Key;
            else if (val >= 8) status.Debuffs[i] = debuffList[i].Value;
        }
    }
}
