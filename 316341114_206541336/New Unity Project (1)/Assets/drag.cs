using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class drag : MonoBehaviour
{
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }
}