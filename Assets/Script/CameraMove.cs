using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float MoveSpeed = 10f; // 이동속도
    private float RotSpeed = 5f; // 도는 화면

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftShift)) MoveSpeed = 20f; // 시프트 시 속도 20 증가
            else MoveSpeed = 10f;
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.Rotate(0, x * RotSpeed, 0, Space.World);
            transform.Rotate(-y * RotSpeed, 0, 0);
        }

        float zKey = Input.GetAxis("Vertical");
        float xKey = Input.GetAxis("Horizontal");

        transform.Translate(xKey * MoveSpeed * Time.deltaTime, 0, zKey * MoveSpeed * Time.deltaTime);
    }
}
