using UnityEngine;

public class Save : MonoBehaviour
{
    public void GameSave()
    {
        var objects = FindObjectsByType<SaveID>(FindObjectsSortMode.None);

        PlayerPrefs.SetInt("Object", objects.Length);

        for (int i = 0; i < objects.Length; i++)
        {
            var obj = objects[i];

            PlayerPrefs.SetString("id" + i, obj.id);


        }
    }
}
