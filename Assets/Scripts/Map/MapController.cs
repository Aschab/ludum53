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
        Vector2 v11 = new Vector2(from.x, from.y);
        Vector2 v12 = new Vector2(from.x, from.y);
        Vector2 v13 = new Vector2(from.x, from.y);
        Vector2 v14 = new Vector2(from.x, from.y);
        Vector2 v15 = new Vector2(from.x, from.y);
        Vector2 v21 = new Vector2(from.x, from.y);
        Vector2 v22 = new Vector2(from.x, from.y);
        Vector2 v23 = new Vector2(from.x, from.y);
        Vector2 v24 = new Vector2(from.x, from.y);
        Vector2 v25 = new Vector2(from.x, from.y);

        switch(to)
        {
            case Side.Left:
                v11.x -= 1;
                v12.x -= 1;
                v13.x -= 1;
                v14.x -= 1;
                v15.x -= 1;
                v21.x -= 2;
                v22.x -= 2;
                v23.x -= 2;
                v24.x -= 2;
                v25.x -= 2;
                v11.y += 1;
                v12.y += 2;
                v14.y -= 1;
                v15.y -= 2;
                v21.y += 1;
                v22.y += 2;
                v24.y -= 1;
                v25.y -= 2;
                break;
            case Side.Right:
                v11.x += 1;
                v12.x += 1;
                v13.x += 1;
                v14.x += 1;
                v15.x += 1;
                v21.x += 2;
                v22.x += 2;
                v23.x += 2;
                v24.x += 2;
                v25.x += 2;
                v11.y += 1;
                v12.y += 2;
                v14.y -= 1;
                v15.y -= 2;
                v21.y += 1;
                v22.y += 2;
                v24.y -= 1;
                v25.y -= 2;
                break;
            case Side.Top:
                v11.y += 1;
                v12.y += 1;
                v13.y += 1;
                v14.y += 1;
                v15.y += 1;
                v21.y += 2;
                v22.y += 2;
                v23.y += 2;
                v24.y += 2;
                v25.y += 2;
                v11.x += 1;
                v12.x += 2;
                v14.x -= 1;
                v15.x -= 2;
                v21.x += 1;
                v22.x += 2;
                v24.x -= 1;
                v25.x -= 2;
                break;                
            case Side.Bottom:
                v11.y -= 1;
                v12.y -= 1;
                v13.y -= 1;
                v14.y -= 1;
                v15.y -= 1;
                v21.y -= 2;
                v22.y -= 2;
                v23.y -= 2;
                v24.y -= 2;
                v25.y -= 2;
                v11.x += 1;
                v12.x += 2;
                v14.x -= 1;
                v15.x -= 2;
                v21.x += 1;
                v22.x += 2;
                v24.x -= 1;
                v25.x -= 2;
                break;            
        }

        CreateIfNotExist(v11);
        CreateIfNotExist(v12);
        CreateIfNotExist(v13);
        CreateIfNotExist(v14);
        CreateIfNotExist(v15);
        CreateIfNotExist(v21);
        CreateIfNotExist(v22);
        CreateIfNotExist(v23);
        CreateIfNotExist(v24);
        CreateIfNotExist(v25);
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
