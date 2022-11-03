using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Positions;
    private bool NeedNewPositon = true;
    private NavMeshAgent _navigation;
    public float WaitTime = 0;
    void Start()
    {
        _navigation = GetComponent<NavMeshAgent>();
        Positions = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AutoMove();

    }

    void AutoMove()
    {
        if (Vector3.Distance(transform.position, Positions) >= 0.1f)
        {
            _navigation.destination = Positions;
        }

        if (Vector3.Distance(transform.position, Positions) < 0.5f)
        {
            if (NeedNewPositon)
            {
                Invoke("GoNewPosition", WaitTime);
                NeedNewPositon = false;
            }


        }

        if (this.GetComponent<DogStatus>().CheckStatusNow)
            _navigation.speed = 0;
        else
            _navigation.speed = 1;
    }

    void GoNewPosition()
    {

        float _randomX = Random.Range(-3.5f, 3.5f);
        float _randomZ = Random.Range(-3.5f, 3.5f);

        Positions = new Vector3(_randomX, 0.36f, _randomZ);

        NeedNewPositon = true;
    }
}