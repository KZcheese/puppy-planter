using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    public LayerMask LayerDetect;
    public float DetectDistance;

    public static CameraDetect Instance;
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
        Detect();

    }

    void Detect()
    {
        Ray _cameraRay = new Ray(transform.position, transform.forward);
        RaycastHit _cameraHit;
        if (Physics.Raycast(_cameraRay, out _cameraHit, DetectDistance, LayerDetect))
        {
            _cameraHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 4;
            if ((Input.GetMouseButtonDown(0)) && (DaySwitchControl.Instance.TransportScene.activeSelf == false))
            {
                GameManager.Instance.IsStatusActive = true;
                GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
                StatusUpdate.Instance.UpdateStatusText(_cameraHit.collider.gameObject.GetComponent<DogStatus>());
                StatusUpdate.Instance.DogId = _cameraHit.collider.gameObject.GetComponent<DogStatus>().DogID;
                _cameraHit.collider.gameObject.GetComponent<DogStatus>().CheckStatusNow = true;
            }
        }
        else
        {

            foreach (var dogs in GameManager.Instance.DogList)
            {
                dogs.GetComponent<Outline>().OutlineWidth = 0;
                dogs.GetComponent<DogStatus>().CheckStatusNow = false;
            }
        }
    }
}
