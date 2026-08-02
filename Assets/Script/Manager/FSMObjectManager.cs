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
    public bool isTree = false;
    public bool isFoodItem = false;
    public bool isWaterItem = false;

    public void AddButton(int index)
    {
        switch (index)
        {
            case 1:
                isFood = true;
                break;
            case 2: 
                isWater = true;
                break;
            case 3:
                isTree = true;
                break;
            case 4:
                isFoodItem = true;
                break;
            case 5:
                isWaterItem = true;
                break;

        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (isFood || isWater || isTree || isFoodItem || isWaterItem) )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 사료와 물 공통 위치 계산 (변수명을 범용적으로 spawnPos로 변경)
                Vector3 spawnPos = hit.point;
                spawnPos.y += 0.5f;

                if (isFood)
                {
                    isFood = false;
                    if (GameManager.instance.usePoint(10f))
                    {
                        FoodBowl.Add(Instantiate(FoodBowlPrefab, spawnPos, Quaternion.identity));
                    }
                }
                else if (isWater)
                {
                    isWater = false;
                    if (GameManager.instance.usePoint(10f))
                    {
                        WaterBowl.Add(Instantiate(WaterBowlPrefab, spawnPos, Quaternion.identity));
                    }
                }
                else if (isTree)
                {
                    isTree = false;
                    if (GameManager.instance.usePoint(20f))
                    {
                        TreeShades.Add(Instantiate(TreeShadesPrefab, spawnPos, Quaternion.identity));
                    }
                }
                else if (isFoodItem)
                {
                    isFoodItem = false;
                    FoodBowl.Add(Instantiate(FoodBowlPrefab, spawnPos, Quaternion.identity));
                }
                else if (isWaterItem)
                {
                    isWaterItem = false;
                    WaterBowl.Add(Instantiate(WaterBowlPrefab, spawnPos, Quaternion.identity));
                }
            }
        }
    }
}