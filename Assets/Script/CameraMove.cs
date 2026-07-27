using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float MoveSpeed = 5f;
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MoveSpeed = 10f;
            }
            else
            {
                MoveSpeed = 5f;
            }
            float speed = 5f;
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.Rotate(0, x * speed, 0, Space.World);
            transform.Rotate(-y * speed, 0, 0);
        }

        

        float zKey = Input.GetAxis("Vertical");
        float xKey = Input.GetAxis("Horizontal");

        transform.Translate(xKey * MoveSpeed * Time.deltaTime, 0, zKey * MoveSpeed * Time.deltaTime);
    }
}
