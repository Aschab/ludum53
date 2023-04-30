using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float angle = (float) ((int)Random.Range(0, 3)) * 90f;
        transform.Rotate(0f, 0f, angle);
    }
}
