using UnityEngine;

public class FoodEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var An = other.GetComponent<Animal>();
        if (!other.CompareTag("Animal") || An.food > 30f) return;
        An.AddExp(5f);
        An.food = 100f;
        An.Change(Animalstate.Idle);
        GameManager.instance.FSMObjectManager.FoodBowl.Remove(gameObject);
        Destroy(gameObject);
    }
}
