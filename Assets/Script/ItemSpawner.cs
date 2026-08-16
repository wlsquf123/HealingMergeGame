using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemsOBJ;
    public float Timer;

    private void Update()
    {
        var trees = FindObjectsByType<TreeEnter>(FindObjectsSortMode.None);

        if (trees.Length == 0) return; // 나무가 0개면 돌아가라

        int random = Random.Range(0, trees.Length); // 나무중 하나 랜덤
        TreeEnter treeEnter = trees[random];
        
        Timer += Time.deltaTime;
        if (Timer >= 12.5f)
        {
            Instantiate(itemsOBJ, treeEnter.transform.position + treeEnter.transform.forward * 3.5f + Vector3.up * 15f, transform.rotation);
            Timer -= 12.5f;
        }
    }
}