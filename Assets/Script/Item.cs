using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
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

    private void OnMouseDown()
    {
        if (Time.timeScale == 0f) return;
        GameManager.instance.inventoryManager.AddItem(ItemType);
        Destroy(gameObject);
    }
}
