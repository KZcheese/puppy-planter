using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int DogIndex;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(CheckDogStatusButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckDogStatusButton()
    {
        Debug.Log(GameManager.Instance.DogList[DogIndex].Name);
        ///GameManager.Instance.DogList[]
    }
}
