using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UImanager;
    public FSMObjectManager FSMObjectManager;

    public float DayTIme = 228f; // 시간
    public int Daycount; // 288초당 1 증가 


    // 포인트 추가할것.
    // 점수 표시

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
        DayTIme -= Time.deltaTime;


        if (DayTIme <= 0)
        {
            DayTIme = 288f;
            Daycount++;
        }
    }
}
