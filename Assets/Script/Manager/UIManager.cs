using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Text pointText; // 포인트 텍스트
    public Text ScoreText; // 점수 텍스트
    public TMP_Text DayText; // 일 시 분
    public Text weatherText; // 날씨 텍스트

    public Image stopImage; // 일시정지
    public Image endGameImage; // 게임끝 이미지
    public Text endScoreText; // 게임 끝 스코어 텍스트

    void Update()
    {
        StateUI();
    }

    public void StateUI()
    {
        var gameManager = GameManager.instance;

        pointText.text = gameManager.point.ToString(); // 포인트
        ScoreText.text = gameManager.score.ToString(); // 점수
        endScoreText.text = "최종점수: " + gameManager.endScore; // 최종 점수
        DayText.text = "Day " + gameManager.Day+ "\n" + gameManager.h.ToString("00") + ":" + gameManager.m.ToString("00"); // 시간
        switch (gameManager.weatherManager.currentWeather)
        {
            case WeatherType.Sunny:
                weatherText.text = "맑음";
                break;
            case WeatherType.Cloudy:
                weatherText.text = "흐림";
                break;
            case WeatherType.Rain:
                weatherText.text = "비";
                break;
            case WeatherType.Thunder:
                weatherText.text = "천둥";
                break;
        }
    }

    public void StopUI() // 일시정지
    {
        stopImage.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnlickKeep() // 계속하기
    {
        Time.timeScale = 1.0f;
        stopImage.gameObject.SetActive(false);
    }

    // 랭킹버튼()
    public void OnlickRanking()
    {

    }
    public void OnclickOption() // 설정
    {
        // 설정창 열기
    }
    
    // 종료버튼
    public void ExitButton()
    {
        Application.Quit();
    }

}