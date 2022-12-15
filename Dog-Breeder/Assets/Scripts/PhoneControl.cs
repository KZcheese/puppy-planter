using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneControl : MonoBehaviour
{
    public GameObject MainScreen,PairScreen;
    public Text PairListText, DogName, UpdatePenText;
    public int UpdatePenCost;
    // Start is called before the first frame update

    public static PhoneControl Instance;
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
        ShowPairList();

        UpdatePenText.text = "You have "+ GameManager.Instance.DogList.Count+ "/"+ GameManager.Instance.MaxDogNumber+ " Dogs\nSpend " + UpdatePenCost.ToString("c2") + " to update pen";
        
    }



    public void GoBackMainScreenButton()
    {
        PairScreen.SetActive(false);
        MainScreen.SetActive(true);

    }
    
    public void PairScreenButton()
    {
        List<Dropdown.OptionData> maleDogList = new List<Dropdown.OptionData>();
        List<Dropdown.OptionData> femaleDogList = new List<Dropdown.OptionData>();

        for (int i = 0; i < GameManager.Instance.DogList.Count; i++)
        {
            DogStatus dog = GameManager.Instance.DogList[i];
            if (dog.isAdult)
            {
                if (dog.gender)
                {
                    maleDogList.Add(new Dropdown.OptionData(dog.Name + " " + dog.DogID));
                }
                else
                {
                    femaleDogList.Add(new Dropdown.OptionData(dog.Name + " " + dog.DogID));
                }
            }
        }
        for (int i = 0; i < 2; i++)
        {
            PairScreen.transform.GetChild(i).GetComponent<Dropdown>().ClearOptions();
        }
        PairScreen.transform.GetChild(0).GetComponent<Dropdown>().AddOptions(maleDogList);
        PairScreen.transform.GetChild(1).GetComponent<Dropdown>().AddOptions(femaleDogList);

        MainScreen.SetActive(false);
        PairScreen.SetActive(true);
    }

    public void PairDogButton()
    {
        int _firstDropDownValue = PairScreen.transform.GetChild(0).GetComponent<Dropdown>().value;
        int _secondDropDownValue = PairScreen.transform.GetChild(1).GetComponent<Dropdown>().value;

        int _firstDogID = int.Parse(PairScreen.transform.GetChild(0).GetComponent<Dropdown>().options[_firstDropDownValue].text.Split(' ')[1]);
        int _secondDogID = int.Parse(PairScreen.transform.GetChild(1).GetComponent<Dropdown>().options[_secondDropDownValue].text.Split(' ')[1]);
        if(GameManager.Instance.DogList.Count > GameManager.Instance.MaxDogNumber)
        {
            Debug.Log("You only can have "+ GameManager.Instance.MaxDogNumber + " dogs, cant pair now, sell some dogs");
        }
        else
        {
            if (_firstDogID == _secondDogID || GameManager.Instance.GetDog(_firstDogID) == null || GameManager.Instance.GetDog(_secondDogID) == null)
                Debug.Log("Invalid pair");
            else
            {
                Debug.Log("Pair Succeed");
                GameManager.Instance.DogPaired(_firstDogID, _secondDogID);
                GameManager.Instance.GetDog(_firstDogID).PairDogName = DogName.text;
                GameManager.Instance.GetDog(_secondDogID).PairDogName = DogName.text;
            }
        }


    }

    public void ShowPairList()
    {
        string _pairDogList = "";
        foreach (var key in GameManager.Instance.DogPairDic.Keys)
        {
            string _pairDogName1 = GameManager.Instance.DogList[GameManager.Instance.FindDogIndex(key)].Name;
            string _pairDogName2 = GameManager.Instance.DogList[GameManager.Instance.FindDogIndex(GameManager.Instance.DogPairDic[key])].Name;
            string _pairDogs = _pairDogName1 + " pair with " + _pairDogName2 + "\n";
            _pairDogList += _pairDogs;
        }
        PairListText.text = _pairDogList;
    }
     
    public void UpdatePenButton()
    {
        GameManager.Instance.Money -= UpdatePenCost;
        GameManager.Instance.TodayUpdateCost += UpdatePenCost;
        GameManager.Instance.MaxDogNumber += 1;
    }

    public void CloseMainMenu()
    {
        GoBackMainScreenButton();
        GameManager.Instance.IsPhoneActive = false;
        GameManager.Instance.PhoneUI.SetActive(GameManager.Instance.IsPhoneActive);
        CameraMovement.Instance._moveMode = true;
    }
}
