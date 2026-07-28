using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var An = other.GetComponent<Animal>();
        if (!other.CompareTag("Animal") || An.water > 30f) return;
        An.AddExp(3); // 경험치 증가
        An.water = 100f;
        An.Change(Animalstate.idle);
        GameManager.instance.FSMObjectManager.WaterBowl.Remove(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
