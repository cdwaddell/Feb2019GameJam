using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Order Order { get; set; }

    public void Update()
    {
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collider = GetComponent<CapsuleCollider2D>();

        if (Input.GetMouseButtonDown(0) && collider.OverlapPoint(point))
        {
            OnUserClicked(new CustomerClickedEventArgs());
        }
    }

    protected virtual void OnUserClicked(CustomerClickedEventArgs e)
    {
        PlayerTransitioned?.Invoke(gameObject, e);
    }

    public event EventHandler<CustomerClickedEventArgs> PlayerTransitioned;
}
