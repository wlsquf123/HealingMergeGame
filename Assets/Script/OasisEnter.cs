using UnityEngine;

public class OasisEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var An = other.GetComponent<Animal>();
        if (!other.CompareTag("Animal") || An.water > 30) return;

        An.water = 100f;
        An.AddExp(3f);
        An.Change(Animalstate.Idle);
    }
}