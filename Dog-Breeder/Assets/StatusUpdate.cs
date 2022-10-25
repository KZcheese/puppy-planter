using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdate : MonoBehaviour
{
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
        for(int i = 0; i < GameManager.Instance.DogList.Count; i++)
        {
            if (GameManager.Instance.DogList[i].CanBeChecked)
                this.GetComponent<Text>().text = GameManager.Instance.DogList[i].Name;
        }
    }
}
