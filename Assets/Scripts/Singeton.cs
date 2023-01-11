using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T> {
    public static T Instance { get; private set; }


    private void Awake() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = (T)this;
            OnInstanceAwake();
        }
    }

    protected virtual void OnInstanceAwake() {
    }
}