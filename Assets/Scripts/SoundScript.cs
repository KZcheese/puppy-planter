using UnityEngine;

public class SoundScript : MonoBehaviour {
    public AudioSource dogBark;

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    public void PlaySoundEffect() {
        dogBark.Play();
    }
}