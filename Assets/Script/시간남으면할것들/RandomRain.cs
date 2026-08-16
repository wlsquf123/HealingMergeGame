using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RandomRain : MonoBehaviour
{
    public GameObject Rain;

    float Timer = 0;

    private void Update()
    {
        Timer += Time.deltaTime;
        float randomX = Random.Range(-40f, 20f);
        float randomZ = Random.Range(-40f, 40f);

        Vector3 pos = new Vector3(randomX, 0, randomZ);
        if (Timer > 0.1f)
        {
            Instantiate(Rain, pos, transform.rotation = Quaternion.Euler(-90f, 0f, 0f));
            Timer -= 0.1f;
        }
    }
}
