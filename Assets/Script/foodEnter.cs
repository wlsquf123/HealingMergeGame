using UnityEngine;

public class FoodEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;
        var An = other.GetComponent<Animal>();
        An.Foods();
        GameManager.instance.FSMObjectManager.FoodBowl.Remove(gameObject);
        Destroy(gameObject);
    }
}
