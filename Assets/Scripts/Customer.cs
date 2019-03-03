using UnityEngine;

public class Customer : MonoBehaviour
{
    public void Update()
    {
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collider = GetComponent<CapsuleCollider2D>();

        if (Input.GetMouseButtonDown(0) && collider.OverlapPoint(point))
        {
        }
    }
}
