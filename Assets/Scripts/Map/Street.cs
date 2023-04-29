using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{

    public Vector2 mapPos;

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
}
