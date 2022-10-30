using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdate : MonoBehaviour
{
    public int index = -1;
    public Text statusText;
    public static StatusUpdate Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!Instance) Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateStatusText(DogStatus Dog)
    {
        statusText.text = Dog.GetDescription();
    }
    public void Reset()
    {
        index = -1;
    }

    public void SellDog()
    {
        if (GameManager.Instance.DogList.Count > 2)
        {
            DogManager.Instance.SellDog(GameManager.Instance.DogList[index].DogID);
            GameManager.Instance.IsStatusActive = !GameManager.Instance.IsStatusActive;
            index = -1;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Only 2 dog left, you should not sell them");
        }
    }
}
