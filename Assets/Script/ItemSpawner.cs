using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public float Timer;

    private void Update()
    {

        var trees = FindObjectsByType<TreeEnter>(FindObjectsSortMode.None);

        if (trees.Length == 0) return; // 나무가 0개면 돌아가라

        int random = Random.Range(0, trees.Length); // 나무중 하나 랜덤
        int randomItem = Random.Range(0, itemPrefabs.Count); // 아이템 랜덤
        TreeEnter treeEnter = trees[random];
        
        Timer += Time.deltaTime;
        if (Timer >= 6f)
        {
            Instantiate(itemPrefabs[randomItem], treeEnter.transform.position, transform.rotation);
            Timer = 0;
        }
    }

}
