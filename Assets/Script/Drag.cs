using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("고정할 Y축 높이")]
    [SerializeField] private float targetY = 5f; // 인스펙터에서 원하는 높이로 수정 가능

    private void OnMouseDrag()
    {
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.transform.position.y;

        // 화면 좌표를 월드 좌표로 변환
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousPos);

        // Y 축 값을 지정한 높이로 강제 고정
        targetPos.y = targetY;

        // 최종 위치 적용
        transform.position = targetPos;
    }
}