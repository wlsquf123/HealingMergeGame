using System.Collections.Generic;
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
    public float speed = 5f; // 이동속도
    private float currentSpeed; // 진짜 이동속도
    public int Rating = 1; // 등급
    public bool isThunder = false;

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
        UpdateSpeed();
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

    public void IdleState() // 대기
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            Change(Animalstate.Move);
        }
    }


    Vector3 moveDirection = Vector3.zero;
    public void MoveState() // 이동
    {
        if (food <= 0 || water <= 0 || hp <= 0) return;

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(moveDirection);

        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            Change(Animalstate.Idle);
        }
    }

    public void EatState() // 먹기
    {
        MoveToTarget(GameManager.instance.FSMObjectManager.FoodBowl);
    }

    public void DrnkState() // 마시기
    {
        MoveToTarget(GameManager.instance.FSMObjectManager.WaterBowl);
    }

    public void RestState() // 쉬기
    {
        MoveToTarget(GameManager.instance.FSMObjectManager.TreeShades);
    }

    public void MoveToTarget(List<GameObject> targetList)
    {
        if (targetList == null || targetList.Count == 0)
        {
            Change(Animalstate.Idle);
            return;
        }

        GameObject nearestTarget = null;
        float nearestDistance = Mathf.Infinity; // 9999999f 대신 무한대를 뜻하는 멋진 코드입니다.

        // 가장 가까운 타겟 찾기
        foreach (var target in targetList)
        {
            float currentDistance = Vector3.Distance(transform.position, target.transform.position);
            if (currentDistance < nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestTarget = target;
            }
        }

        // 타겟을 향해 이동
        transform.LookAt(nearestTarget.transform);
        transform.position = Vector3.MoveTowards(transform.position, nearestTarget.transform.position, currentSpeed * Time.deltaTime);
    }

    public void UpdateSpeed() // 속도
    {
        if (GameManager.instance.weatherManager.currentWeather == WeatherType.Cloudy)
        {
            currentSpeed = speed * 0.5f; // 흐림일 때는 0.5로 곱하기
        }
        else
        {
            currentSpeed = speed; // 그 외에는 원래 속도(5f)로 덮어쓰기
        }
    }

    public void Change(Animalstate state)
    {
        StateType = state;

        switch (StateType)
        {
            case Animalstate.Idle:
                idleTimer = 60f;
                break;
            case Animalstate.Move:
                moveDirection.x = Random.Range(-3f, 3f);
                moveDirection.z = Random.Range(-3f, 3f);
                idleTimer = 3f;
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

        if (foodTimer >= 25f)
        {
            food -= 10f; // 배고픔 10 감소

            food = Mathf.Clamp(food, 0f, 100f);

            foodTimer -= 25f;
        }

        if (waterAndHpTimer >= 12.5f)
        {
            float hpAmount = 5f;
            if (GameManager.instance.weatherManager.currentWeather == WeatherType.Rain)
            {
                hpAmount = 10f;
            }

            hp -= hpAmount; // 체력 5 감소
            water -= 10f; // 물 10 감소

            water = Mathf.Clamp(water, 0f, 100f);
            hp = Mathf.Clamp(hp, 0f, 100f);

            waterAndHpTimer -= 12.5f;
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