using UnityEngine;
using UnityEngine.AI;

public class DogAutoMove : MonoBehaviour {
    // Start is called before the first frame update
    public Vector3 Positions;
    public float WaitTime;
    private NavMeshAgent _navigation;
    private bool init;
    private bool NeedNewPositon = true;

    private void Start() {
        _navigation = GetComponent<NavMeshAgent>();
        _navigation.enabled = false;
    }

    // Update is called once per frame
    private void Update() {
        if (GetComponent<DogStatus>().isAdult) {
            if (GameManager.Instance.IsPhoneActive) {
                _navigation.enabled = false;
                GetComponent<Animator>().SetBool("Walk", false);
            }
            else {
                _navigation.enabled = true;
                AutoMove();
            }

            if (!init) {
                Positions = transform.position;
                init = true;
            }
        }
        else {
            if (_navigation.enabled) _navigation.enabled = false;
        }
    }

    private void AutoMove() {
        if (Vector3.Distance(transform.position, Positions) >= 0.1f) _navigation.destination = Positions;

        if (Vector3.Distance(transform.position, Positions) < 0.5f)
            if (NeedNewPositon) {
                NeedNewPositon = false;
                Invoke("GoNewPosition", WaitTime);
                GetComponent<Animator>().SetBool("Walk", false);
            }

        if (GetComponent<DogStatus>().CheckStatusNow && NeedNewPositon) {
            _navigation.speed = 0;
            GetComponent<Animator>().SetBool("Walk", false);
        }

        else if (!GetComponent<DogStatus>().CheckStatusNow && NeedNewPositon) {
            _navigation.speed = 1;
            GetComponent<Animator>().SetBool("Walk", true);
        }
    }

    public void GoNewPosition() {
        var _randomX = Random.Range(-3.5f, 3.5f);
        while (_randomX < -2f)
            _randomX = Random.Range(-3.5f, 3.5f);


        var _randomZ = Random.Range(-3.5f, 3.5f);
        while (_randomZ < -2.6f && _randomZ > 1f)
            _randomZ = Random.Range(-3.5f, 3.5f);

        Positions = new Vector3(_randomX, transform.position.y, _randomZ);

        NeedNewPositon = true;
        GetComponent<Animator>().SetBool("Walk", true);
    }
}