using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AnimalStateLookat : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        target = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
    }

}