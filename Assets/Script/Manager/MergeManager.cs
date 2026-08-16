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
            MergeRanks[i].SetActive(i == rating - 2); // i가 2이고 rating이 2이면 0배열꺼
        }
    }

    public void AnimalClick(GameObject prefab) // 동물 선택
    {
        Instantiate(prefab, mergePosition, Quaternion.identity);
        MergedSelect.SetActive(false);
    }
}