using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("아이템 최대 개수")]
    public int maxStack = 30;

    [Header("아이템 수량")]
    public int foodCount;
    public int waterCount;
    public int autoFoodCount;
    public int autoWaterCount;
    public int autoHpCount;
    public int thunderCount;

    [Header("아이템 버튼")]
    public Button foodButton;
    public Button waterButton;
    public Button autoFoodButton;
    public Button autoWaterButton;
    public Button autoHpButton;
    public Button thunderButton;

    [Header("수량 텍스트")]
    public Text foodCountText;
    public Text waterCountText;
    public Text autoFoodCountText;
    public Text autoWaterCountText;
    public Text autoHpCountText;
    public Text thunderCountText;

    private void Update()
    {
        UpdateInventoryUI();
    }

    // 아이템 획득
    public void AddItem(Items item)
    {
        switch (item)
        {
            case Items.foodItem:
                if (foodCount >= maxStack) return;
                foodCount++;
                break;

            case Items.waterItem:
                if (waterCount >= maxStack) return;
                waterCount++;
                break;

            case Items.autoAllFoodItem:
                if (autoFoodCount >= maxStack) return;
                autoFoodCount++;
                break;

            case Items.autoAllWaterItem:
                if (autoWaterCount >= maxStack) return;
                autoWaterCount++;
                break;

            case Items.autoAllHpItem:
                if (autoHpCount >= maxStack) return;
                autoHpCount++;
                break;

            case Items.autoAllThunderItem:
                if (thunderCount >= maxStack) return;
                thunderCount++;
                break;
        }
    }

    // 모든 버튼과 수량 표시 갱신
    private void UpdateInventoryUI()
    {
        foodButton.gameObject.SetActive(foodCount > 0);
        waterButton.gameObject.SetActive(waterCount > 0);
        autoFoodButton.gameObject.SetActive(autoFoodCount > 0);
        autoWaterButton.gameObject.SetActive(autoWaterCount > 0);
        autoHpButton.gameObject.SetActive(autoHpCount > 0);
        thunderButton.gameObject.SetActive(thunderCount > 0);

        foodCountText.text = "먹이 x" + foodCount;
        waterCountText.text = "물 x" + waterCount;
        autoFoodCountText.text = "전체 먹이 \nx" + autoFoodCount;
        autoWaterCountText.text = "전체 물 \nx" + autoWaterCount;
        autoHpCountText.text = "전체 회복 \nx" + autoHpCount;
        thunderCountText.text = "천둥 방어 \nx" + thunderCount;
    }

    public void UseItem(int index)
    {
        switch (index)
        {
            case 0: // 먹이
                foodCount--;
                break;
            case 1: // 물
                waterCount--;
                break;
            case 2: // 전체 포만도
                autoFoodCount--;
                StartCoroutine(AllRecovery(Items.autoAllFoodItem));
                break;
            case 3: // 전체 수분
                autoWaterCount--;
                StartCoroutine(AllRecovery(Items.autoAllWaterItem));
                break;
            case 4: // 전체 체력
                autoHpCount--;
                StartCoroutine(AllRecovery(Items.autoAllHpItem));
                break;
            case 5: // 천둥 방어
                thunderCount--;
                Animal[] animals = FindObjectsByType<Animal>(FindObjectsSortMode.None);
                foreach (Animal animal in animals)
                {
                    animal.isThunder = true;
                }
                break;
        }
    }
    private IEnumerator AllRecovery(Items item)
    {
        // 60초 동안 반복
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(1f);

            Animal[] animals =
                FindObjectsByType<Animal>(FindObjectsSortMode.None);

            foreach (Animal animal in animals)
            {
                switch (item)
                {
                    case Items.autoAllFoodItem:
                        animal.food =
                            Mathf.Clamp(animal.food + 1f, 0f, 100f);
                        break;

                    case Items.autoAllWaterItem:
                        animal.water =
                            Mathf.Clamp(animal.water + 1f, 0f, 100f);
                        break;

                    case Items.autoAllHpItem:
                        animal.hp =
                            Mathf.Clamp(animal.hp + 1f, 0f, 100f);
                        break;
                }
            }
        }
    }



}