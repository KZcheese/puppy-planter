using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed,RotateSpeed,Height;

    public bool _moveMode = true;
    private Rigidbody _rigidbody;

    public static CameraMovement Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (!Instance) Instance = this;

    }
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (_moveMode)
        //{
        //    Cursor.visible = false;
        //    if (GameManager.Instance.IsPhoneActive == false)
        //        Movement();
        //}
        //else
        //{
        //    Cursor.visible = true;
        //}

        //if (Input.GetKeyDown(KeyCode.Escape))
        //    _moveMode = !_moveMode;

        Movement();
        transform.position = new Vector3(transform.position.x,Height, transform.position.z);
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


        float _horizontal = Input.GetAxis("Mouse X");
        float _vertical = Input.GetAxis("Mouse Y");

        Vector3 _cameraAngles = transform.eulerAngles;
        _cameraAngles.x -= _vertical * RotateSpeed;
        _cameraAngles.y += _horizontal * RotateSpeed;
        Camera.main.transform.eulerAngles = _cameraAngles;


    }
}
