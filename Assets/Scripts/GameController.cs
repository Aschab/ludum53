using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<DeliverableType> active = new List<DeliverableType>();
    public List<DeliverableType> delivered = new List<DeliverableType>();

    public GameObject deliverable;
    public GameObject deliverableArea;
    public GameObject vehicle;
    public GameObject indicator;

    public float minXArea;
    public float minYArea;
    public float maxXArea;
    public float maxYArea;

    private VehicleController _vehicleController;

    void Start()
    {
        _vehicleController = vehicle.GetComponent<VehicleController>();
        SpawnDeliverable();
    }

    public void Grab(DeliverableType type)
    {
        active.Add(type);
        Vector2 areaPos = new Vector2(UnityEngine.Random.Range(minXArea, maxXArea), UnityEngine.Random.Range(minYArea, maxYArea));
        GameObject newArea = Instantiate(deliverableArea, areaPos, Quaternion.identity) as GameObject;
        GameObject pointing = GameObject.Find("PointTowards");
        if (pointing != null) {
            Destroy(pointing);
        }
        GameObject newIndicator = Instantiate(indicator, Vector2.zero, Quaternion.identity) as GameObject;
        newIndicator.transform.parent = vehicle.transform;
        newIndicator.transform.localPosition = Vector2.zero;
        newIndicator.transform.name = "PointTowards";
        newIndicator.GetComponent<PointTowards>().target = newArea.transform;

    }

    public void Deliver(DeliverableType type)
    {
        delivered.Add(type);
        SpawnDeliverable();
    }

    private void SpawnDeliverable()
    {
        Vector2 deliverablePos = new Vector2(UnityEngine.Random.Range(minXArea, maxXArea), UnityEngine.Random.Range(minYArea, maxYArea));
        GameObject newDeliverable = Instantiate(deliverable, deliverablePos, Quaternion.identity) as GameObject;
        GameObject pointing = GameObject.Find("PointTowards");
        if (pointing != null) {
            Destroy(pointing);
        }
        GameObject newIndicator = Instantiate(indicator, Vector2.zero, Quaternion.identity) as GameObject;
        newIndicator.transform.parent = vehicle.transform;
        newIndicator.transform.localPosition = Vector2.zero;
        newIndicator.transform.name = "PointTowards";
        newIndicator.GetComponent<PointTowards>().target = newDeliverable.transform;
    }
}
