using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCollider : MonoBehaviour
{
    public Side side;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Street street = collision.gameObject.GetComponent<Street>();
            street.Populate(side);
        }
    }
}
