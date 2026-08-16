using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float speed = 1f;

    private void Update()
    {
        float rotation = RenderSettings.skybox.GetFloat("_Rotation");
        RenderSettings.skybox.SetFloat("_Rotation", rotation + Time.deltaTime * speed);
    }
}