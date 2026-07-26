using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    private void OnMouseDrag()
    {
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.transform.position.y;

        transform.position = Camera.main.ScreenToWorldPoint(mousPos);
    }
}