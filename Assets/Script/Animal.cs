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
    public string animalName;
    public float exp;
    public int Lv;
    public float food = 50f; // 포만도
    public float water; // 수분유지
    public float hp;
    public float speed; // 이동속도
    public int Rating; // 등급

    public float idleTimer = 60f;
    public float foodTimer = 0;
    public float waterAndHpTimer = 0;


    public Animalstate StateType = Animalstate.idle;
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
        transform.Translate(moveDirection * speed * Time.deltaTime);
        if (idleTimer <= 0)
        {
            Change(Animalstate.idle);
        }
    }

    public void EatState()
    {
        var foodOBJ = GameManager.instance.FSMObjectManager.FoodBowl;
        if (foodOBJ != null)
        {
            Change(Animalstate.idle);
            return;
        }

        int nearestFoodBowl = 0;
        float nearestDistance = 9999999f;

        for (int i = 0; i < foodOBJ.Count; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, foodOBJ[i].transform.position);
            if (currentDistance <= nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestFoodBowl = i;
            }
        }
        transform.LookAt(foodOBJ[nearestFoodBowl].transform);
        idleTimer -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, foodOBJ[nearestFoodBowl].transform.position, speed * Time.deltaTime);
    }

    public void DrnkState() // 마시기
    {

    }

    public void RestState()
    {

    }

    public void Change(Animalstate state)
    {
        StateType = state;

        switch (StateType)
        {
            case Animalstate.idle:
                idleTimer = 12f;
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
            Change(Animalstate.None);
            return;
        }
        if (food <= 30f)
        {
            Change(Animalstate.Eat);
            return;
        }

        if (water <= 30f)
        {
            Change(Animalstate.Drink);
            return;
        }

        if (hp <= 30f)
        {
            Change(Animalstate.Rest);
            return;
        }
    }

    public void Foods()
    {
        food = 100f;
        Change(Animalstate.idle);
    }
}