using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreInfo : MonoBehaviour
{
    public string url;

    public void Open()
    {
        Application.OpenURL(url);
    }
}
