using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RentFeeUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public Text RentFeeText;


    // Update is called once per frame
    void Update()
    {
        RentFeeText.text = "Hello Dear Player, hope everything is well :))\nJust a friendly reminder, you need to pay " + GameManager.Instance.CostPerMonth.ToString("c3") + " after " + (30 - GameManager.Instance.DayCount) + " days.";
    }

    public void Quit()
    {
        this.gameObject.SetActive(false);
        CameraMovement.Instance._moveMode = true;
    }
}
