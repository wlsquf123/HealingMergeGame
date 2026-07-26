using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FSMObjectManager : MonoBehaviour
{
    public List<GameObject> FoodBowl;
    public List<GameObject> WaterBowl;
    public List<GameObject> TreeShades;

    public GameObject FoodBowlPrefab;
    public GameObject WaterBowlPrefab;
    public GameObject TreeShadesPrefab;

    public bool isFood = false;
    public bool isWater = false;

    public void FoodButton()
    {
        isFood = true;
    }

    public void WaterButton()
    {
        isWater = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (isFood || isWater))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 사료와 물 공통 위치 계산 (변수명을 범용적으로 spawnPos로 변경)
                Vector3 spawnPos = hit.point;
                spawnPos.y += 0.5f;

                if (isFood)
                {
                    FoodBowl.Add(Instantiate(FoodBowlPrefab, spawnPos, Quaternion.identity));
                    isFood = false;
                }
                else if (isWater)
                {
                    WaterBowl.Add(Instantiate(WaterBowlPrefab, spawnPos, Quaternion.identity));
                    isWater = false;
                }
            }
        }
    }
}