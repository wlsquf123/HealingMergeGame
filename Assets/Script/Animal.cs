using UnityEngine;
using UnityEngine.UI;

public enum Animalstate
{
    None,
    Move, // 이동
    Idle, // 대기
    Eat,   // 먹기
    Drink, // 물 마시기
    Rest // 휴식
}

public class Animal : MonoBehaviour
{
    public Animalstate StateType = Animalstate.Idle;
    public string animalName; // 이름
    public float exp = 0f; // 경험치
    public int Lv = 1; // 레벨
    public float food = 50f; // 포만도
    public float water = 50f; // 수분유지
    public float hp = 100f; // 체력
    public float speed = 2f; // 이동속도
    public int Rating = 1; // 등급

    public Text LvText;
    public Image expBar;
    public Image foodBar;
    public Image waterBar;
    public Image hpBar;

    public float idleTimer = 2f;
    public float foodTimer = 0;
    public float waterAndHpTimer = 0;

    private void Update()
    {
        State();
        // 업데이트에서는 상태 체크만 한다!!
        switch (StateType)
        {
            case Animalstate.Idle:
                IdleState();
                break;
            case Animalstate.Move:
                MoveState();
                break;
            case Animalstate.Eat:
                EatState();
                break;
            case Animalstate.Drink:
                DrnkState();
                break;
            case Animalstate.Rest:
                RestState();
                break;
        }
    }

    public void IdleState()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            Change(Animalstate.Move);
        }
    }

    Vector3 moveDirection = Vector3.zero;
    public void MoveState()
    {
        if (food <= 0 || water <= 0 || hp <= 0) return;

        idleTimer -= Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(moveDirection);
        if (idleTimer <= 0)
        {
            Change(Animalstate.Idle);
        }
    }

    public void EatState() // 먹기
    {
        var foodOBJ = GameManager.instance.FSMObjectManager.FoodBowl;

        if (foodOBJ == null || foodOBJ.Count == 0) // 리스트에서 foodOBJ가 없으면? 돌아가라
        {
            Change(Animalstate.Idle);
            return;
        }

        GameObject nearestBowl = null;
        float nearestDistance = 9999999f; // 가장 가까운 거리 저장

        foreach (var bowl in foodOBJ) //foodOBJ 리스트에 들어있는 모든 밥통을 하나씩 순서대로 꺼내어 검사
        {
            float currentDistance = Vector3.Distance(transform.position, bowl.transform.position); // 현재거리에 저장

            if (currentDistance < nearestDistance) // 현재거리가 저장된 가장가까운 거리보다 더 가깝냐? 맞으면 아래 실행
            {
                nearestDistance = currentDistance; // 가장가까운거리에 저장
                nearestBowl = bowl; // 가장 가까운 밥통 객체에도 저장
            }
        }
        transform.LookAt(nearestBowl.transform);
        idleTimer -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nearestBowl.transform.position, speed * Time.deltaTime);
    }

    public void DrnkState() // 마시기
    {
        var waterOBJ = GameManager.instance.FSMObjectManager.WaterBowl;
        if (waterOBJ == null || waterOBJ.Count == 0)
        {
            Change(Animalstate.Idle);
            return;
        }

        GameObject nearestBowl = null;
        float nearestDistance = 9999999f;

        foreach (var bowl in waterOBJ)
        {
            float currentDistance = Vector3.Distance(transform.position, bowl.transform.position);
            if (currentDistance <= nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestBowl = bowl;
            }
        }
        transform.LookAt(nearestBowl.transform);
        idleTimer -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nearestBowl.transform.position, speed * Time.deltaTime);
    }

    public void RestState() // 쉬기
    {
        var TreeOBJ = GameManager.instance.FSMObjectManager.TreeShades;
        if (TreeOBJ == null || TreeOBJ.Count == 0)
        {
            Change(Animalstate.Idle);
            return;
        }

        GameObject nearestBowl = null;
        float nearestDistance = 9999999f;

        foreach (var bowl in TreeOBJ)
        {
            float currentDistance = Vector3.Distance(transform.position, bowl.transform.position);
            if (currentDistance <= nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestBowl = bowl;
            }
        }
        transform.LookAt(nearestBowl.transform);
        idleTimer -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nearestBowl.transform.position, speed * Time.deltaTime);
    }

    public void Change(Animalstate state)
    {
        StateType = state;

        switch (StateType)
        {
            case Animalstate.Idle:
                idleTimer = 2f;
                break;
            case Animalstate.Move:
                moveDirection.x = Random.Range(-5f, 5f);
                moveDirection.z = Random.Range(-5f, 5f);
                idleTimer = 2f;
                AddExp(1f);
                break;
        }
    }

    public void State()
    {
        // UI
        LvText.text = Lv.ToString();            // 레벨
        expBar.fillAmount = exp / 1000f;        // 경험치
        foodBar.fillAmount = food / 100f;       // 배고픔
        waterBar.fillAmount = water / 100f;     // 물
        hpBar.fillAmount = hp / 100f;           // 체력

        waterAndHpTimer += Time.deltaTime;
        foodTimer += Time.deltaTime;

        if (foodTimer >= 12f)
        {
            food -= 10f; // 배고픔 10 감소

            food = Mathf.Clamp(food, 0f, 100f);

            foodTimer -= 12f;
        }

        if (waterAndHpTimer >= 6f)
        {
            hp -= 5f; // 체력 5 감소
            water -= 10f; // 물 10 감소

            water = Mathf.Clamp(water, 0f, 100f);
            hp = Mathf.Clamp(hp, 0f, 100f);

            waterAndHpTimer -= 6;
        }

        // 먹기, 마시기, 휴식 상태
        if (food <= 0 || water <= 0 || hp <= 0) return;
        if (StateType == Animalstate.Eat || StateType == Animalstate.Drink || StateType == Animalstate.Rest) return;

        if (food <= 30f && GameManager.instance.FSMObjectManager.FoodBowl.Count > 0)
        {
            Change(Animalstate.Eat);
        }

        else if (water <= 30f && GameManager.instance.FSMObjectManager.WaterBowl.Count > 0)
        {
            Change(Animalstate.Drink);
        }

        else if (hp <= 30f && GameManager.instance.FSMObjectManager.TreeShades.Count > 0)
        {
            Change(Animalstate.Rest);
        }
    }

    public void AddExp(float add)
    {
        exp = Mathf.Clamp(exp + add, 0f, 1000f); // Mathf.Clamp(검사할 값, 최소값, 최대값);
    }
}