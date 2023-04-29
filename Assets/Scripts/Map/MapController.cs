using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapController : MonoBehaviour
{
    public float startX;
    public float startY;
    public float distanceX;
    public float distanceY;
    public GameObject fullStreet;

    private Dictionary<Vector2, GameObject> map = new Dictionary<Vector2, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        Vector2 start = new Vector2(0,0);
        CreateIfNotExist(start);
        Expand(start, Side.Left);
        Expand(start, Side.Right);
        Expand(start, Side.Top);
        Expand(start, Side.Bottom);
    }

    public void Expand(Vector2 from, Side to)
    {
        Vector2 v1 = new Vector2(from.x, from.y);
        Vector2 v2 = new Vector2(from.x, from.y);
        Vector2 v3 = new Vector2(from.x, from.y);

        switch(to)
        {
            case Side.Left:
                v1.x -= 1;
                v2.x -= 1;
                v3.x -= 1;
                v1.y += 1;
                v3.y -= 1;
                break;
            case Side.Right:
                v1.x += 1;
                v2.x += 1;
                v3.x += 1;
                v1.y += 1;
                v3.y -= 1;
                break;
            case Side.Top:
                v1.y += 1;
                v2.y += 1;
                v3.y += 1;
                v1.x += 1;
                v3.x -= 1;
                break;                
            case Side.Bottom:
                v1.y -= 1;
                v2.y -= 1;
                v3.y -= 1;
                v1.x += 1;
                v3.x -= 1;
                break;            
        }

        CreateIfNotExist(v1);
        CreateIfNotExist(v2);
        CreateIfNotExist(v3);
    }

    private void CreateIfNotExist(Vector2 pos)
    {
        if (!map.ContainsKey(pos)) 
        {
            Vector2 finalPos = new Vector2(pos.x * distanceX + startX, pos.y * distanceY + startY);
            GameObject newStreet = Instantiate(fullStreet, finalPos, Quaternion.identity) as GameObject;
            newStreet.transform.parent = transform;
            Street street = newStreet.gameObject.GetComponent<Street>();
            street.mapPos = pos;
            map.Add(pos, newStreet);
        }
    }


}
    
[Serializable]
public enum Side
{
    None,
    Left,
    Right,
    Top,
    Bottom
}
