using UnityEngine;

public class ExitButton : MonoBehaviour {
    public static ExitButton Instance;

    private void Awake() {
        if (!Instance) Instance = this;
    }

    public void ExitfromUI() {
        GameManager.Instance.IsStatusActive = false;
        CameraMovement.Instance._moveMode = true;
        StatusUpdate.Instance.Dog.CheckStatusNow = false;

        CameraDetect.Instance.CMVcam2.SetActive(true);
        CameraDetect.Instance.CMVcam1.SetActive(false);
        CameraDetect.Instance.ModifierTable.SetActive(false);
        Invoke("CloseCMVcam", 3f);
        GameManager.Instance.StatusUI.SetActive(GameManager.Instance.IsStatusActive);
        CameraDetect.Instance.Dog.SetActive(true);
        //Camera.main.transform.rotation= Quaternion.Euler(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, 0);
    }

    private void CloseCMVcam() {
        CameraDetect.Instance.CMVcam2.SetActive(false);
    }
}