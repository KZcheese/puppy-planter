using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int DayCount = 1; // The day of the game, add 1 every day 
    public int WeekCount = 1;
    public int MonthCount = 1;

    

    public GameObject dogPrefab;
    public Transform bornSpot;
    public Transform outSpot;

    public GameObject DayUI;
    public GameObject PhoneUI,StatusUI;
    public GameObject Lines;

    public LayerMask LayerDetect;

    public Text moneyText;

    public int DogCapMax = 10;

    public List<DogStatus> DogList = new List<DogStatus>();

    public bool IsPhoneActive = false;
    public bool IsPairLineActive = true;
    public bool IsStatusActive = false;

    public static GameManager Instance;

    public int DogIDNow = 1000;

    //public List<PairInfo> pairInfos = new List<PairInfo>();
    public Dictionary<int,int> DogPairDic = new Dictionary<int , int>();

    [HideInInspector]
    public float[] demands = new float[5];
    public float Money,YesterdayMoney;
    public int CostPerDay, CostPerWeak, CostPerMonth;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    void Start()
    {
        YesterdayMoney = Money;
        DemandController.Instance.ShuffleDemands();
    }

    // Update is called once per frame
    void Update()
    {
        TakeMyPhone();
        CheckDogs();

        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPairLineActive = !IsPairLineActive;
            Lines.SetActive(IsPairLineActive);
        }

        if (IsPairLineActive)
            LinePairDogs();

        moneyText.text = Money.ToString("c2"); // 2dp currency;
    }

    void LinePairDogs()
    {
        ClearLine();

        LineRenderer[] _lineRenderer =  new LineRenderer[DogPairDic.Count];

        int _index = 0;
        foreach (var key in DogPairDic.Keys)
        {
            string _lineName = "Lines/Lines" + _index;
            _lineRenderer[_index] = GameObject.Find(_lineName).GetComponent<LineRenderer>();
            _lineRenderer[_index].SetPosition(0, DogList[FindDogIndex(key)].transform.position);
            _lineRenderer[_index].SetPosition(1, DogList[FindDogIndex(DogPairDic[key])].transform.position);
            _index += 1;
        }
    }

    void ClearLine()
    {
        for(int i = 0; i < 5; i++)
        {
            string _lineName = "Lines/Lines" + i;
            LineRenderer _lineRenderer = GameObject.Find(_lineName).GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
    void CheckDogs() // Use Click to check the click dog
    {
        
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit, Mathf.Infinity, LayerDetect))
            {
                
                _hit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 4;
                

                if ((Input.GetMouseButtonDown(0))&&(DaySwitchControl.Instance.TransportScene.activeSelf == false))
                {
                    IsStatusActive = !IsStatusActive;
                    StatusUI.SetActive(IsStatusActive);
                    StatusUpdate.Instance.UpdateStatusText(_hit.collider.gameObject.GetComponent<DogStatus>());
                StatusUpdate.Instance.DogId = _hit.collider.gameObject.GetComponent<DogStatus>().DogID;
                    _hit.collider.gameObject.GetComponent<DogStatus>().CheckStatusNow = true;
                }

        }
        else
        {
            foreach (var dogs in DogList)
            {
                dogs.GetComponent<Outline>().OutlineWidth = 0;
                dogs.GetComponent<DogStatus>().CheckStatusNow = false;
            }
        }
        
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

    public int FindDogIndex(int DogId) // Find the dog index in Dog list from pair dictionary
    {
        for(int i = 0; i < DogList.Count; i++)
        {
            if (DogList[i].DogID == DogId)
                return i;
        }

        return 0;
    }

    public DogStatus GetDog(int DogId)
    {
        for (int i = 0; i < DogList.Count; i++)
        {
            if (DogList[i].DogID == DogId)
                return DogList[i];
        }
        return null;
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

    public void RemovePairFromDic(int FirstDogId, int SecondDogId) // Remove pair first, incase the dog has already paired.
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

    public void NewDay()
    {
        DogManager.Instance.GenerateNewDogs();
        DogPairDic.Clear();
        for (int i = 0; i < Instance.DogList.Count; i++)
        {
            Instance.DogList[i].IsPair = false;
            Instance.DogList[i].PairDogName = "";
            Instance.DogList[i].DebuffEffected = false;
        }
    }
}
