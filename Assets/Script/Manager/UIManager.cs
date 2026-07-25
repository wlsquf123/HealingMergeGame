using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AnimalList AnimalList;

    public Image MergedSelect; // 머지 선택
    public Button AnimalButton; // 동물 버튼

    public void OpenAnimallists(int Ranking, Vector3 mergePosition)
    {
        MergedSelect.gameObject.SetActive(true); // 머지 선택창 활성화
        GameObject[] x = AnimalList.GetAnimalList(++Ranking);

        for (int i = 0; i < x.Length; i++)
        {
            var newButton = Instantiate(AnimalButton, MergedSelect.transform);
            var ButtonText = newButton.GetComponentInChildren<Text>(); ;
            var name = x[i].GetComponent<Animal>().animalName;
            ButtonText.text = name;

            GameObject animalPrefab = x[i];

            newButton.onClick.AddListener(() => AnimalClick(animalPrefab, mergePosition)); // 얘 일단은 나중에 이해를 해야겠네 하하.
        }
    }

    public void AnimalClick(GameObject prefab, Vector3 spawnPosition) // 동물 선택
    {
        Instantiate(prefab, spawnPosition, transform.rotation);
        MergedSelect.gameObject.SetActive(false);

        for (int i = 0; i < MergedSelect.transform.childCount; i++)
        {
            Destroy(MergedSelect.transform.GetChild(i).gameObject);
        }
    }
}