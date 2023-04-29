using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<DeliverableType> active = new List<DeliverableType>();
    public List<DeliverableType> delivered = new List<DeliverableType>();

    public GameObject deliverableArea;

    public float minXArea;
    public float minYArea;
    public float maxXArea;
    public float maxYArea;

    public void Grab(DeliverableType type)
    {
        active.Add(type);
        Vector2 areaPos = new Vector2(UnityEngine.Random.Range(minXArea, maxXArea), UnityEngine.Random.Range(minYArea, maxYArea));
        GameObject newArea = Instantiate(deliverableArea, areaPos, Quaternion.identity) as GameObject;

    }
}
