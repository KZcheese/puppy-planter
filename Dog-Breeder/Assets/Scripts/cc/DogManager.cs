using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager
{
    public static DogManager instance;
    public static DogManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DogManager();
            return instance;
        }
    }

    public void GenerateNewDogs()
    {
        foreach (var parents in GameManager.Instance.DogPairDic)
        {
            GameObject newDog = Object.Instantiate(GameManager.Instance.dogPrefab);
            DogStatus newStatus = newDog.GetComponent<DogStatus>();
            SkinnedMeshRenderer newSkin = newDog.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer parSkin1 = GameManager.Instance.GetDog(parents.Key).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer parSkin2 = GameManager.Instance.GetDog(parents.Value).GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < newSkin.sharedMesh.blendShapeCount; i++)
            {
                float min = Mathf.Min(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) - 10;
                float max = Mathf.Max(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) + 10;
                float value = Mathf.Clamp(Random.Range(min, max), 0, 100);
                newSkin.SetBlendShapeWeight(i, Mathf.Round(value / 10) * 10);
            }

            newStatus.gender = Random.value > .5f;
            newStatus.birthday = GameManager.Instance.DayCount;
            newStatus.Name = GameManager.Instance.GetDog(parents.Key).PairDogName;
            newDog.transform.position = GameManager.Instance.bornSpot.position;
            DebuffManager.Instance.InitializeDebuffs(newDog);
        }
    }

    public float CalculateProfit(int id)
    {
        float profit = 0;
        SkinnedMeshRenderer skin = GameManager.Instance.GetDog(id).GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < skin.sharedMesh.blendShapeCount; i++)
        {
            profit += 10 - Mathf.Abs(skin.GetBlendShapeWeight(i) - GameManager.Instance.demands[i]) / 10;
        }
        return profit;
    }

    public void SellDog(int id)
    {
        GameManager.Instance.Money += CalculateProfit(id);
        Object.Destroy(GameManager.Instance.GetDog(id).gameObject);
        GameManager.Instance.DogList.Remove(GameManager.Instance.GetDog(id));
    }
}

//public struct PairInfo
//{
//    public KeyValuePair<int, int> parents;
//    public string name;
//}