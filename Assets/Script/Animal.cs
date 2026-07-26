using Unity.VisualScripting;
using UnityEngine;

public enum Animalstate
{
    None,
    Move, // 이동
    idle, // 대기
    Eat,   // 먹기
    Drink, // 물 마시기
    Rest // 휴식
}

public class Animal : MonoBehaviour
{
    public Animalstate StateType = Animalstate.idle;
    public string animalName; // 이름
    public float exp = 0f; // 경험치
    public int Lv = 1; // 레벨
    public float food = 50f; // 포만도
    public float water = 50f; // 수분유지
    public float hp = 100f; // 체력
    public float speed = 2f; // 이동속도
    public int Rating = 1; // 등급

    public float idleTimer = 2f;
    public float foodTimer = 0;
    public float waterAndHpTimer = 0;

    private void Update()
    {
        State();
        // 업데이트에서는 상태 체크만 한다!!
        switch (StateType)
        {
            case Animalstate.None:
                break;
            case Animalstate.idle:
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
        idleTimer -= Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(moveDirection);
        if (idleTimer <= 0)
        {
            Change(Animalstate.idle);
        }
    }

    public void EatState() // 먹기
    {
        var foodOBJ = GameManager.instance.FSMObjectManager.FoodBowl;

        if (foodOBJ == null) // 리스트에서 foodOBJ가 없으면? 돌아가라
        {
            Change(Animalstate.idle);
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
        if (waterOBJ == null)
        {
            Change(Animalstate.idle);
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
        if (TreeOBJ == null)
        {
            Change(Animalstate.idle);
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
            case Animalstate.idle:
                idleTimer = 2f;
                break;
            case Animalstate.Move:
                moveDirection.x = Random.Range(-5f, 5f);
                moveDirection.z = Random.Range(-5f, 5f);
                idleTimer = 2f;
                break;
        }
    }

    public void State()
    {
        foodTimer += Time.deltaTime;
        waterAndHpTimer += Time.deltaTime;

        if (foodTimer >= 12f) // 밥통 1시간마다 감소 
        {
            food -= 10f;
            foodTimer = 0;
        }
        if (waterAndHpTimer >= 6f) // 물, 체력 30분 마다 감소
        {
            water -= 10f;
            hp -= 5f;
            waterAndHpTimer = 0;
        }

        // 상태 판단
        if (food <= 0 || water <= 0 || hp <= 0)
        {
            Change(Animalstate.None); // 0되면 멈춰라 행동불능
            return;
        }
        if (StateType == Animalstate.Eat || StateType == Animalstate.Drink || StateType == Animalstate.Rest) return;
        if (food <= 30f)
        {
            Change(Animalstate.Eat);    
        }

        if (water <= 30f)
        {
            Change(Animalstate.Drink);
        }

        if (hp <= 30f)
        {
            Change(Animalstate.Rest);
        }
    }

    public void Foods()
    {
        food = 100f;
        Change(Animalstate.idle);
    }

    public void Waters()
    {
        water = 100f;
        Change(Animalstate.idle);
    }
}