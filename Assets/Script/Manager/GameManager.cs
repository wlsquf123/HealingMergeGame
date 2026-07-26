using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UImanager;
    public FSMObjectManager FSMObjectManager;

    public float DayTIme = 228f; // ½Ã°£
    public int Daycount; // 

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

    void Start()
    {

    }

    void Update()
    {
        DayTIme -= Time.deltaTime;


        if (DayTIme <= 0)
        {
            DayTIme = 228f;
            Daycount++;
        }
    }
}
