using UnityEngine;

public class ThunderEnter : MonoBehaviour
{
    private void Start()
    {
        // 생성되고 0.1초 뒤에 삭제
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        Destroy(other.gameObject);
    }
}
