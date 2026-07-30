using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("땅으로 사용할 레이어")]
    public LayerMask groundMask;

    private void OnMouseDrag()
    {
        if (Time.timeScale == 0f) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 마우스 아래의 땅을 찾음
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 targetPosition = hit.point;

            targetPosition.y += 1f;// 위치를 1증가

            transform.position = targetPosition;
        }
    }
}