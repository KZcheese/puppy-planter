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
        if (GetComponent<DogStatus>().isAdult)
        {
            if (!_navigation.enabled)
            {
                _navigation.enabled = true;
            }
            AutoMove();
        }
        else
        {
            if (_navigation.enabled)
            {
                _navigation.enabled = false;
            }
        }
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
                NeedNewPositon = false;
                Invoke("GoNewPosition", WaitTime);
                GetComponent<Animator>().SetBool("Walk", false);
                
            }
        }

        /*if (this.GetComponent<DogStatus>().CheckStatusNow)
            _navigation.speed = 0;
        else
            _navigation.speed = 1;*/
    }

    public void GoNewPosition()
    {

        float _randomX = Random.Range(-3.5f, 3.5f);
        while (_randomX < -2f)
            _randomX = Random.Range(-3.5f, 3.5f);
        
            

        float _randomZ = Random.Range(-3.5f, 3.5f);
        while ((_randomZ < -2.6f)&& (_randomZ > 1f))
            _randomZ = Random.Range(-3.5f, 3.5f);

        Positions = new Vector3(_randomX, transform.position.y, _randomZ);

        NeedNewPositon = true;
        GetComponent<Animator>().SetBool("Walk", true);
    }
}