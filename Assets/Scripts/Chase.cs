using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform target;
    public float speed;

    void Update()
    {
        var step =  speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
