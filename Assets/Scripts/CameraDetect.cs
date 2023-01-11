using UnityEngine;

public class CameraDetect : MonoBehaviour {
    public static CameraDetect Instance;
    public LayerMask LayerDetect;
    public float DetectDistance;
    public float doorDetectableDistance;
    public GameObject CMVcam1, CMVcam2, ModifierTable;


    public GameObject Dog;

    // Start is called before the first frame update

    private void Awake() {
        if (!Instance) Instance = this;
    }

    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        Detect();
    }

    private void Detect() {
        var _cameraRay = new Ray(transform.position, transform.forward);
        RaycastHit _cameraHit;
        if (Physics.Raycast(_cameraRay, out _cameraHit, DetectDistance, LayerDetect)) {
            _cameraHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 4;
            if (Input.GetMouseButtonDown(0) && DaySwitchControl.Instance.TransportScene.activeSelf == false &&
                GameManager.Instance.IsPhoneActive == false) {
                AudioMgr.Instance.PlayFx(AudioFxType.DogBark);
                Dog = _cameraHit.collider.gameObject;

                CMVcam1.SetActive(true);
                ModifierTable.SetActive(true);
                GameManager.Instance.IsStatusActive = true;
                GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
                StatusUpdate.Instance.Dog = _cameraHit.collider.gameObject.GetComponent<DogStatus>();
                StatusUpdate.Instance.DogId = _cameraHit.collider.gameObject.GetComponent<DogStatus>().DogID;
                StatusUpdate.Instance.ProfitText.text = StatusUpdate.Instance.Dog.GetDescription();
                DogModifier.Instance.Dog = _cameraHit.collider.gameObject.GetComponent<DogStatus>();
                StatusUpdate.Instance.UpdateStatusText();
                _cameraHit.collider.gameObject.GetComponent<DogStatus>().CheckStatusNow = true;
                _cameraHit.collider.gameObject.SetActive(false);

                for (var i = 1;
                     i < ModifierTable.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh.blendShapeCount;
                     i++) //Make the dog in modifier table looks like same as dog we pick
                    ModifierTable.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(i,
                        _cameraHit.collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>()
                            .GetBlendShapeWeight(i));

                CameraMovement.Instance._moveMode = false;
            }
        }
        else if (Physics.Raycast(_cameraRay, out _cameraHit, doorDetectableDistance, LayerMask.GetMask("NoteBook"))) {
            _cameraHit.collider.gameObject.GetComponentInParent<Outline>().OutlineWidth = 10;
            if (Input.GetMouseButtonDown(0)) {
                GameManager.Instance.OpenRentReminderUI();
                CameraMovement.Instance._moveMode = false;
            }
        }
        else {
            foreach (var dogs in GameManager.Instance.DogList) dogs.GetComponent<Outline>().OutlineWidth = 0;

            GameObject.Find("NoteBook").GetComponent<Outline>().OutlineWidth = 0;
        }

        if (Physics.Raycast(_cameraRay, out _cameraHit, doorDetectableDistance, LayerMask.GetMask("Door"))) {
            _cameraHit.collider.gameObject.GetComponentInParent<Outline>().OutlineWidth = 10;
            if (Input.GetMouseButtonDown(0) && !DaySwitchControl.Instance.TransportScene.activeSelf &&
                !GameManager.Instance.IsPhoneActive) DaySwitchControl.Instance.GoNextDayButton();
        }
        else {
            GameObject.Find("Door").GetComponent<Outline>().OutlineWidth = 0;
        }
    }
}