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
        RentFeeText.text = "This is a reminder that your rent of " + GameManager.Instance.CostPerMonth.ToString("c3") + " at the end of the month.\n" + (30 - GameManager.Instance.DayCount) + " days remain.";
    }

    public void Quit()
    {
        this.gameObject.SetActive(false);
        CameraMovement.Instance._moveMode = true;
    }
}
