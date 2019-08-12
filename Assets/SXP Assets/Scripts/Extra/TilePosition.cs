using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition : MonoBehaviour
{
    [SerializeField] int xOffset;
    [SerializeField] int yOffset;

    public Vector2 GetTilePosition
    {
        get
        {
            int x = Mathf.FloorToInt(transform.localPosition.x);
            int y = Mathf.FloorToInt(transform.localPosition.y);
            
            return new Vector2(x + xOffset, y + yOffset);
        }
    }
}
