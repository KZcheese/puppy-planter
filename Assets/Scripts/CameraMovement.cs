using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public static CameraMovement Instance;
    public float MoveSpeed, RotateSpeed, Height;

    public bool _moveMode = true;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update

    private void Awake() {
        if (!Instance) Instance = this;
    }

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update() {
        if (_moveMode) {
            Cursor.visible = false;
            if (GameManager.Instance.IsPhoneActive == false)
                Movement();
        }
        else {
            Cursor.visible = true;
        }

        transform.position = new Vector3(transform.position.x, Height, transform.position.z);
    }


    private void Movement() {
        var _horizontalMove = Input.GetAxis("Horizontal");
        var _verticalMove = Input.GetAxis("Vertical");

        _rigidbody.velocity = transform.forward * _verticalMove * MoveSpeed +
                              transform.right * _horizontalMove * MoveSpeed;


        var _horizontal = Input.GetAxis("Mouse X");
        var _vertical = Input.GetAxis("Mouse Y");

        var _cameraAngles = transform.eulerAngles;
        _cameraAngles.x -= _vertical * RotateSpeed;
        _cameraAngles.y += _horizontal * RotateSpeed;
        Camera.main.transform.eulerAngles = _cameraAngles;
    }
}