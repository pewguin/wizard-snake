using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Cell[,] cells;
    public Tilemap tilemap;
    public int width;
    public int height;

    public enum CellType
    {
        Wall,
    }
    public class Cell
    {
        public CellType type;
        public TileBase tile;
    }
    private void Awake()
    {
        cells = new Cell[width, height];
    }
    private void ImportFromTilemap()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile == null) continue;

            if (tile is WallTile)
            {
                cells[pos.x, pos.y].type = CellType.Wall;
                cells[pos.x, pos.y].tile = tile;
            }
        }
    }
}
