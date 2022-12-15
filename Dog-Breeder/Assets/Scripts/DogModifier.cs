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

    public void earInc()
    {
        Dog.Mod_DogEarSizeInc();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void earDec()
    {
        Dog.Mod_DogEarSizeDec();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void eyeInc()
    {
        Dog.Mod_DogEyeSizeInc();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void eyeDec()
    {
        Dog.Mod_DogEyeSizeDec();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void muscleInc()
    {
        Dog.Mod_DogMuscleSizeInc();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void muscleDec()
    {
        Dog.Mod_DogMuscleSizeDec();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void noseInc()
    {
        Dog.Mod_DogNoseSizeInc();
        StatusUpdate.Instance.UpdateStatusText();
    }
    public void noseDec()
    {
        Dog.Mod_DogNoseSizeDec();
        StatusUpdate.Instance.UpdateStatusText();
    }
}
