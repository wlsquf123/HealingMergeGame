using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UImanager;
    public FSMObjectManager FSMObjectManager;

    public float DayTIme = 0; // 시간
    public float Day = 1f; // 일
    public float h; // 시
    public float m; // 분

    public float point = 100f; // 포인트
    public float pointTime = 0; // 2초당 초기화

    public int score = 0; // 동물 점수
    public int cheatScore = 0; // 치트키 점수 
    public int endScore = 700; // 최종점수


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        GameTIme(); //게임 시간
        State(); // 상태
        Cheatkey(); // 치트키
        TotalScore(); // 점수 계산
        GameEnd(); // 게임 엔딩
    }

    public void State()
    {
        pointTime += Time.deltaTime;
        // 포인트 2초마다 1증가
        if (pointTime > 2f)
        {
            point++;
            pointTime -= 2f;
        }
    }

    public void GameTIme()
    {
        DayTIme += Time.deltaTime;

        if (DayTIme >= 0.2f) // 현실 시간 0.2초마다 게임 시간 1분 증가
        {
            m++;
            DayTIme -= 0.2f;
        }
        if (m >= 60)
        {
            h++;
            m -= 60;
        }
        if (h >= 24)
        {
            h -= 24;
            Day++;
        }
    }

    public void GameEnd()
    {
        if (Day >= 8)
        {
            Debug.Log("게임 끝");
            Time.timeScale = 0f;
            UImanager.endGameImage.gameObject.SetActive(true);
            endScore += score; // 최종 점수 계산 700 + score;
        }
    }

    public bool usePoint(float amount)
    {
        if (point < amount)
        {
            Debug.Log("포인트 부족");
            return false;
        }
        point -= amount;
        return true;
    }

    public void TotalScore()
    {
        score = 0;
        Animal[] animals = FindObjectsByType<Animal>(FindObjectsSortMode.None); // 순서를 따로 정렬하지 말고, 검색된 순서 그대로 가져와라

        // 동물별 레벨 × 등급 계산
        foreach (var animal in animals)
        {
            score += animal.Lv * animal.Rating;
        }

        score += cheatScore; // 스코어 안에 치트키 넣으면 사라지니까 따로 저장해서 합치기
    }

public void Cheatkey() // 치트키
    {
        // 일시정지
        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Escape))
        {
            UImanager.StopUI();
        }

        // 점수 증가
        if (Input.GetKeyDown(KeyCode.F2))
        {
            cheatScore += 100;
        }

        // 시간 2배속
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale = (Time.timeScale == 1f) ? 2f : 1f;
        }

        // 날씨 변경
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // 날씨 변경
        }

        // 포인트 증가
        if (Input.GetKeyDown(KeyCode.F5))
        {
            point += 10000f;
        }
    }
}
