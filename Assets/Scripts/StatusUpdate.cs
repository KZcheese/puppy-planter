using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUpdate : MonoBehaviour
{
    public int index = -1;
    public int DogId;
   /* public Text statusText;*/
    public TextMeshProUGUI doggyname;
    public TextMeshProUGUI doggyage;
    public TextMeshProUGUI doggygen;
    public TextMeshProUGUI earSize;
    public TextMeshProUGUI eyeSize;
    public TextMeshProUGUI muscleSize;
    public TextMeshProUGUI noseSize;
    public static StatusUpdate Instance;
    public DogStatus Dog;
    public Text ProfitText;
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

    public void UpdateStatusText()
    {
       /* statusText.text = Dog.GetDescription();*/
        doggyname.text = Dog.Dog_Name();
        doggyage.text = Dog.Dog_Age();
        doggygen.text = Dog.Dog_Gen();
        earSize.text = Dog.DogEar_Size();
        eyeSize.text = Dog.DogEye_Size();
        muscleSize.text = Dog.DogMuscle_Size();
        noseSize.text = Dog.DogNose_Size();

    }

    public void Reset()
    {
        index = -1;
    }

    public void SellDog()
    {
        if (GameManager.Instance.DogList.Count > 2)
        {
            DogManager.Instance.SellDog(DogId);
            GameManager.Instance.IsStatusActive = !GameManager.Instance.IsStatusActive;
            index = -1;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Only 2 dog left, you should not sell them");
        }
    }

    public void ExitStatusScene()
    {
        GameManager.Instance.IsStatusActive = false;
        GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
        Dog.CheckStatusNow = false;
        CameraMovement.Instance._moveMode = true;
    }
}
