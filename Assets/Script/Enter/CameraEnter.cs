using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEnter : MonoBehaviour
{
    [Header("UI 연결")]
    public Button followButton; // '동물 따라가기' 버튼
    public Button exitButton;   // '나가기' 버튼 (추적 중에만 켜짐)

    [Header("카메라 설정")]
    public Camera mainCamera;   // 조종할 카메라
    public Vector3 cameraOffset = new Vector3(0f, 3f, -5f); // 동물 뒤쪽/위쪽 오프셋 위치

    [Header("감지 상태")]
    private Animal detectedAnimal; // 현재 감지된 동물
    private Animal targetAnimal;   // 최종적으로 선택해서 따라가는 동물
    private List<Animal> animalsInArea = new List<Animal>();

    private bool isFollowing = false; // 현재 동물을 따라가고 있는지 여부
    public BoxCollider triggerCollider;

    private void LateUpdate()
    {
        // 따라가기 모드일 때 매 프레임마다 카메라가 동물을 쫓아감
        if (isFollowing && targetAnimal != null)
        {
            // 동물의 위치 기준으로 목표 카메라 위치 계산
            Vector3 targetPosition = targetAnimal.transform.position + cameraOffset;

            // 카메라도 동물을 바라보게 시선 고정
            mainCamera.transform.LookAt(targetAnimal.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return; // 태그가 동물이 아니면 나가라

        Animal animal = other.GetComponentInParent<Animal>();

        animalsInArea.Add(animal); // 리스트 추가

        
        if (!isFollowing && detectedAnimal == null) // 만약 따라가는 중이 아니라면, 첫 번째 동물 지정
        {
            SetDetectedAnimal(animalsInArea[0]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        Animal animal = other.GetComponentInParent<Animal>();

        animalsInArea.Remove(animal); // 리스트 삭제

        if (animal != detectedAnimal) return; // 방금 구역 밖으로 나간 동물이, 지금 화면에 버튼이 띄워져 있는 '대표 동물'이 아니라면 그냥 무시해라

        if (animalsInArea.Count > 0)
        {
            SetDetectedAnimal(animalsInArea[0]);
        }
        else
        {
            detectedAnimal = null;
            followButton.gameObject.SetActive(false);
        }
    }

    // [동물 따라가기] 버튼을 눌렀을 때
    public void StartFollowing()
    {
        if (detectedAnimal == null) return;

        targetAnimal = detectedAnimal; // 진짜 추적 대상으로 확정!
        isFollowing = true;

        triggerCollider.enabled = false; // 콜라이더 끄기

        followButton.gameObject.SetActive(false); // 따라가기 버튼 숨기기
        exitButton.gameObject.SetActive(true);     // 나가기 버튼 표시

        Debug.Log(targetAnimal.animalName + " 추적 시작!");
    }

    // [나가기] 버튼을 눌렀을 때 (자유 카메라로 복귀)
    public void ExitFollowing()
    {
        isFollowing = false;
        targetAnimal = null;

        triggerCollider.enabled = true; // 콜라이더 키기

        exitButton.gameObject.SetActive(false); // 나가기 버튼 숨기기

        // 여전히 구역 안에 다른 동물이 있다면 따라가기 버튼 다시 띄우기
        if (animalsInArea.Count > 0)
        {
            SetDetectedAnimal(animalsInArea[0]);
        }

        Debug.Log("자유 카메라 상태로 복귀");
    }


    private void SetDetectedAnimal(Animal newAnimal)
    {
        detectedAnimal = newAnimal;
        followButton.gameObject.SetActive(true);
    }
}