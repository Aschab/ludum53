using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowards : MonoBehaviour
{

    public Transform target;

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
