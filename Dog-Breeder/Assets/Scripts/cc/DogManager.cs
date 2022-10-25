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
            SkinnedMeshRenderer parSkin1 = GameManager.Instance.GetDog(parents.Key).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer parSkin2 = GameManager.Instance.GetDog(parents.Value).GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < newSkin.sharedMesh.blendShapeCount; i++)
            {
                float min = Mathf.Min(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) - 10;
                float max = Mathf.Max(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) + 10;
                float value = Mathf.Clamp(Random.Range(min, max), 0, 100);
                newSkin.SetBlendShapeWeight(i, value);
            }

            //TODO
            newSkin.GetComponent<DogStatus>().birthday = GameManager.Instance.DayCount;
            newSkin.GetComponent<DogStatus>().Name = GameManager.Instance.GetDog(parents.Key).PairDogName;
            newSkin.transform.position = GameManager.Instance.bornSpot.position;
        }
    }

    public float CalculateProfit(int id)
    {
        float profit = 0;
        SkinnedMeshRenderer skin = GameManager.Instance.GetDog(id).GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            profit += Mathf.Abs(skin.GetBlendShapeWeight(i) - GameManager.Instance.demands[i]);
        }
        profit /= 10;
        return profit;
    }

    public void SellDog(int id)
    {
        GameManager.Instance.Money += CalculateProfit(id);
        Destroy(GameManager.Instance.GetDog(id).gameObject);
        GameManager.Instance.DogList.Remove(GameManager.Instance.GetDog(id));
    }
}

//public struct PairInfo
//{
//    public KeyValuePair<int, int> parents;
//    public string name;
//}