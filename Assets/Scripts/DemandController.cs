using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemandController : MonoBehaviour
{
    public Text demandText;
    public static DemandController Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShuffleDemands();
        }
    }

    public void ShuffleDemands()
    {
        for (int i = 0; i < GameManager.Instance.demands.Length; i++)
        {
            int val = Random.Range(0, 11);
            GameManager.Instance.demands[i] = val * 10;
        }
        UpdateDemand();
    }

    void UpdateDemand()
    {
        float[] demands = GameManager.Instance.demands;
        demandText.text = $"<size=30>DEMAND</size>: \nMuscle: {demands[1]}\nEar Shape: {demands[2]}\nEye Size: {demands[3]}\nNose Shape: {demands[4]}";
    }
}
