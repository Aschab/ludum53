using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampSpeed : MonoBehaviour
{
    public float dampValue;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<VehicleController>().Damp(dampValue);
            if (audio)
            {
                audio.Play();
            }
        }
    }
  
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<VehicleController>().Damp(0f);
            if (audio)
            {
                audio.Stop();
            }
        }
    }
}
