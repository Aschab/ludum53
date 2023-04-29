using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitRigidBodies : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Deliverable" || collision.gameObject.tag == "DeliverableArea")
        {
            // Doesnt work :C 
            Collider2D collider = GetComponent<Collider2D>();
            collision.gameObject.transform.position = collider.ClosestPoint(collision.gameObject.transform.position);
        }
    }
}
