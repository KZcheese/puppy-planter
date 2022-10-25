using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemandController : MonoBehaviour
{
    public Text demandText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShuffleDemands();
            UpdateDemand();
        }
    }

    void ShuffleDemands()
    {
        for (int i = 0; i < GameManager.Instance.demands.Length; i++)
        {
            float val = Random.Range(0, 100);
            GameManager.Instance.demands[i] = val;
        }
    }

    void UpdateDemand()
    {
        float[] demands = GameManager.Instance.demands;
        demandText.text = $"demands: \ncheek_droopy: {demands[0]}\neye_small: {demands[1]}\neye_big: {demands[2]}\near_droopy: {demands[3]}\near_pointy: {demands[4]}\nleg_shorter: {demands[5]}\nleg_longer: {demands[6]}\nmuscle_small: {demands[7]}\nmuscle_big: {demands[8]}\nnose_small: {demands[9]}\nnose_big: {demands[10]}";
    }
}
