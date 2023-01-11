using UnityEngine;
using UnityEngine.UI;

public class DemandController : MonoBehaviour {
    public static DemandController Instance;
    public Text demandText;

    private void Awake() {
        if (!Instance) Instance = this;
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) ShuffleDemands();
    }

    public void ShuffleDemands() {
        for (var i = 0; i < GameManager.Instance.demands.Length; i++) {
            var val = Random.Range(0, 11);
            GameManager.Instance.demands[i] = val * 10;
        }

        UpdateDemand();
    }

    private void UpdateDemand() {
        var demands = GameManager.Instance.demands;
        demandText.text =
            $"<size=30>DEMAND</size>: \nMuscle: {demands[1]}\nEar Shape: {demands[2]}\nEye Size: {demands[3]}\nNose Shape: {demands[4]}";
    }
}