using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalDragManager : MonoBehaviour
{
    private Transform animal; // 동물 위치
    public TerrainCollider terrain; // 동물이 움직일 투명한 수평 바닥
    private Vector3 offset; // 동물이 마우스 위치로 갑자기 튀지 않게 하는 거리 차이

    private void Update()
    {
        Mouse mouse = Mouse.current;
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue()); 

        // 동물 잡기
        if (mouse.leftButton.wasPressedThisFrame && Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("Animal"))
        {
            animal = hit.transform;

            if (terrain.Raycast(ray, out RaycastHit groundHit, Mathf.Infinity))
            {
                offset = animal.position - groundHit.point;
            }
        }

        // Terrain 위로 동물 이동
        if (animal != null && mouse.leftButton.isPressed && terrain.Raycast(ray, out RaycastHit moveHit, Mathf.Infinity))
        {
            animal.position = moveHit.point + offset;
        }

        // 동물 놓기
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            animal = null;
        }
    }
}