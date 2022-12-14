using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public void ExitfromUI()
    {
        GameManager.Instance.IsStatusActive = false;
        GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
    }
}
