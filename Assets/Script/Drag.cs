using UnityEngine;

public class Drag : MonoBehaviour
{
    public LayerMask groundMask;

    private void OnMouseDrag()
    {
        if (Time.timeScale == 0f) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 마우스 아래의 땅을 찾음
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point + Vector3.up;
        }
    }
}