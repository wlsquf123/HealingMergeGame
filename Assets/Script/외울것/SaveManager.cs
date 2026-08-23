using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void GameSave()
    {
        var objects = FindObjectsByType<SaveID>(FindObjectsSortMode.None); // SaveID 이 스크립트를 다 찾겠다. 

        PlayerPrefs.SetInt("ObjectCount", objects.Length);

        for (int i = 0; i < objects.Length; i++)
        {
            PlayerPrefs.SetString("id" + i, objects[i].id);

            PlayerPrefs.SetFloat("x" + i, objects[i].transform.position.x);
            PlayerPrefs.SetFloat("y" + i, objects[i].transform.position.y);
            PlayerPrefs.SetFloat("z" + i, objects[i].transform.position.z);

            PlayerPrefs.SetFloat("sx" + i, objects[i].transform.localScale.x);
            PlayerPrefs.SetFloat("sy" + i, objects[i].transform.localScale.y);
            PlayerPrefs.SetFloat("sz" + i, objects[i].transform.localScale.z);

            Animal animal = objects[i].GetComponent<Animal>();

            if (animal != null)
            {
                PlayerPrefs.SetFloat("Exp" + i, animal.exp);
                PlayerPrefs.SetFloat("Food" + i, animal.food);
                PlayerPrefs.SetFloat("Water" + i, animal.water);
                PlayerPrefs.SetFloat("Hp" + i, animal.hp);
                PlayerPrefs.SetInt("Lv" + i, animal.Lv);
            }
        }

        PlayerPrefs.SetInt("Day", (int)GameManager.instance.Day);
        PlayerPrefs.SetFloat("Point", GameManager.instance.point);
        PlayerPrefs.SetInt("Weather", (int)GameManager.instance.weatherManager.currentWeather);

        PlayerPrefs.Save();

        Debug.Log("저장 완료");
    }

    public void LoadGame()
    {
        int count = PlayerPrefs.GetInt("ObjectCount", 0);

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("프리팹/" + PlayerPrefs.GetString("id" + i));

            Vector3 pos = new Vector3(PlayerPrefs.GetFloat("x" + i), PlayerPrefs.GetFloat("y" + i), PlayerPrefs.GetFloat("z" + i));
            Vector3 scale = new Vector3(PlayerPrefs.GetFloat("sx" + i), PlayerPrefs.GetFloat("sy" + i), PlayerPrefs.GetFloat("sz" + i));

            GameObject newObj = Instantiate(prefab, pos, Quaternion.identity);
            newObj.transform.localScale = scale;

            Animal animal = newObj.GetComponent<Animal>();

            if (animal != null)
            {
                animal.exp = PlayerPrefs.GetFloat("Exp" + i);
                animal.food = PlayerPrefs.GetFloat("Food" + i);
                animal.water = PlayerPrefs.GetFloat("Water" + i);
                animal.hp = PlayerPrefs.GetFloat("Hp" + i);
                animal.Lv = PlayerPrefs.GetInt("Lv" + i);
            }
        }

        GameManager.instance.Day = PlayerPrefs.GetInt("Day", 1);
        GameManager.instance.point = PlayerPrefs.GetFloat("Point", 0);
        GameManager.instance.weatherManager.currentWeather = (WeatherType)PlayerPrefs.GetInt("Weather", 0);

        Debug.Log("불러오기 완료");
    }
}