using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed,RotateSpeed;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsPhoneActive == false)
            Movement();
        
        transform.position = new Vector3(transform.position.x,1.25f, transform.position.z);
    }

    void OnTriggerStay(Collider other)
     {
         //For Colliders         
         transform.position += new Vector3(0, 0.2f, -0.2f);    
     }

    void Movement()
    {
        float _horizontalMove = Input.GetAxis("Horizontal");
        float _verticalMove = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))
        {
            _rigidbody.velocity = transform.forward * _verticalMove * MoveSpeed;
        }

        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            _rigidbody.velocity = transform.right * _horizontalMove * MoveSpeed;
        }

        if (Input.GetMouseButton(1))
        {
            float _horizontal = Input.GetAxis("Mouse X");
            float _vertical = Input.GetAxis("Mouse Y");

            Vector3 _cameraAngles = transform.eulerAngles;
            _cameraAngles.x -= _vertical * RotateSpeed;
            _cameraAngles.y += _horizontal * RotateSpeed;
            Camera.main.transform.eulerAngles = _cameraAngles;
        }

    }
}
