using UnityEngine;

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

    public float score; // 점수

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
        GameTIme();
        State();
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
}
