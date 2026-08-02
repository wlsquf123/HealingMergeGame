using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UImanager;
    public FSMObjectManager FSMObjectManager;
    public WeatherManager weatherManager;
    public InventoryManager inventoryManager;

    public float Day = 1f; // 일
    public float h; // 시
    public float m; // 분

    public float point = 100f; // 포인트
    public float pointTime = 0; // 2초당 초기화

    public int score = 0; // 동물 점수
    int cheatScore = 0; // 치트키 점수 
    public int endScore = 0; // 최종점수

    [Header("낮과 밤")]
    public Light sunLight; // Directional Light

    [Header("스카이박스")]
    public Material daySkybox;   // 낮 하늘
    public Material nightSkybox; // 밤 하늘


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
        Cheatkey(); // 치트키
        GameTIme(); //게임 시간
        State(); // 상태
        TotalScore(); // 점수 계산
        GameEnd(); // 게임 엔딩
        DayNight(); // 낮밤 변경
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
        m+= Time.deltaTime * 2.4f;

        if (m >= 60)
        {
            h++;
            m -= 60;
        }
        if (h >= 24)
        {
            h -= 24;
            Day++;

            weatherManager.ChangeRandomWeather(); // 날씨 변경 (랜덤)
        }
    }

    public void GameEnd()
    {
        if (Day >= 8)
        {
            Debug.Log("게임 끝");
            Time.timeScale = 0f;
            UImanager.endGameImage.gameObject.SetActive(true);
            endScore = score + 700; // 최종 점수 계산
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

    public void DayNight()
    {
        float currentTime = h + (m / 60f);

        // 1. 태양 각도 조절
        sunLight.transform.rotation = Quaternion.Euler((currentTime / 24) * 360 - 90f, 170f, 0f);

        // 2. 시간대에 따른 낮밤 판별 및 스카이박스 변경
        if (currentTime >= 18f || currentTime < 6f)
        {
            RenderSettings.skybox = nightSkybox; // 밤 하늘로 변경!
            sunLight.intensity = 0.1f;
        }
        else
        {
            RenderSettings.skybox = daySkybox;   // 낮 하늘로 변경!
            sunLight.intensity = 1f;
        }
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
            weatherManager.ChangeNextWeather();
        }

        // 포인트 증가
        if (Input.GetKeyDown(KeyCode.F5))
        {
            point += 10000f;
        }
    }
}
