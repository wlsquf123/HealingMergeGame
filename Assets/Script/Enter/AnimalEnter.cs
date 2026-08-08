using UnityEngine;

public class AnimalEnter : MonoBehaviour
{
    public bool Merged = false;
    public Animal animal;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var otherEnter = other.GetComponent<AnimalEnter>(); // 닿은 오브젝트의 스크립트를 otherMerged 변수에 저장

        // 3. 이미 머지 중인 오브젝트인지 확인 (중복 실행 방지)
        if (Merged || otherEnter.Merged ||
            animal.animalName != otherEnter.animal.animalName || // 같은 종류인지 확인(이름으로 비교)
            animal.Lv != otherEnter.animal.Lv || // 같은 레벨인지 확인
            animal.exp < 10f || otherEnter.animal.exp < 10f // 두 동물 모두 경험치 1000인지 확인
            ) return; // 돌아가라

        // 내 오브젝트와 닿은 오브젝트 모두 true로 변경 다른 충돌 방지
        Merged = true;
        otherEnter.Merged = true;

        if (animal.Lv != 3) // 3레벨이 아니라면
        {
            Destroy(other.gameObject); // 상대방 삭제
            transform.localScale *= 1.25f; // 크기 1.25배씩 커지게 하기

            animal.Lv++;      // 내 레벨 1 증가
            animal.exp = 0f;  // 경험치 초기화
            animal.food = 50f;
            animal.water = 50f;
            animal.hp = 100f;

            animal.Change(Animalstate.Idle); // 오류 걸릴 수도있으니 상태는 Idle로

            Merged = false; // 나는 파괴되지 않고 살아남았으므로, 다음 머지를 위해 다시 false로 되돌려줌
        }
        else
        {
            // 기존에 작성하셨던 진화 UI 호출 또는 상위 동물 생성
            GameManager.instance.UImanager.OpenAnimallists(animal.Rating + 1, transform.position);

            // 진화라서 원래있던 오브젝트들 삭제
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}