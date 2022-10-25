using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAutoMove : MonoBehaviour
{
    
    public GameObject[] Positions;

    private NavMeshAgent _navMesh;
    private int _number = 0;

    // Start is called before the first frame update
    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Positions.Length != 0)
        {
            AutoMove();
        }

    }

    void AutoMove()
    {
        if (Vector3.Distance(transform.position, Positions[_number].transform.position) >= 0.2f)
        {
            _navMesh.destination = Positions[_number].transform.position;
        }

        if (Vector3.Distance(transform.position, Positions[_number].transform.position) <= 1f)
        {
            _number++;
            if (_number >= Positions.Length)
                _number = 0;
        }

        if (this.GetComponent<DogStatus>().CheckStatusNow)
        {
            _navMesh.speed = 0;
        }
        else
        {
            _navMesh.speed = 2;
        }
    }

}
