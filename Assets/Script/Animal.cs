using System.Collections.Generic;
using System.Xml.Linq;
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

    public Text expText;
    public Text foodText;
    public Text waterText;
    public Text hpText;

    public float idleTimer = 60f;
    public float foodTimer = 0;
    public float waterAndHpTimer = 0;

    private Vector3 moveDirection = Vector3.forward;

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
                DrinkState();
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
            SelectRandomBehavior();
        }
    }

    public void MoveState() // 이동
    {
        if (food <= 0 || water <= 0 || hp <= 0) return;

        transform.rotation = Quaternion.LookRotation(moveDirection);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0)
        {
            SelectRandomBehavior();
        }
    }

    public void EatState() // 먹기
    {
        if (food > 30)
        {
            SelectRandomBehavior();
            return;
        }
        MoveToTarget(GameManager.instance.FSMObjectManager.FoodBowl);
    }

    public void DrinkState() // 마시기
    {
        if (water > 30)
        {
            SelectRandomBehavior();
            return;
        }
        MoveToTarget(GameManager.instance.FSMObjectManager.WaterBowl);
    }

    public void RestState() // 쉬기
    {
        // 이동 중에 체력이 30 초과로 회복되었다면 쉬러 가는 걸 취소하고 대기/이동 선택
        if (hp > 30f)
        {
            SelectRandomBehavior();
            return;
        }
        MoveToTarget(GameManager.instance.FSMObjectManager.TreeShades);
    }

    public void MoveToTarget(List<GameObject> targetList)
    {
        if (targetList == null || targetList.Count == 0)
        {
            SelectRandomBehavior();
            return;
        }

        GameObject nearestTarget = targetList[0];
        float nearestDistance = 999999999999f;

        // 가장 가까운 타겟 찾기
        foreach (GameObject target in targetList)
        {
            float currentDistance = Vector3.SqrMagnitude(transform.position - target.transform.position);

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
        if (GameManager.instance.weatherManager.currentWeather == WeatherType.Cloudy) // 날씨가 흐리면
        {
            currentSpeed = speed * 0.5f;
        }
        else
        {
            currentSpeed = speed;
        }
    }

    public void SelectRandomBehavior() // 이동과 대기 중 하나를 랜덤 선택
    {
        int randomState = Random.Range(0, 2);

        if (randomState == 0)
        {
            Change(Animalstate.Move);
        }
        else
        {
            Change(Animalstate.Idle);
        }
    }

    public void Change(Animalstate state)
    {
        StateType = state;

        switch (StateType)
        {
            case Animalstate.Idle: // 체인지 대기
                idleTimer = 60f;
                break;
            case Animalstate.Move: // 체인지 움직임
                moveDirection.x = Random.Range(-10f, 10f);
                moveDirection.z = Random.Range(-10f, 10f);
                idleTimer = 3f;
                AddExp(1f);
                break;
        }
    }

    public void State()
    {
        // UI
        LvText.text = Lv.ToString();            // 레벨텍스트
        expBar.fillAmount = exp / 10f;        // 경험치바
        foodBar.fillAmount = food / 100f;       // 배고픔바
        waterBar.fillAmount = water / 100f;     // 물바
        hpBar.fillAmount = hp / 100f;           // 체력바

        expText.text = exp.ToString() + " / 10";
        foodText.text = food.ToString() + " / 100";
        waterText.text = water.ToString() + " / 100";
        hpText.text = hp.ToString() + " / 100";

        foodTimer += Time.deltaTime;
        waterAndHpTimer += Time.deltaTime;

        if (foodTimer >= 25f)
        {
            food = Mathf.Clamp(food - 10f, 0f, 100f); // 배고픔 10 감소
            foodTimer -= 25f;
        }

        if (waterAndHpTimer >= 12.5f)
        {
            float hpAmount;

            if (GameManager.instance.weatherManager.currentWeather == WeatherType.Rain) // 날씨가 비라면
            {
                hpAmount = 10f;
            }
            else
            {
                hpAmount = 5f;
            }

            hp = Mathf.Clamp(hp - hpAmount, 0f, 100f); // 체력 5 감소
            water = Mathf.Clamp(water - 10f, 0f, 100f); // 물 10 감소
            waterAndHpTimer -= 12.5f;
        }

        // 먹기, 마시기, 휴식 상태
        if (food <= 0 || water <= 0 || hp <= 0) return;

        if (StateType == Animalstate.Eat || StateType == Animalstate.Drink || StateType == Animalstate.Rest) return;

        FSMObjectManager fsmObjectManager = GameManager.instance.FSMObjectManager;

        if (food <= 30f && fsmObjectManager.FoodBowl.Count > 0)
        {
            Change(Animalstate.Eat);
        }
        else if (water <= 30f && fsmObjectManager.WaterBowl.Count > 0)
        {
            Change(Animalstate.Drink);
        }
        else if (hp <= 30f && fsmObjectManager.TreeShades.Count > 0)
        {
            Change(Animalstate.Rest);
        }
    }

    public void AddExp(float add)
    {
        exp = Mathf.Clamp(exp + add, 0f, 10f); // Mathf.Clamp(검사할 값, 최소값, 최대값);
    }
}