using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject dogPrefab; // 개 프리팹


    public void AnimalAdd() // 동물 생성 버튼
    {
        float randomX = Random.Range(-10f, 10f); // 랜덤 위치 X
        float randomY = Random.Range(-10f, 10f); // 랜덤 위치 Y
        float randomRotY = Random.Range(0, 360f);

        Vector3 spawnPos = new Vector3(randomX, 0, randomY);
        Quaternion spawnRot = Quaternion.Euler(0, randomRotY, 0);

        Instantiate(dogPrefab, spawnPos, spawnRot); // 프리팹 생성
    }
}
