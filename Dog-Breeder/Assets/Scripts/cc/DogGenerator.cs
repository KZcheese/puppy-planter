using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogGenerator : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
            {
                int randomWeight = Random.Range(0, 101);
                smr.SetBlendShapeWeight(i, randomWeight);
            }
        }
    }
}
