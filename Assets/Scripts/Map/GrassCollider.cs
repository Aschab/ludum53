using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class GrassCollider : MonoBehaviour
{
    [SerializeField] private BoolGameEvent onGrassEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") onGrassEvent.Raise(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") onGrassEvent.Raise(false);
    }
}
