using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    // Start is called before the first frame update


    public GameObject dogPrefab;
    public Transform bornSpot;
    public Transform outSpot;

    public GameObject PhoneUI, StatusUI, RentReminderUI;
    public GameObject Lines;

    public LayerMask LayerDetect;

    public Text moneyText;


    public List<DogStatus> DogList = new();

    public bool IsPhoneActive;
    public bool IsPairLineActive = true;
    public bool IsStatusActive;

    public int DogIDNow = 1000;
    public int MaxDogNumber;
    public int MaxPenCap;
    public int CostPerDog, CostPerDay, CostPerWeak, CostPerMonth;
    public List<int> ReminderPopDaysList = new();

    [HideInInspector] public float[] demands = new float[5];

    public float Money, YesterdayMoney;
    public int DayCount = 1; // The day of the game, add 1 every day 
    public int WeekCount = 1;
    public int MonthCount = 1;
    public int TodayUpdateCost;

    public int newDogCount;

    //public List<PairInfo> pairInfos = new List<PairInfo>();
    public Dictionary<int, int> DogPairDic = new();


    //[HideInInspector]
    //public bool UIpop = false;

    private void Awake() {
        if (!Instance) Instance = this;

        PhoneUI.SetActive(IsPhoneActive = true);
        PhoneUI.SetActive(IsPhoneActive = false);
    }

    private void Start() {
        AudioMgr.Instance.PlayGameBgm(AudioBgmType.RoomBgm);

        YesterdayMoney = Money;
        DemandController.Instance.ShuffleDemands();
    }

    // Update is called once per frame
    private void Update() {
        OpenMainMenu();

        if (Input.GetKeyDown(KeyCode.P)) {
            IsPairLineActive = !IsPairLineActive;
            Lines.SetActive(IsPairLineActive);
        }

        if (IsPairLineActive)
            LinePairDogs();


        moneyText.text = Money.ToString("c2"); // 2dp currency;

        /*if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();*/
    }

    private void LinePairDogs() {
        ClearLine();

        var _lineRenderer = new LineRenderer[DogPairDic.Count];

        var _index = 0;
        foreach (var key in DogPairDic.Keys) {
            var _lineName = "Lines/Lines" + _index;
            _lineRenderer[_index] = GameObject.Find(_lineName).GetComponent<LineRenderer>();
            _lineRenderer[_index].SetPosition(0, DogList[FindDogIndex(key)].transform.position);
            _lineRenderer[_index].SetPosition(1, DogList[FindDogIndex(DogPairDic[key])].transform.position);
            _index += 1;
        }
    }

    private void ClearLine() {
        for (var i = 0; i < 5; i++) {
            var _lineName = "Lines/Lines" + i;
            var _lineRenderer = GameObject.Find(_lineName).GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }


    private void OpenMainMenu() // Bag
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            if (IsPhoneActive) {
                if (!PhoneControl.Instance.PairScreen.activeSelf) {
                    IsPhoneActive = !IsPhoneActive;
                    PhoneControl.Instance.GoBackMainScreenButton();
                    PhoneUI.SetActive(IsPhoneActive);
                    CameraMovement.Instance._moveMode = true;
                }
            }
            else {
                IsPhoneActive = !IsPhoneActive;
                PhoneUI.SetActive(IsPhoneActive);
                CameraMovement.Instance._moveMode = false;
            }
        }
    }

    public void DogPaired(int FirstDogId, int SecondDogId) // Pair two dogs
    {
        RemovePairFromDic(FirstDogId, SecondDogId);

        DogPairDic.Add(FirstDogId, SecondDogId);

        var _firstDogIndex = FindDogIndex(FirstDogId);
        var _secondDogIndex = FindDogIndex(SecondDogId);

        DogList[_firstDogIndex].IsPair = DogList[_secondDogIndex].IsPair = true;

        Instance.DogList[_firstDogIndex].PairDogName = DogList[_secondDogIndex].Name;
        Instance.DogList[_secondDogIndex].PairDogName = DogList[_firstDogIndex].Name;
    }

    public int FindDogIndex(int DogId) // Find the dog index in Dog list from pair dictionary
    {
        for (var i = 0; i < DogList.Count; i++)
            if (DogList[i].DogID == DogId)
                return i;

        return 0;
    }

    public DogStatus GetDog(int DogId) {
        for (var i = 0; i < DogList.Count; i++)
            if (DogList[i].DogID == DogId)
                return DogList[i];
        return null;
    }

    private int FindKey(int value) // Find the key in pair dictionary
    {
        foreach (var key in DogPairDic.Keys)
            if (DogPairDic[key] == value)
                return key;

        return 0;
    }

    public void
        RemovePairFromDic(int FirstDogId, int SecondDogId) // Remove pair first, incase the dog has already paired.
    {
        var FirstDogIndex = FindDogIndex(FirstDogId);
        var SecondDogIndex = FindDogIndex(SecondDogId);

        if (DogPairDic.ContainsKey(FirstDogId)) {
            DogList[FindDogIndex(DogPairDic[FirstDogId])].PairDogName = "";
            DogList[FindDogIndex(DogPairDic[FirstDogId])].IsPair = false;
            DogPairDic.Remove(FirstDogId);
        }
        else if (DogPairDic.ContainsValue(FirstDogId)) {
            DogList[FindDogIndex(FindKey(FirstDogId))].PairDogName = "";
            DogList[FindDogIndex(FindKey(FirstDogId))].IsPair = false;
            DogPairDic.Remove(FindKey(FirstDogId));
        }

        DogList[FirstDogIndex].PairDogName = "";
        DogList[FirstDogIndex].IsPair = false;

        if (DogPairDic.ContainsKey(SecondDogId)) {
            DogList[FindDogIndex(DogPairDic[SecondDogId])].PairDogName = "";
            DogList[FindDogIndex(DogPairDic[SecondDogId])].IsPair = false;
            DogPairDic.Remove(SecondDogId);
        }
        else if (DogPairDic.ContainsValue(SecondDogId)) {
            DogList[FindDogIndex(FindKey(SecondDogId))].PairDogName = "";
            DogList[FindDogIndex(FindKey(SecondDogId))].IsPair = false;
            DogPairDic.Remove(FindKey(SecondDogId));
        }

        DogList[SecondDogIndex].PairDogName = "";
        DogList[SecondDogIndex].IsPair = false;
    }

    public void NewDay() {
        DogManager.Instance.GenerateNewDogs();
        DogPairDic.Clear();
        for (var i = 0; i < Instance.DogList.Count; i++) {
            Instance.DogList[i].IsPair = false;
            Instance.DogList[i].PairDogName = "";
            Instance.DogList[i].DebuffEffected = false;
        }
    }

    public void OpenRentReminderUI() {
        CameraMovement.Instance._moveMode = false;
        RentReminderUI.SetActive(true);
    }
}