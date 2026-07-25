using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalDragManager : MonoBehaviour
{
    public bool Merged = false;
    public Animal animal;

    private void OnMouseDrag()
    {
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.transform.position.y;

        transform.position = Camera.main.ScreenToWorldPoint(mousPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var otherMerged = other.GetComponent<AnimalDragManager>(); // 닿은 오브젝트의 스크립트를 otherMerged 변수에 저장

        if (otherMerged.Merged == true) return; // Merged가 참이면 돌아가라

        Merged = true; // 내 오브젝트는 합성된 상태
        Destroy(other.gameObject);
        Destroy(gameObject);

        GameManager.instance.UImanager.OpenAnimallists(animal.Rating, transform.position);
    }


}