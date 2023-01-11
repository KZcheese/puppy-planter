using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneControl : MonoBehaviour {
    // Start is called before the first frame update

    public static PhoneControl Instance;
    public GameObject MainScreen, PairScreen;
    public Text PairListText, DogName, UpdatePenText;
    public int UpdatePenCost;

    private void Awake() {
        if (!Instance) Instance = this;
    }

    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        ShowPairList();

        UpdatePenText.text = "You have " + GameManager.Instance.DogList.Count + "/" +
                             GameManager.Instance.MaxDogNumber + " Dogs\nSpend " + UpdatePenCost.ToString("c2") +
                             " to update pen";
    }


    public void GoBackMainScreenButton() {
        PairScreen.SetActive(false);
        MainScreen.SetActive(true);
    }

    public void PairScreenButton() {
        var maleDogList = new List<Dropdown.OptionData>();
        var femaleDogList = new List<Dropdown.OptionData>();

        for (var i = 0; i < GameManager.Instance.DogList.Count; i++) {
            var dog = GameManager.Instance.DogList[i];
            if (dog.isAdult) {
                if (dog.gender)
                    maleDogList.Add(new Dropdown.OptionData(dog.Name + " " + dog.DogID));
                else
                    femaleDogList.Add(new Dropdown.OptionData(dog.Name + " " + dog.DogID));
            }
        }

        for (var i = 0; i < 2; i++) PairScreen.transform.GetChild(i).GetComponent<Dropdown>().ClearOptions();
        PairScreen.transform.GetChild(0).GetComponent<Dropdown>().AddOptions(maleDogList);
        PairScreen.transform.GetChild(1).GetComponent<Dropdown>().AddOptions(femaleDogList);

        MainScreen.SetActive(false);
        PairScreen.SetActive(true);
    }

    public void PairDogButton() {
        var _firstDropDownValue = PairScreen.transform.GetChild(0).GetComponent<Dropdown>().value;
        var _secondDropDownValue = PairScreen.transform.GetChild(1).GetComponent<Dropdown>().value;

        var _firstDogID = int.Parse(PairScreen.transform.GetChild(0).GetComponent<Dropdown>()
            .options[_firstDropDownValue].text.Split(' ')[1]);
        var _secondDogID = int.Parse(PairScreen.transform.GetChild(1).GetComponent<Dropdown>()
            .options[_secondDropDownValue].text.Split(' ')[1]);
        if (GameManager.Instance.DogList.Count > GameManager.Instance.MaxDogNumber) {
            Debug.Log("You only can have " + GameManager.Instance.MaxDogNumber +
                      " dogs, cant pair now, sell some dogs");
        }
        else {
            if (_firstDogID == _secondDogID || GameManager.Instance.GetDog(_firstDogID) == null ||
                GameManager.Instance.GetDog(_secondDogID) == null) {
                Debug.Log("Invalid pair");
            }
            else {
                Debug.Log("Pair Succeed");
                GameManager.Instance.DogPaired(_firstDogID, _secondDogID);
                GameManager.Instance.GetDog(_firstDogID).PairDogName = DogName.text;
                GameManager.Instance.GetDog(_secondDogID).PairDogName = DogName.text;
            }
        }
    }

    public void ShowPairList() {
        var _pairDogList = "";
        foreach (var key in GameManager.Instance.DogPairDic.Keys) {
            var _pairDogName1 = GameManager.Instance.DogList[GameManager.Instance.FindDogIndex(key)].Name;
            var _pairDogName2 = GameManager.Instance
                .DogList[GameManager.Instance.FindDogIndex(GameManager.Instance.DogPairDic[key])].Name;
            var _pairDogs = _pairDogName1 + " pair with " + _pairDogName2 + "\n";
            _pairDogList += _pairDogs;
        }

        PairListText.text = _pairDogList;
    }

    public void UpdatePenButton() {
        if (GameManager.Instance.MaxDogNumber + 1 <= GameManager.Instance.MaxPenCap &&
            GameManager.Instance.Money - UpdatePenCost > 0) {
            GameManager.Instance.Money -= UpdatePenCost;
            GameManager.Instance.TodayUpdateCost += UpdatePenCost;
            GameManager.Instance.MaxDogNumber += 1;
            UpdatePenCost = UpdatePenCost * 2;
        }
    }

    public void CloseMainMenu() {
        GoBackMainScreenButton();
        GameManager.Instance.IsPhoneActive = false;
        GameManager.Instance.PhoneUI.SetActive(GameManager.Instance.IsPhoneActive);
        CameraMovement.Instance._moveMode = true;
    }
}