using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    private static DogManager instance = null;
    private static readonly object padlock = new object();

    public static DogManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DogManager();
                    }
                }
            }
            return instance;
        }
    }

    public void GenerateNewDogs()
    {
        foreach (var parents in GameManager.Instance.DogPairDic)
        {
            SkinnedMeshRenderer newSkin = Instantiate(GameManager.Instance.dogPrefab).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer parSkin1 = GameManager.Instance.DogList[parents.Key].GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer parSkin2 = GameManager.Instance.DogList[parents.Value].GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < newSkin.sharedMesh.blendShapeCount; i++)
            {
                float min = Mathf.Min(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) - 10;
                float max = Mathf.Max(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) + 10;
                float value = Mathf.Clamp(Random.Range(min, max), 0, 100);
                newSkin.SetBlendShapeWeight(i, value);
            }
        }
    }

    public void SellDogs(params int[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            SkinnedMeshRenderer skin = GameManager.Instance.DogList[ids[i]].GetComponent<SkinnedMeshRenderer>();
            // TODO profit
        }
    }
}
