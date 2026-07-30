using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StopButton() // 일시정지
    {
        GameManager.instance.UImanager.StopUI();
    }

    public void OnlickKeep() // 계속하기
    {
        Time.timeScale = 1.0f;
        GameManager.instance.UImanager.stopImage.gameObject.SetActive(false);
    }

    // 랭킹버튼()

    public void OnclickOption() // 설정
    {
        // 설정창 열기
    }

    public void OnClickRanking() // 종료
    {
        // 메인화면으로 이동? 같음
    }
}
