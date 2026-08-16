using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] Animals;
    public float[] prices;

    public void AnimalAdd(int index) // 동물 생성 버튼
    {
        Vector3 spawnPos = new Vector3(Random.Range(-10f, 10f), 5f, Random.Range(0f, 10f));

        if (GameManager.instance.UsePoint(prices[index]))
        {
            Instantiate(Animals[index], spawnPos, Quaternion.identity);
        }
    }
}