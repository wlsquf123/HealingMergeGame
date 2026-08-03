using System.Collections.Generic;
using UnityEngine;

public class ThunderEnter : MonoBehaviour
{
    private void Start()
    {
        // 생성되고 0.1초 뒤에 삭제
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Animal animal = other.GetComponentInParent<Animal>();
        if (!other.CompareTag("Animal")) return;

        if (animal.isThunder == true)
        {
            animal.isThunder = false;
            return;
        }
        Destroy(animal.gameObject);
    }
}
