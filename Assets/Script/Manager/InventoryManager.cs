using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("인벤토리 설정")]
    public int maxSlots = 7;
    public int maxStack = 30;

    public Items[] slots;
    public int[] slotCounts;

    [Header("슬롯 UI")]
    public Text[] slotTexts;
    public Button[] slotButtons;

    private void Awake()
    {
        slots = new Items[maxSlots];
        slotCounts = new int[maxSlots];
    }

    private void Start()
    {
        for (int i = 0; i < maxSlots; i++)
            UpdateSlotUI(i);
    }

    // 아이템 획득
    public bool AddItem(Items item)
    {
        if (item == Items.None) return false;

        // 1. 이미 같은 아이템을 가지고 있는지 먼저 확인
        for (int i = 0; i < maxSlots; i++)
        {
            if (slots[i] == item)
            {
                if (slotCounts[i] >= maxStack) return false; // 최대치 초과

                slotCounts[i]++;
                UpdateSlotUI(i);
                return true;
            }
        }

        // 2. 같은 아이템이 없다면, 앞에서부터 빈 슬롯 찾기
        for (int i = 0; i < maxSlots; i++)
        {
            if (slots[i] == Items.None)
            {
                slots[i] = item;
                slotCounts[i] = 1;
                UpdateSlotUI(i);
                return true;
            }
        }

        return false; // 인벤토리가 가득 참
    }

    // 슬롯 버튼 클릭 (아이템 소비)
    public void OnClickSlot(int index) => RemoveItem(index);

    // 아이템 1개 감소
    public bool RemoveItem(int index)
    {
        if (slots[index] == Items.None) return false;

        slotCounts[index]--; // 수량 1 감소

        // 감소 후 수량이 0이 되면 슬롯 비우기
        if (slotCounts[index] <= 0)
        {
            slots[index] = Items.None;
            slotCounts[index] = 0;
        }

        UpdateSlotUI(index);
        return true;
    }

    // 슬롯 UI 갱신
    private void UpdateSlotUI(int index)
    {
        // 아이템이 있는지 여부만 확인 (수량이 0이면 RemoveItem에서 None으로 바꾸므로 이것만 체크해도 충분함)
        bool hasItem = slots[index] != Items.None;

        slotButtons[index].gameObject.SetActive(hasItem);
        slotTexts[index].text = hasItem ? $"{GetItemNameKor(slots[index])}\nx{slotCounts[index]}" : "";
    }

    // 선호하시는 switch문 형태 (가독성을 위해 한 줄 배치)
    private string GetItemNameKor(Items item)
    {
        switch (item)
        {
            case Items.foodItem: return "먹이";
            case Items.waterItem: return "물";
            case Items.autoAllFoodItem: return "포만 회복";
            case Items.autoAllWaterItem: return "수분 회복";
            case Items.autoAllHpItem: return "체력 회복";
            case Items.autoAllThunderItem: return "천둥 방어";
            default: return "";
        }
    }
}