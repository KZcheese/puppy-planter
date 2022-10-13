using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogRandom : MonoBehaviour
{
    [Header("Demands")]
    public int eyeShape;
    public int noseShape;
    public int muscle;
    public int legLength;
    [SerializeField]
    public List<DogData> dogDatas = new List<DogData>();

    int count = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomDogGenerator();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RandomDogBreeder();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrintAllDogs();
        }
        DogSellor();
    }

    void RandomDogGenerator()
    {
        DogData dog = new DogData();
        dog.eyeShape = Random.Range(4, 8);
        dog.noseShape = Random.Range(4, 8);
        dog.muscle = Random.Range(4, 8);
        dog.legLength = Random.Range(4, 8);
        dogDatas.Add(dog);
    }

    void RandomDogBreeder()
    {
        DogData par1 = new DogData();
        DogData par2 = new DogData();
        foreach (DogData data in dogDatas)
        {
            if (data.pair)
            {
                if (count == 0)
                {
                    par1 = data;
                }
                else if (count == 1)
                {
                    par2 = data;
                }
                count++;
            }
        }
        if (count == 2)
        {
            DogData dog = new DogData();
            dog.eyeShape = Mathf.Clamp(Random.Range(Mathf.Min(par1.eyeShape, par2.eyeShape) - 1, Mathf.Max(par1.eyeShape, par2.eyeShape) + 2), 0, 10);
            dog.noseShape = Mathf.Clamp(Random.Range(Mathf.Min(par1.noseShape, par2.noseShape) - 1, Mathf.Max(par1.noseShape, par2.noseShape) + 2), 0, 10);
            dog.muscle = Mathf.Clamp(Random.Range(Mathf.Min(par1.muscle, par2.muscle) - 1, Mathf.Max(par1.muscle, par2.muscle) + 2), 0, 10);
            dog.legLength = Mathf.Clamp(Random.Range(Mathf.Min(par1.legLength, par2.legLength) - 1, Mathf.Max(par1.legLength, par2.legLength) + 2), 0, 10);
            dogDatas.Add(dog);
            par1.pair = false;
            par2.pair = false;
            string str = "Parents' traits here:\n";
            str += string.Format("{0}: eyeShape--{1} noseShape--{2} muscle--{3} legLength--{4}\n", par1.name, par1.eyeShape, par1.noseShape, par1.muscle, par1.legLength);
            str += string.Format("{0}: eyeShape--{1} noseShape--{2} muscle--{3} legLength--{4}\n", par2.name, par2.eyeShape, par2.noseShape, par2.muscle, par2.legLength);
            str += "New dog breeded. Make sure to give it a name.\n";
            str += string.Format("New dog's traits here: eyeShape--{0} noseShape--{1} muscle--{2} legLength--{3}", dog.eyeShape, dog.noseShape, dog.muscle, dog.legLength);
            Debug.Log(str);
            count = 0;
        }
        else
        {
            Debug.LogError("Make sure choose only 2 dogs to pair!");
            count = 0;
        }
    }

    void PrintAllDogs()
    {
        if (dogDatas.Count > 0)
        {
            string str = "";
            foreach (DogData data in dogDatas)
            {
                int profit = 40 - Mathf.Abs(eyeShape - data.eyeShape) - Mathf.Abs(noseShape - data.noseShape) - Mathf.Abs(muscle - data.muscle) - Mathf.Abs(legLength - data.legLength);
                str += string.Format("{0}: eyeShape--{1} noseShape--{2} muscle--{3} legLength--{4}, profit if sell it--{5}\n", data.name, data.eyeShape, data.noseShape, data.muscle, data.legLength, profit);
            }
            Debug.Log(str);
        }
    }

    void DogSellor()
    {
        for (int i = dogDatas.Count - 1; i >= 0; i--)
        {
            if (dogDatas[i].sell)
            {
                int profit = 40 - Mathf.Abs(eyeShape - dogDatas[i].eyeShape) - Mathf.Abs(noseShape - dogDatas[i].noseShape) - Mathf.Abs(muscle - dogDatas[i].muscle) - Mathf.Abs(legLength - dogDatas[i].legLength);
                Debug.Log("Profit from selling <" + dogDatas[i].name + ">: " + profit);
                dogDatas.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class DogData
{
    public bool pair;
    public bool sell;

    public string name;
    public int eyeShape;
    public int noseShape;
    public int muscle;
    public int legLength;
}
