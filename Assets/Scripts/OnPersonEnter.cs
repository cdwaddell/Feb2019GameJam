using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPersonEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

        }
    }
}
