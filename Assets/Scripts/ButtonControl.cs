using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour {
    // Start is called before the first frame update
    public int DogIndex;

    private void Start() {
        GetComponent<Button>().onClick.AddListener(CheckDogStatusButton);
    }

    // Update is called once per frame
    private void Update() {
    }

    public void CheckDogStatusButton() {
        Debug.Log(GameManager.Instance.DogList[DogIndex].Name);
        ///GameManager.Instance.DogList[]
    }
}