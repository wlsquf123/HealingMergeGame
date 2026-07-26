using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FSMObjectManager : MonoBehaviour
{
    public List<GameObject> FoodBowl;
    public GameObject[] asd;
    public GameObject[] WaterBowl;
    public GameObject[] TreeShades;

    public GameObject FoodBowlPrefab;


    public void FoodButton()
    {
        FoodBowl.Add(Instantiate(FoodBowlPrefab));
    }
}