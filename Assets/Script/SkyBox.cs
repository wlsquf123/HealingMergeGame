using UnityEngine;

public class SkyBox : MonoBehaviour
{
    public float spped = 5f;
    void Update()
    {
        float rot = RenderSettings.skybox.GetFloat("_rot");
        RenderSettings.skybox.SetFloat("_rot", rot +  spped * Time.deltaTime);
    }
}
