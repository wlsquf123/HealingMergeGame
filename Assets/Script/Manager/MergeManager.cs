using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public GameObject MergedSelect; // 머지 선택
    public GameObject[] MergeRanks;

    private Vector3 mergePosition;

    public void OpenAnimallists(int rating, Vector3 position)
    {
        MergedSelect.SetActive(true);
        mergePosition = position;

        for (int i = 0; i < MergeRanks.Length; i++)
        {
            MergeRanks[i].SetActive(i == rating - 2); // 무조건 그냥 -2 해주면 댐. i == rating -2 
        }
    }

    public void AnimalClick(GameObject prefab) // 동물 선택 버튼
    {
        Instantiate(prefab, mergePosition, Quaternion.identity);
        MergedSelect.SetActive(false); // 판때기는 끄겠다.
    }
}