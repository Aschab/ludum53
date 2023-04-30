using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<DeliverableType> active = new List<DeliverableType>();
    public List<DeliverableType> delivered = new List<DeliverableType>();

    public GameObject deliverable;
    public GameObject deliverableArea;
    private GameObject vehicle;
    public GameObject indicator;
    private GameData data;
    public float minXArea;
    public float minYArea;
    public float maxXArea;
    public float maxYArea;

    public float generationCenter;

    private int difficulty = 1;
    private int difficultyModifier = 1;

    private VehicleController _vehicleController;

    void Awake()
    {
        data = Resources.Load<GameData>("GameData");
        vehicle = Instantiate(data.selectedVehicle.gameObject, Vector3.zero, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().target = vehicle;
    }

    void Start()
    {
        vehicle.transform.SetParent(null);
        _vehicleController = vehicle.GetComponent<VehicleController>();
        Init();
    }

    public void Init()
    {
        difficulty = 1;
        SpawnDeliverable();        
    }

    public void Grab(DeliverableType type)
    {
        active.Add(type);
        Vector2 areaPos = GetRandomPositionFree(new Vector2(minXArea*difficulty, minYArea*difficulty), new Vector2(maxXArea*difficulty, maxYArea*difficulty));
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
        Debug.Log("here");
        Vector2 deliverablePos = GetRandomPositionFree(new Vector2(minXArea*difficulty, minYArea*difficulty), new Vector2(maxXArea*difficulty, maxYArea*difficulty));
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
                Debug.Log("here2");

    }

    private bool CanSpawn(Vector2 pos)
    {
        return Physics2D.OverlapCircle(pos, generationCenter) == null;
    }

    private Vector2 GetRandomPositionFree(Vector2 min, Vector2 max)
    {
        // accidental infinite loops food :D 
        while (true)
        {
            Vector2 areaPos = new Vector2(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y));
            if (CanSpawn(areaPos))
            {
                return areaPos;
            }
        }
    }

    public void IncreaseDifficulty()
    {
        difficulty += difficultyModifier;
    }
}
