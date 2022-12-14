using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DogModifier : MonoBehaviour
{
    public DogStatus Dog;
    public static DogModifier Instance;
    public TextMeshProUGUI _earSize;
    public TextMeshProUGUI _eyeSize;
    public TextMeshProUGUI _muscleSize;
    public TextMeshProUGUI noseSize;
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

    public void earMod()
    {
        Dog.Mod_DogEarSize();
        StatusUpdate.Instance.UpdateStatusText();
    }
}
