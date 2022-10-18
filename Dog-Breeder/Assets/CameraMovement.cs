using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed,RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(0, -MoveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
        }

        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        Vector3 _cameraAngles = transform.eulerAngles;
        _cameraAngles.x -= rv * RotateSpeed;
        _cameraAngles.y += rh * RotateSpeed;
        Camera.main.transform.eulerAngles = _cameraAngles;
    }
}
