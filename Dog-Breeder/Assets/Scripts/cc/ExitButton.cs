using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Canvas DogStatusUI;
    public void ExitfromUI()
    {
        DogStatusUI.transform.gameObject.SetActive(false);
    }
}
