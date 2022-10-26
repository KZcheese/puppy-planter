using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaySwitchControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TransportScene;
    public Text DayCountText;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNextDayButton()
    {
        TransportScene.SetActive(true);
        GameManager.Instance.DayCount += 1;
        DayCountText.text = "Day: "+ GameManager.Instance.DayCount + " Week: " + GameManager.Instance.WeekCount + " Month: " + GameManager.Instance.MonthCount;

        GameManager.Instance.Money -= GameManager.Instance.CostPerDay;

        if(GameManager.Instance.DayCount == 5)
        {
            GameManager.Instance.DayCount = 0;
            GameManager.Instance.WeekCount += 1;
            GameManager.Instance.Money -= GameManager.Instance.CostPerWeak;
        }

        if (GameManager.Instance.WeekCount == 4)
        {
            GameManager.Instance.MonthCount += 1;
            GameManager.Instance.WeekCount = 0;
            GameManager.Instance.Money -= GameManager.Instance.CostPerMonth;
        }



        Time.timeScale = 0;
    }

    public void GoContinueButton()
    {
        Time.timeScale = 1;
        TransportScene.SetActive(false);
        GameManager.Instance.NewDay();
    }
}
