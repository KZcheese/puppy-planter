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
        DayCountText.text = ""+ GameManager.Instance.DayCount;
        Time.timeScale = 0;
    }

    public void GoContinueButton()
    {
        Time.timeScale = 1;
        TransportScene.SetActive(false);
    }
}
