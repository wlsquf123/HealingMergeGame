using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("땅에서 띄울 높이")]
    float groundOffset = 1f;

    [Header("땅으로 사용할 레이어")]
    public LayerMask groundMask;

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 마우스 아래의 땅을 찾음
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 targetPosition = hit.point;

            // 현재 땅 높이에서 5만큼 위로 배치
            targetPosition.y += groundOffset;

            transform.position = targetPosition;
        }
    }
}