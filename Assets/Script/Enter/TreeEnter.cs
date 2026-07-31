using UnityEngine;

public class TreeEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var An = other.GetComponent<Animal>();
        if (!other.CompareTag("Animal") || An.hp > 30f) return;
        An.AddExp(4); // 경험치 증가
        An.hp = 100f;
        An.Change(Animalstate.Idle);
    }
}
