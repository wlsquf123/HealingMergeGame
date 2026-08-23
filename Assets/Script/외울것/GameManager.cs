using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UImanager;
    public FSMObjectManager FSMObjectManager;
    public WeatherManager weatherManager;
    public InventoryManager inventoryManager;
    public SaveManager SaveManager;
    public SceneManage SceneManage;
    public MergeManager MergeManager;
    public RankingManager RankingManager;

    public bool isGame = false; // 게임 시작 여부 

    public float Day = 1f; // 일
    public float h; // 시
    public float m; // 분

    public float point = 100f; // 포인트
    public float pointTime = 0; // 2초당 초기화

    public int score = 0; // 동물 점수
    int cheatScore = 0; // 치트키 점수 
    public int endScore = 0; // 최종점수

    public Light Light;
    public Material 낮;
    public Material 밤;
    public Material 노을;
    public Material 흐림;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);        }
    }

    void Update()
    {
        if (Time.timeScale == 0 || isGame == false) return;
        Cheatkey(); // 치트키
        GameTIme(); //게임 시간
        State(); // 상태
        TotalScore(); // 점수 계산
        GameEnd(); // 게임 엔딩
        DayNight(); // 낮밤 변경
    }

    public void State()
    {
        pointTime += Time.deltaTime * 2.4f;

        if (pointTime >= 10f)
        {
            point++;
            pointTime -= 10f;
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

    public bool UsePoint(float amount)
    {
        if (point < amount)
        {
            Debug.Log("포인트 부족");
            return false;
        }
        point -= amount;
        return true;
    }

    public void GameEnd() // 이거도 기억해요 대회에서 쓸거야.
    {
        if (Day >= 8)
        {
            Debug.Log("게임 끝");
            Time.timeScale = 0f;
            UImanager.endGameImage.SetActive(true);
            endScore = score + 700; // 최종 점수 계산
        }
    }


    public void TotalScore()
    {
        score = 0; // 이거 해줘야함 안그러면 계속 더해짐. 한번 초기화 하는거임 그리고 저장기능떄 안만들어도 됨.
        var animals = FindObjectsByType<Animal>(FindObjectsSortMode.None);

        // 동물별 레벨 × 등급 계산
        foreach (var animal in animals)
        {
            score += animal.Lv * animal.Rating;
        }

        score += cheatScore; // 스코어 안에 치트키 넣으면 사라지니까 따로 저장해서 합치기
    }
    
    public void DayNight()
    {
        float currTimer = h + m / 60f;

        Light.transform.rotation = Quaternion.Euler(currTimer / 24 * 360 - 90, 0f, 0f);

        if (currTimer >= 20f || currTimer < 6)
        {
            RenderSettings.skybox = 밤;
        }
        else if (weatherManager.currentWeather == WeatherType.Thunder || weatherManager.currentWeather == WeatherType.Rain) 
        {
            RenderSettings.skybox = 흐림;
        }
        else if (currTimer >= 17)
        {
            RenderSettings.skybox = 노을;
        }
        else
        {
            RenderSettings.skybox = 낮;
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

        if (Input.GetKeyDown(KeyCode.F7))
        {
            h += 1f;
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            SaveManager.GameSave();
        }
    }
}
