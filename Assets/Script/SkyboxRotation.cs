using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float speed = 1f;
    float Rotation;

    private void Start()
    {
        Rotation = RenderSettings.skybox.GetFloat("_Rotation");
    }

    private void Update()
    {
        Rotation += Time.deltaTime * speed;
        RenderSettings.skybox.SetFloat("_Rotation", Rotation);
    }



}