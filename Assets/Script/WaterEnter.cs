using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Animal")) return;

        var An = other.GetComponent<Animal>();
        An.Waters();
        GameManager.instance.FSMObjectManager.WaterBowl.Remove(gameObject);
        Destroy(gameObject);

    }
}
