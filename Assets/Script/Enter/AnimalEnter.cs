using UnityEngine;

public class AnimalEnter : MonoBehaviour
{
    public bool Merged = false;
    public Animal animal;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        // 닿은 동물의 AnimalEnter 가져오기
        var otherEnter = other.GetComponent<AnimalEnter>(); 

        // 중복 실행 방지
        if (Merged || otherEnter.Merged) return;
        if (animal.animalName != otherEnter.animal.animalName) return;
        if (animal.Lv != otherEnter.animal.Lv) return;
        if (animal.exp < 10f || otherEnter.animal.exp < 10f) return;

        // 머지가 중복 실행되지 않도록 둘 다 true
        Merged = true;
        otherEnter.Merged = true;

        // 3레벨이 아니라면
        if (animal.Lv < 3)
        {
            Destroy(other.gameObject); // 상대방 삭제
            transform.localScale *= 1.25f; // 크기 1.25배씩 커지게 하기

            animal.Lv++;      // 내 레벨 1 증가
            animal.exp = 0f;  // 경험치 초기화
            animal.food = 50f;
            animal.water = 50f;
            animal.hp = 100f;

            animal.Change(Animalstate.Idle);

            Merged = false; // 자기 자신은 살아남았으니 다음 머지를 위해 false
        }
        else
        {
            // 상위 등급 객체 생성
            GameManager.instance.MergeManager.OpenAnimallists(animal.Rating++, transform.position);

            // 기존 두마리 삭제
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}