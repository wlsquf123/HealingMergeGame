using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public GameObject MainOBJ; // 메인오브젝트
    public GameObject Sub; // 메인카메라

    public void StartScene()
    {
        MainOBJ.SetActive(false);
        Sub.SetActive(true);
        GameManager.instance.UImanager.gameObject.SetActive(true);
        Time.timeScale = 1f;

        GameManager.instance.isGame = true;
    }

    public void LoadScene() // 이어하기
    {
        MainOBJ.SetActive(false);
        Sub.SetActive(true);
        GameManager.instance.UImanager.gameObject.SetActive(true);
        Time.timeScale = 1f;

        GameManager.instance.isGame = true;

        GameManager.instance.SaveManager.LoadGame();
    }

    public void ResetButton() // 게임 나감 메인화면으로 이동
    {
        MainOBJ.SetActive(true);
        Sub.SetActive(false);
        GameManager.instance.UImanager.gameObject.SetActive(false);
        Time.timeScale = 1f;

        GameManager.instance.isGame = false;

        foreach (var ID in FindObjectsByType<SaveID>(FindObjectsSortMode.None))
        {
            Destroy(ID.gameObject);
        }

        GameManager.instance.inventoryManager.foodCount = 0;
        GameManager.instance.inventoryManager.waterCount = 0;
        GameManager.instance.inventoryManager.autoFoodCount = 0;
        GameManager.instance.inventoryManager.autoWaterCount = 0;
        GameManager.instance.inventoryManager.autoHpCount = 0;
        GameManager.instance.inventoryManager.thunderCount = 0;

        GameManager.instance.Day = 1;
        GameManager.instance.m = 0;
        GameManager.instance.h = 0;
        GameManager.instance.point = 100f;

        RenderSettings.skybox = GameManager.instance.밤;

    }

}
