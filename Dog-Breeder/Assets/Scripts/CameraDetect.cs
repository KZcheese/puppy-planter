using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraDetect : MonoBehaviour
{
    public LayerMask LayerDetect;
    public float DetectDistance;
    public float doorDetectableDistance;

    public static CameraDetect Instance;

    
    // Start is called before the first frame update

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect();

    }

    void Detect()
    {
        Ray _cameraRay = new Ray(transform.position, transform.forward);
        RaycastHit _cameraHit;
        if (Physics.Raycast(_cameraRay, out _cameraHit, DetectDistance, LayerDetect))
        {
            _cameraHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 4;
            if ((Input.GetMouseButtonDown(0)) && (DaySwitchControl.Instance.TransportScene.activeSelf == false) && (GameManager.Instance.IsPhoneActive == false))
            {

                GameManager.Instance.IsStatusActive = true;
                GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
                StatusUpdate.Instance.Dog = _cameraHit.collider.gameObject.GetComponent<DogStatus>();
                StatusUpdate.Instance.DogId = _cameraHit.collider.gameObject.GetComponent<DogStatus>().DogID;
                DogModifier.Instance.Dog = _cameraHit.collider.gameObject.GetComponent<DogStatus>();
                StatusUpdate.Instance.UpdateStatusText();
                _cameraHit.collider.gameObject.GetComponent<DogStatus>().CheckStatusNow = true;
                CameraMovement.Instance._moveMode = false;
            }
        }else if (Physics.Raycast(_cameraRay, out _cameraHit, doorDetectableDistance, LayerMask.GetMask("NoteBook")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.OpenRentReminderUI();
                CameraMovement.Instance._moveMode = false;
            }
                
        }
        else
        {

            foreach (var dogs in GameManager.Instance.DogList)
            {
                dogs.GetComponent<Outline>().OutlineWidth = 0;

            }
        }

        if (Physics.Raycast(_cameraRay, out _cameraHit, doorDetectableDistance, LayerMask.GetMask("Door")))
        {
            if (Input.GetMouseButtonDown(0) && !DaySwitchControl.Instance.TransportScene.activeSelf && !GameManager.Instance.IsPhoneActive)
            {
                DaySwitchControl.Instance.GoNextDayButton();
            }
        }
    }
}
