using UnityEngine;
public enum Items
{
    None,
    foodItem,
    waterItem,
    autoAllFoodItem,
    autoAllWaterItem,
    autoAllHpItem,
    autoAllThunderItem
}

public class Item : MonoBehaviour
{
    public Items ItemType;

    private void Start()
    {
        ItemType = (Items)Random.Range(1, 7); // None 제외하고 랜덤 아이템 선택
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0f) return;
        GameManager.instance.inventoryManager.AddItem(ItemType);
        Destroy(gameObject);
    }
}
