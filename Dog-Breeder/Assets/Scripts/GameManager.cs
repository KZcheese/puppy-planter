using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int DayCount = 0; // The day of the game, add 1 every day 

    public GameObject DayUI;
    public GameObject PhoneUI;

    public int DogCapMax = 10;

    public List<DogStatus> DogList = new List<DogStatus>();

    public bool IsPhoneActive = false;

    public static GameManager Instance;

    public int DogIDNow = 1000;

    public Dictionary<int,int> DogPairDic = new Dictionary<int , int>();

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeMyPhone();
    }


    void TakeMyPhone() // Bag
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            IsPhoneActive = !IsPhoneActive;
            PhoneUI.SetActive(IsPhoneActive);
        }
    }

    public void DogPaired(int FirstDogId, int SecondDogId) // Pair two dogs
    {

        RemovePairFromDic(FirstDogId, SecondDogId);

        DogPairDic.Add(FirstDogId, SecondDogId);

        int _firstDogIndex = FindDogIndex(FirstDogId);
        int _secondDogIndex = FindDogIndex(SecondDogId);

        DogList[_firstDogIndex].IsPair = DogList[_secondDogIndex].IsPair = true;

        Instance.DogList[_firstDogIndex].PairDogName = DogList[_secondDogIndex].Name;
        Instance.DogList[_secondDogIndex].PairDogName = DogList[_firstDogIndex].Name;
    }

    int FindDogIndex(int DogId) // Find the dog index in Dog list from pair dictionary
    {
        for(int i = 0; i < DogList.Count; i++)
        {
            if (DogList[i].DogID == DogId)
                return i;
        }

        return 0;
    }

    int FindKey(int value) // Find the key in pair dictionary
    {
        foreach(int key in DogPairDic.Keys)
        {
            if (DogPairDic[key] == value)
                return key;
        }

        return 0;
    }

    void RemovePairFromDic(int FirstDogId, int SecondDogId) // Remove pair first, incase the dog has already paired.
    {
        int FirstDogIndex = FindDogIndex(FirstDogId);
        int SecondDogIndex = FindDogIndex(SecondDogId);

        if (DogPairDic.ContainsKey(FirstDogId))
        {
            DogList[FindDogIndex(DogPairDic[FirstDogId])].PairDogName = "";
            DogList[FindDogIndex(DogPairDic[FirstDogId])].IsPair = false;
            DogPairDic.Remove(FirstDogId);
        }
        else if (DogPairDic.ContainsValue(FirstDogId))
        {
            
            DogList[FindDogIndex(FindKey(FirstDogId))].PairDogName = "";
            DogList[FindDogIndex(FindKey(FirstDogId))].IsPair = false;
            DogPairDic.Remove(FindKey(FirstDogId));
        }

        DogList[FirstDogIndex].PairDogName = "";
        DogList[FirstDogIndex].IsPair = false;

        if (DogPairDic.ContainsKey(SecondDogId))
        {
            DogList[FindDogIndex(DogPairDic[SecondDogId])].PairDogName = "";
            DogList[FindDogIndex(DogPairDic[SecondDogId])].IsPair = false;
            DogPairDic.Remove(SecondDogId);
        }
        else if (DogPairDic.ContainsValue(SecondDogId))
        {
            DogList[FindDogIndex(FindKey(SecondDogId))].PairDogName = "";
            DogList[FindDogIndex(FindKey(SecondDogId))].IsPair = false;
            DogPairDic.Remove(FindKey(SecondDogId));
        }

        DogList[SecondDogIndex].PairDogName = "";
        DogList[SecondDogIndex].IsPair = false;

    }

    
}
