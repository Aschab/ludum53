using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampSpeed : MonoBehaviour
{
    public float dampValue;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<VehicleController>().Damp(dampValue);
        }
    }
  
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<VehicleController>().Damp(0f);
        }
    }
}
