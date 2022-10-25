using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Positions;

    private NavMeshAgent _navigation;
    private int _moveNumber = 0;
    void Start()
    {
        _navigation = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Positions.Length != 0)
        {
            AutoMove();
        }

    }

    void AutoMove()
    {
        if (Vector3.Distance(transform.position, Positions[_moveNumber].transform.position) >= 0.1f)
        {
            _navigation.destination = Positions[_moveNumber].transform.position;

        }

        if (Vector3.Distance(transform.position, Positions[_moveNumber].transform.position) < 1f)
        {
            _moveNumber++;
            if (_moveNumber == Positions.Length)
                _moveNumber = 0;
        }

        if (this.GetComponent<DogStatus>().CheckStatusNow)
            _navigation.speed = 0;
        else
            _navigation.speed = 2;
    }
}
