using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaySwitchControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TransportScene;
    public Text DayCountText;

    public Text TransSceenText;

    public string TransText = "";
    public string DebuffText = "";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNextDayButton()
    {
        TransText = "";
        DebuffText = "";
        TransportScene.SetActive(true);
        GameManager.Instance.DayCount += 1;
        DayCountText.text = "Day: "+ GameManager.Instance.DayCount + " Week: " + GameManager.Instance.WeekCount + " Month: " + GameManager.Instance.MonthCount;

        TransText = "Today is a new day. You cost is : Day Cost: " + GameManager.Instance.CostPerDay + "\n";
        CostCalculate();

        for(int i = 0;i< GameManager.Instance.DogList.Count; i++)
            GameManager.Instance.DogList[i].RandomPosition();

        DemandController.Instance.ShuffleDemands();
        DebuffEffectNow();
        TransSceenText.text = TransText + "\n" + DebuffText;
        Time.timeScale = 0;
    }
    void DebuffEffectNow()
    {
        for (int i = 0; i < GameManager.Instance.DogList.Count; i++)
        {
            if(GameManager.Instance.DogList[i].Debuffs.Count != 0)
            {
                DebuffText += GameManager.Instance.DogList[i].Name + "Debuffs effect: \n";
                for (int z =0;z< GameManager.Instance.DogList[i].Debuffs.Count;z++) 
                {
                    GameManager.Instance.DogList[i].Debuffs[z].Effective(GameManager.Instance.DogList[i]);
                    DebuffText += GameManager.Instance.DogList[i].Debuffs[z].DebuffName;
                }
            }
        }
    }
    void CostCalculate()
    {
        GameManager.Instance.Money -= GameManager.Instance.CostPerDay;

        if (GameManager.Instance.DayCount == 5)
        {
            GameManager.Instance.DayCount = 0;
            GameManager.Instance.WeekCount += 1;
            GameManager.Instance.Money -= GameManager.Instance.CostPerWeak;
            TransText += "Week Cost: " + GameManager.Instance.CostPerWeak + "\n";
        }

        if (GameManager.Instance.WeekCount == 4)
        {
            GameManager.Instance.MonthCount += 1;
            GameManager.Instance.WeekCount = 0;
            GameManager.Instance.Money -= GameManager.Instance.CostPerMonth;
            TransText += "Month Cost: " + GameManager.Instance.CostPerMonth + "\n";
        }
    }
    public void GoContinueButton()
    {
        Time.timeScale = 1;
        TransportScene.SetActive(false);
        GameManager.Instance.NewDay();


        
        
        
    }
}
