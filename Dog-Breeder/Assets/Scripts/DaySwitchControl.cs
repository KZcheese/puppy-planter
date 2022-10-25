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
        DayCountText.text = "Day: "+ GameManager.Instance.DayCount + " Weak: " + GameManager.Instance.WeakCount + " Month: " + GameManager.Instance.MonthCount;
        Time.timeScale = 0;

        GameManager.Instance.DogPairDic.Clear();
        for(int i=0;i< GameManager.Instance.DogList.Count; i++)
        {
            GameManager.Instance.DogList[i].IsPair = false;
            GameManager.Instance.DogList[i].PairDogName = "";
        }
        
    }

    public void GoContinueButton()
    {
        Time.timeScale = 1;
        TransportScene.SetActive(false);
    }
}
