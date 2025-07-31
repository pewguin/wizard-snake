using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    
    public TileBase GetTile(Vector3Int pos)
    {
        var tile = tilemap.GetTile(pos);
        if (tile != null)
        Debug.Log(tile.name);
        return tile;
    }
    public bool WallAt(Vector3Int pos)
    {
        return GetTile(pos) != null;
    }
}
