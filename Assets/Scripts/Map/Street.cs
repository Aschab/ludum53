using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{

    public Vector2 mapPos;

    public List<GameObject> prefabs1x1;
    public List<GameObject> prefabs2x2;
    public List<GameObject> prefabs3x3;

    public List<Vector2> occupied;

    public Vector2 grid;
    public Vector2 start1x1;
    public Vector2 start2x2;
    public Vector2 start3x3;

    public float innerDistanceX;
    public float innerDistanceY;

    public float odds1x1;
    public float odds2x2;
    public float odds3x3;

    public void Populate(Side side)
    {
        MapController mp = transform.parent.gameObject.GetComponent<MapController>();
        Vector2 expandFrom = new Vector2(mapPos.x, mapPos.y);
        switch(side)
        {
            case Side.Left:
                expandFrom.x -= 1;
                break;
            case Side.Right:
                expandFrom.x += 1;
                break;
            case Side.Top:
                expandFrom.y += 1;
                break;                
            case Side.Bottom:
                expandFrom.y -= 1;
                break;            
        }
        mp.Expand(expandFrom, side);    
    }

    void Start()
    {
        odds3x3 = (odds3x3/grid.x)/grid.y;
        odds2x2 = (odds2x2/grid.x)/grid.y;
        odds1x1 = (odds1x1/grid.x)/grid.y;

        FormCity();
    }

    private void FormCity()
    {
        Vector2 size = new Vector2(3, 3);
        FormBySize(prefabs3x3, start3x3, size, odds3x3);
        size.x = 2;
        size.y = 2;
        FormBySize(prefabs2x2, start2x2, size, odds2x2);
        size.x = 1;
        size.y = 1;
        FormBySize(prefabs1x1, start1x1, size, odds1x1);
    }

    private void FormBySize(List<GameObject> prefabs, Vector2 startPosition, Vector2 size, float probability)
    {
        int amount = prefabs.Count;

        for (int x = 0; x <= grid.x - size.x; x++) 
        {
            for (int y = 0; y <= grid.y - size.y; y++) 
            {
                if (IsFree(x, y, size) && Random.Range(0f, 1f) < probability)
                {
                    int prefab = Random.Range(0, amount);              
                    Vector2 buildingPos = new Vector2(startPosition.x + innerDistanceX*x, startPosition.y + innerDistanceY*y);
                    GameObject newBuilding = Instantiate(prefabs[prefab], transform) as GameObject;
                    newBuilding.transform.localPosition = buildingPos;
                    
                    for (int occupiedX = x; occupiedX < x+size.x; occupiedX++)
                    {
                        for (int occupiedY = y; occupiedY < y+size.y; occupiedY++)
                        {
                            Vector2 occupant = new Vector2(occupiedX, occupiedY);
                            occupied.Add(occupant);
                        }
                    }
                }
            }
        }
    }

    private bool IsFree(int x, int y, Vector2 size)
    {

        for (int i = x; i < x + size.x; i++)
        {
            for (int j = y; j < y + size.y; j++)
            {
                if (occupied.Contains(new Vector2(i, j)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
