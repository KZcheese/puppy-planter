using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaySwitchControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TransportScene;
    public GameObject MoneyCal, DebuffCal;
    public Text DayCountText;

    public Text DayText,SavingText,CostText,FinalValueText,DebText;

    public string TransCostText = "";
    public string DebuffText = "";

    public static DaySwitchControl Instance;
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
        if (TransportScene.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoneyCal.SetActive(false);
                DebuffCal.SetActive(true);
            }
        }
    }

    public void GoNextDayButton()
    {

        GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive = false);
        GameManager.Instance.PhoneUI.SetActive(GameManager.Instance.IsPhoneActive = false);

       

        TransCostText = "";
        DebuffText = "";
        TransportScene.SetActive(true);
        GameManager.Instance.DayCount += 1;
        

        DayText.text = "DAY " + GameManager.Instance.DayCount;
        SavingText.text = "Savings                 " + GameManager.Instance.YesterdayMoney.ToString("c2") + "\n" ;
        SavingText.text += "Sales                        " + (GameManager.Instance.Money - GameManager.Instance.YesterdayMoney).ToString("c2");

        CostCalculate();
        FinalValueText.text = "------------------------------------------------------------\n                                                      " + GameManager.Instance.Money.ToString("c2");


        for (int i = 0;i< GameManager.Instance.DogList.Count; i++)
            GameManager.Instance.DogList[i].NewDay();

        DemandController.Instance.ShuffleDemands();

        GameManager.Instance.NewDay();
        DebText.text = DebuffText;

        Time.timeScale = 0;
    }

    void CostCalculate()
    {
        GameManager.Instance.Money -= GameManager.Instance.CostPerDay;
        TransCostText += "Food                       -" + GameManager.Instance.CostPerDay.ToString("c2") + "\n";
        if (GameManager.Instance.DayCount % 6 == 0)
        {
            GameManager.Instance.WeekCount += 1;
            GameManager.Instance.Money -= GameManager.Instance.CostPerWeak;
            TransCostText += "Rent                       -" + GameManager.Instance.CostPerWeak.ToString("c2") + "\n";

            foreach(var dog in GameManager.Instance.DogList)
            {
                dog.HP -= 1;
            }
           
        }

        if (GameManager.Instance.WeekCount % 5 == 0)
        {
            GameManager.Instance.MonthCount += 1;
            GameManager.Instance.Money -= GameManager.Instance.CostPerMonth;
            TransCostText += "Rent/Month:           -" + GameManager.Instance.CostPerMonth.ToString("c2") + "\n";
        }

        CostText.text = TransCostText;
    }
    public void GoContinueButton()
    {
        Time.timeScale = 1;
        TransportScene.SetActive(false);
        GameManager.Instance.YesterdayMoney = GameManager.Instance.Money;
        int _dayAll = GameManager.Instance.DayCount;
        
        int _weekDay = _dayAll % 5;
        switch (_weekDay)
        {
            case 1:
                DayCountText.text = "MONDAY";
                break;

            case 2:
                DayCountText.text = "TUESDAY";
                break;

            case 3:
                DayCountText.text = "WEDNESDAY";
                break;

            case 4:
                DayCountText.text = "THURSDAY";
                break;

            case 0:
                DayCountText.text = "FRIDAY";
                break;

        }
        DayCountText.text += "\n<size=56>DAY " + _dayAll+ "</size>";
        MoneyCal.SetActive(true);
        DebuffCal.SetActive(false);
        PhoneControl.Instance.PairScreenButton();


    }
}