using UnityEngine;

public class AnimalList : MonoBehaviour
{
    public GameObject[] AnimalLv1 = new GameObject[2];
    public GameObject[] AnimalLv2 = new GameObject[3];
    public GameObject[] AnimalLv3 = new GameObject[3];
    public GameObject[] AnimalLv4;
    public GameObject[] AnimalLv5;

    public GameObject[] GetAnimalList(int index)
    {
        switch (index)
        {
            case 2:
                return AnimalLv2;
            case 3:
                return AnimalLv3;
            case 4:
                return AnimalLv4;
            case 5:
                return AnimalLv5;
        }
        return null;
    }
}
