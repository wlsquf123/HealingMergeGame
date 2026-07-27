using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public AnimalList Obj;
    public void AnimalAdd(int index) // 동물 생성 버튼
    {
        var gameManager = GameManager.instance;
        float randomX = Random.Range(-10f, 10f); // 랜덤 위치 X
        float randomY = Random.Range(-10f, 10f); // 랜덤 위치 Y
        Vector3 spawnPos = new Vector3(randomX, 5f, randomY);

        if ((index == 1 || index == 2) && gameManager.point < 10)
        {
            // 부족하다는 메시지 출력
            Debug.Log("포인트 부족");
            return;
        }
        if ((index == 3 || index == 4 || index == 5) && gameManager.point < 50)
        {
            // 부족하다는 메시지 출력
            Debug.Log("포인트 부족");
            return;
        }
        if ((index == 6 || index == 7 || index == 8) && gameManager.point < 300)
        {
            // 부족하다는 메시지 출력
            Debug.Log("포인트 부족");
            return;
        }
        if (index == 9 && gameManager.point < 2000) return;
        if (index == 10 && gameManager.point < 7000) return;


        switch (index)
        {
            case 1: // 강아지
                Instantiate(Obj.AnimalLv1[0], spawnPos, transform.rotation);
                gameManager.point -= 10f;
                break;
            case 2: // 고양이
                Instantiate(Obj.AnimalLv1[1], spawnPos, transform.rotation);
                gameManager.point -= 10f;
                break;
            case 3:  // 늑대
                Instantiate(Obj.AnimalLv2[0], spawnPos, transform.rotation);
                gameManager.point -= 50f;
                break;
            case 4: // 염소
                Instantiate(Obj.AnimalLv2[1], spawnPos, transform.rotation);
                gameManager.point -= 50f;
                break;
            case 5: //사슴
                Instantiate(Obj.AnimalLv2[2], spawnPos, transform.rotation);
                gameManager.point -= 50f;
                break;
            case 6: // 곰
                Instantiate(Obj.AnimalLv3[0], spawnPos, transform.rotation);
                gameManager.point -= 300f;
                break;
            case 7: // 호랑이
                Instantiate(Obj.AnimalLv3[1], spawnPos, transform.rotation);
                gameManager.point -= 300f;
                break;
            case 8: // 말
                Instantiate(Obj.AnimalLv3[2], spawnPos, transform.rotation);
                gameManager.point -= 300f;
                break;
            case 9: // 코뿔소
                Instantiate(Obj.AnimalLv4[0], spawnPos, transform.rotation);
                gameManager.point -= 2000f;
                break;
            case 10: // 공룡
                Instantiate(Obj.AnimalLv5[0], spawnPos, transform.rotation);
                gameManager.point -= 7000;
                break;
        }
    }
}
