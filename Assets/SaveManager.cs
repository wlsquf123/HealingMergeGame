using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AnimalSaveData
{
    public string animalName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public int level;
    public float exp;
    public float food;
    public float water;
    public float hp;
    public bool isThunder;
}

[System.Serializable]
public class GameSaveData
{
    public float day;
    public float hour;
    public float minute;
    public float point;

    public int foodCount;
    public int waterCount;
    public int autoFoodCount;
    public int autoWaterCount;
    public int autoHpCount;
    public int thunderCount;

    public WeatherType weather;
    public List<AnimalSaveData> animals = new List<AnimalSaveData>();

    public List<Vector3> foodBowlPositions = new List<Vector3>();
    public List<Vector3> waterBowlPositions = new List<Vector3>();
    public List<Vector3> treeShadePositions = new List<Vector3>();
}

public class SaveManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    [Header("시설물 관리자")]
    public FSMObjectManager fsmObjectManager;

    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/HealingMergeSave.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6)) SaveGame();
        if (Input.GetKeyDown(KeyCode.F7)) LoadGame();
    }

    public void SaveGame()
    {
        GameSaveData data = new GameSaveData();

        data.day = GameManager.instance.Day;
        data.hour = GameManager.instance.h;
        data.minute = GameManager.instance.m;
        data.point = GameManager.instance.point;

        data.foodCount = GameManager.instance.inventoryManager.foodCount;
        data.waterCount = GameManager.instance.inventoryManager.waterCount;
        data.autoFoodCount = GameManager.instance.inventoryManager.autoFoodCount;
        data.autoWaterCount = GameManager.instance.inventoryManager.autoWaterCount;
        data.autoHpCount = GameManager.instance.inventoryManager.autoHpCount;
        data.thunderCount = GameManager.instance.inventoryManager.thunderCount;

        data.weather = GameManager.instance.weatherManager.currentWeather;

        Animal[] animals = FindObjectsByType<Animal>(FindObjectsSortMode.None);

        foreach (Animal animal in animals)
        {
            AnimalSaveData animalData = new AnimalSaveData();

            animalData.animalName = animal.animalName;
            animalData.position = animal.transform.position;
            animalData.rotation = animal.transform.rotation;
            animalData.scale = animal.transform.localScale;
            animalData.level = animal.Lv;
            animalData.exp = animal.exp;
            animalData.food = animal.food;
            animalData.water = animal.water;
            animalData.hp = animal.hp;
            animalData.isThunder = animal.isThunder;

            data.animals.Add(animalData);
        }

        Transform[] allObjects = FindObjectsByType<Transform>(FindObjectsSortMode.None);

        foreach (Transform obj in allObjects)
        {
            if (obj.name == fsmObjectManager.FoodBowlPrefab.name || obj.name == fsmObjectManager.FoodBowlPrefab.name + "(Clone)")
            {
                data.foodBowlPositions.Add(obj.position);
            }
            else if (obj.name == fsmObjectManager.WaterBowlPrefab.name || obj.name == fsmObjectManager.WaterBowlPrefab.name + "(Clone)")
            {
                data.waterBowlPositions.Add(obj.position);
            }
            else if (obj.name == fsmObjectManager.TreeShadesPrefab.name || obj.name == fsmObjectManager.TreeShadesPrefab.name + "(Clone)")
            {
                data.treeShadePositions.Add(obj.position);
            }
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("게임 저장 완료");
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("저장된 파일이 없습니다.");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

        GameManager.instance.Day = data.day;
        GameManager.instance.h = data.hour;
        GameManager.instance.m = data.minute;
        GameManager.instance.point = data.point;

        GameManager.instance.inventoryManager.foodCount = data.foodCount;
        GameManager.instance.inventoryManager.waterCount = data.waterCount;
        GameManager.instance.inventoryManager.autoFoodCount = data.autoFoodCount;
        GameManager.instance.inventoryManager.autoWaterCount = data.autoWaterCount;
        GameManager.instance.inventoryManager.autoHpCount = data.autoHpCount;
        GameManager.instance.inventoryManager.thunderCount = data.thunderCount;

        GameManager.instance.weatherManager.currentWeather = data.weather;
        GameManager.instance.weatherManager.ApplyWeather();

        Animal[] currentAnimals = FindObjectsByType<Animal>(FindObjectsSortMode.None); // 게임에 있는 모든 동물 스크립트 확인

        foreach (Animal animal in currentAnimals)
        {
            Destroy(animal.gameObject);
        }

        foreach (AnimalSaveData animalData in data.animals)
        {
            foreach (GameObject animalPrefab in animalPrefabs)
            {
                if (animalPrefab == null) continue;

                Animal prefabAnimal = animalPrefab.GetComponent<Animal>();

                if (prefabAnimal == null) continue;
                if (prefabAnimal.animalName != animalData.animalName) continue;

                GameObject spawnedObject = Instantiate(animalPrefab, animalData.position, animalData.rotation);
                Animal spawnedAnimal = spawnedObject.GetComponent<Animal>();

                spawnedObject.transform.localScale = animalData.scale;
                spawnedAnimal.Lv = animalData.level;
                spawnedAnimal.exp = animalData.exp;
                spawnedAnimal.food = animalData.food;
                spawnedAnimal.water = animalData.water;
                spawnedAnimal.hp = animalData.hp;
                spawnedAnimal.isThunder = animalData.isThunder;
                spawnedAnimal.Change(Animalstate.Idle);

                break;
            }
        }

        foreach (Vector3 position in data.foodBowlPositions)
        {
            GameObject foodBowl = Instantiate(fsmObjectManager.FoodBowlPrefab, position, Quaternion.identity);
            fsmObjectManager.FoodBowl.Add(foodBowl);
        }

        foreach (Vector3 position in data.waterBowlPositions)
        {
            GameObject waterBowl = Instantiate(fsmObjectManager.WaterBowlPrefab, position, Quaternion.identity);
            fsmObjectManager.WaterBowl.Add(waterBowl);
        }

        foreach (Vector3 position in data.treeShadePositions)
        {
            GameObject treeShade = Instantiate(fsmObjectManager.TreeShadesPrefab, position, Quaternion.identity);
            fsmObjectManager.TreeShades.Add(treeShade);
        }


        Debug.Log("게임 불러오기 완료");
    }
}