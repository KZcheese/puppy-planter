using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    public LayerMask LayerDetect;
    public float DetectDistance;
    public bool IsDogThere = false;

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
            IsDogThere = true;
            Debug.Log("Dogs Here"); // For test is dog in the view
        }
        else
        {
            IsDogThere = false;
        }
    }
}
