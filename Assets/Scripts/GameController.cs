using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public List<DeliverableType> active = new List<DeliverableType>();
    public List<DeliverableType> delivered = new List<DeliverableType>();

    public GameObject deliverable;
    public GameObject deliverableArea;
    public GameObject vehicle;
    public GameObject indicator;
    public TimerController timer;

    public float minDistance;
    public float maxDistance;

    public float generationCenter;

    public float difficulty = 1f;
    private float difficultyModifier = 0.5f;

    private VehicleController _vehicleController;

    void Start()
    {
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
        Vector2 areaPos = GetRandomPositionForArea();
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
        timer.AddTime(10f);
        delivered.Add(type);
        SpawnDeliverable();
    }

    private void SpawnDeliverable()
    {
        Vector2 deliverablePos = GetRandomPositionForSpawn();
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

    private bool CanSpawn(Vector2 pos)
    {
        return Physics2D.OverlapCircle(pos, generationCenter) == null;
    }

    private Vector2 GetRandomPositionForArea()
    {
        Vector2 areaPos = GetRandomPositionForSpawn();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");

        GameObject closestSpawnPoint = null;
        float closestDistance = Mathf.Infinity;
        float distance;

        foreach (GameObject spawnPoint in spawnPoints )
        {
            distance = Vector2.Distance(areaPos,spawnPoint.transform.position);

            if ( distance < closestDistance )
            {
                closestDistance = distance;
                closestSpawnPoint = spawnPoint;
            }
        }

        List<Transform> children = new List<Transform>();
        for (int i = 0; i < closestSpawnPoint.transform.childCount; i++) children.Add(closestSpawnPoint.transform.GetChild(i));

        Transform closestTransform = null;
        closestDistance = Mathf.Infinity;

        foreach (var spawnpoint in children)
        {
            if (IsColliderAtPoint(spawnpoint.position)) continue;
            distance = Vector2.Distance(areaPos,spawnpoint.position);
            if ( distance < closestDistance )
            {
                closestDistance = distance;
                closestTransform = spawnpoint;
            }
        }

        if (closestTransform)
        {
            return closestTransform.position;
        }

        return areaPos;
    }

    private Vector2 GetRandomPositionForSpawn()
    {
        Vector2 center = vehicle.transform.position;
        float distance = UnityEngine.Random.Range(minDistance, maxDistance);
        float radian = UnityEngine.Random.Range(-180f, 180f);
        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);
        Vector3 spawnPoint = new Vector3 (x,y)*distance;
        Vector2 ret = new Vector2(spawnPoint.x + center.x, spawnPoint.y + center.y);
        return ret;
    }

    public void IncreaseDifficulty()
    {
        difficulty += difficultyModifier;
    }

    private bool IsColliderAtPoint(Vector2 at)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(at, 0.1f);
        return colliders.Any(collider => !collider.isTrigger);
    }
}
