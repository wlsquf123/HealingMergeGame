using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEnter : MonoBehaviour
{
    [Header("UI & 카메라")]
    public Button followButton;
    public Button exitButton;
    public Camera mainCamera;
    public float followSpeed = 5f;
    public Vector3 cameraOffset = new Vector3(0f, 8f, -5f);

    [Header("상태")]
    public Collider triggerCollider;
    private Animal detectedAnimal;
    private Animal targetAnimal;
    private List<Animal> animalsInArea = new List<Animal>();

    private void LateUpdate()
    {
        // targetAnimal이 없다면 쫓아가지 않음 (isFollowing 변수 대체)
        if (targetAnimal == null) return;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetAnimal.transform.position + cameraOffset, Time.deltaTime * followSpeed);
        mainCamera.transform.LookAt(targetAnimal.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Animal animal = other.GetComponentInParent<Animal>();

        // 동물이 아니거나, 이미 리스트에 있다면 무시 (태그 검사 대체)
        if (animal == null || animalsInArea.Contains(animal)) return;

        animalsInArea.Add(animal);

        // 추적 중이 아니고 대표 동물이 없을 때
        if (targetAnimal == null && detectedAnimal == null)
            UpdateDetectedAnimal(animal);
    }

    private void OnTriggerExit(Collider other)
    {
        Animal animal = other.GetComponentInParent<Animal>();

        // 동물이 아니거나, 리스트에서 지우는 데 실패했다면(리스트에 없던 애라면) 무시
        if (animal == null || !animalsInArea.Remove(animal)) return;

        // 방금 나간 애가 대표 동물이었다면 다음 타자 지정 (없으면 null)
        if (animal == detectedAnimal)
            UpdateDetectedAnimal(animalsInArea.Count > 0 ? animalsInArea[0] : null);
    }

    public void StartFollowing()
    {
        targetAnimal = detectedAnimal; // 타겟이 생겼으므로 자동 추적 시작
        triggerCollider.enabled = false;

        followButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
    }

    public void ExitFollowing()
    {
        targetAnimal = null; // 타겟을 비워서 추적 중지
        triggerCollider.enabled = true;
        exitButton.gameObject.SetActive(false);

        animalsInArea.Clear();
        UpdateDetectedAnimal(null); // 버튼 끄기 및 대표 동물 초기화를 한 방에!
    }

    // 동물을 지정하고, 동물이 있으면 버튼 ON / 없으면 OFF를 알아서 해주는 만능 함수
    private void UpdateDetectedAnimal(Animal animal)
    {
        detectedAnimal = animal;
        followButton.gameObject.SetActive(detectedAnimal != null);
    }
}