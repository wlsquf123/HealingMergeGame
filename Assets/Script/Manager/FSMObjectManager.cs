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

    private int selectIndex = 0; // 현재 선택한 시설물

    public void AddButton(int index)
    {
        selectIndex = index;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || selectIndex == 0) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 사료와 물 공통 위치 계산 (변수명을 범용적으로 spawnPos로 변경)
            Vector3 spawnPos = hit.point;
            spawnPos.y += 0.5f;

            switch (selectIndex)
            {
                case 1:
                    if (GameManager.instance.UsePoint(10f))
                    {
                        FoodBowl.Add(Instantiate(FoodBowlPrefab, spawnPos, Quaternion.identity));
                    }
                    break;
                case 2:
                    if (GameManager.instance.UsePoint(10f))
                    {
                        WaterBowl.Add(Instantiate(WaterBowlPrefab, spawnPos, Quaternion.identity));
                    }
                    break;
                case 3:
                    if (GameManager.instance.UsePoint(20f))
                    {
                        TreeShades.Add(Instantiate(TreeShadesPrefab, spawnPos, Quaternion.identity));
                    }
                    break;
                case 4:
                    FoodBowl.Add(Instantiate(FoodBowlPrefab, spawnPos, Quaternion.identity));
                    break;
                case 5:
                    WaterBowl.Add(Instantiate(WaterBowlPrefab, spawnPos, Quaternion.identity));
                    break;
            }
            selectIndex = 0; // 설치가 끝나면 선택 해제
        }
    }
}