using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public AnimalList Obj;
    public void AnimalAdd(int index) // 동물 생성 버튼
    {
        float randomX = Random.Range(-10f, 10f); // 랜덤 위치 X
        float randomZ = Random.Range(0f, 10f); // 랜덤 위치 Z

        Vector3 spawnPos = new Vector3(randomX, 5f, randomZ);

        switch (index)
        {
            case 1: // 강아지
                if (GameManager.instance.usePoint(10f)) // 포인트가 차감되었을 때만!
                {
                    Instantiate(Obj.AnimalLv1[0], spawnPos, transform.rotation);
                }
                break;
            case 2: // 고양이
                if (GameManager.instance.usePoint(10f))
                {
                    Instantiate(Obj.AnimalLv1[1], spawnPos, transform.rotation);
                }
                break;
            case 3:  // 늑대
                if (GameManager.instance.usePoint(50f))
                {
                    Instantiate(Obj.AnimalLv2[0], spawnPos, transform.rotation);
                }
                break;
            case 4: // 염소
                if (GameManager.instance.usePoint(50f))
                {
                    Instantiate(Obj.AnimalLv2[1], spawnPos, transform.rotation);
                }
                break;
            case 5: //사슴
                if (GameManager.instance.usePoint(50f))
                {
                    Instantiate(Obj.AnimalLv2[2], spawnPos, transform.rotation);
                }
                break;
            case 6: // 곰
                if (GameManager.instance.usePoint(300f))
                {
                    Instantiate(Obj.AnimalLv3[0], spawnPos, transform.rotation);
                }
                break;
            case 7: // 호랑이
                if (GameManager.instance.usePoint(300f))
                    Instantiate(Obj.AnimalLv3[1], spawnPos, transform.rotation);
                break;
            case 8: // 말
                if (GameManager.instance.usePoint(300f))
                {
                    Instantiate(Obj.AnimalLv3[2], spawnPos, transform.rotation);
                }
                break;
            case 9: // 코뿔소
                if (GameManager.instance.usePoint(2000f))
                {
                    Instantiate(Obj.AnimalLv4[0], spawnPos, transform.rotation);
                }
                break;
            case 10: // 공룡
                if (GameManager.instance.usePoint(7000f))
                {
                    Instantiate(Obj.AnimalLv5[0], spawnPos, transform.rotation);
                }
                break;
        }
    }
}
