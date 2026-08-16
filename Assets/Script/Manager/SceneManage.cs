using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public GameObject MainOBJ; // 움직이는 카메라
    public GameObject MainCamera; // 안움직이는 카메라

    public void StartScene()
    {
        GameManager.instance.UImanager.gameObject.SetActive(true);
        MainOBJ.SetActive(false);
        MainCamera.SetActive(true);
        GameManager.instance.isGame = true;
    }

    public void LoadScene()
    {
        GameManager.instance.UImanager.gameObject.SetActive(true);
        MainOBJ.SetActive(false);
        MainCamera.SetActive(true);
        GameManager.instance.isGame = true;
        GameManager.instance.SaveManager.LoadGame();
    }
}
