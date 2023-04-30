using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitRigidBodies : MonoBehaviour
{
    public Vector2 size;
    public float radius;
    public Collider2D[] colliders;

    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
        if(colliders.Length > 1)
        {
            foreach (Collider2D col in colliders) {
                if (col.gameObject.tag == "Deliverable" || col.gameObject.tag == "DeliverableArea")
                {
                    Vector2 from = transform.position;
                    Vector2 to = col.gameObject.transform.position;
                    float angle = Vector2.SignedAngle(from, to);

                    float radian = angle*Mathf.Deg2Rad;
                    float x = Mathf.Cos(radian);
                    float y = Mathf.Sin(radian);
                    Vector3 spitPoint = new Vector3 (x,y,0)*radius;
                    spitPoint = transform.position + spitPoint;
                    col.gameObject.transform.position = spitPoint;
                }
            }
        }
    }
}
