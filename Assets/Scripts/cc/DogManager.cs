using UnityEngine;

public class DogManager {
    public static DogManager instance;

    public static DogManager Instance {
        get {
            if (instance == null)
                instance = new DogManager();
            return instance;
        }
    }

    public void GenerateNewDogs() {
        foreach (var parents in GameManager.Instance.DogPairDic) {
            var newDog = Object.Instantiate(GameManager.Instance.dogPrefab);
            var newStatus = newDog.GetComponent<DogStatus>();
            var newSkin = newDog.GetComponentInChildren<SkinnedMeshRenderer>();
            var parSkin1 = GameManager.Instance.GetDog(parents.Key).GetComponentInChildren<SkinnedMeshRenderer>();
            var parSkin2 = GameManager.Instance.GetDog(parents.Value).GetComponentInChildren<SkinnedMeshRenderer>();
            for (var i = 0; i < newSkin.sharedMesh.blendShapeCount; i++) {
                var min = Mathf.Min(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) - 10;
                var max = Mathf.Max(parSkin1.GetBlendShapeWeight(i), parSkin2.GetBlendShapeWeight(i)) + 10;
                var value = Mathf.Clamp(Random.Range(min, max), 0, 100);
                newSkin.SetBlendShapeWeight(i, value);
            }

            newStatus.HP = 100;
            newStatus.gender = Random.value > .5f;
            newStatus.birthday = GameManager.Instance.DayCount;
            newStatus.isAdult = false;
            newStatus.Name = GameManager.Instance.GetDog(parents.Key).PairDogName;
            newDog.transform.position = GameManager.Instance.bornSpot.position;
            newDog.transform.localScale = new Vector3(.5f, .5f, .5f);
            DebuffManager.Instance.InitializeDebuffs(newDog);
        }
    }

    public float CalculateProfit(int id) {
        float profit = 0;
        var skin = GameManager.Instance.GetDog(id).GetComponentInChildren<SkinnedMeshRenderer>();
        for (var i = 1; i < skin.sharedMesh.blendShapeCount; i++)
            profit += 10 - Mathf.Abs(skin.GetBlendShapeWeight(i) - GameManager.Instance.demands[i]) / 10;
        return Mathf.Round(profit);
    }

    public void SellDog(int id) {
        GameManager.Instance.Money += CalculateProfit(id);
        Object.Destroy(GameManager.Instance.GetDog(id).gameObject);
        GameManager.Instance.DogList.Remove(GameManager.Instance.GetDog(id));
        ExitButton.Instance.ExitfromUI();
    }
}

//public struct PairInfo
//{
//    public KeyValuePair<int, int> parents;
//    public string name;
//}