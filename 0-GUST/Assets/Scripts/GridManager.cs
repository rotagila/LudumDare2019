using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap pathTilemap;
    public TileBase pathTile;
    public Vector3Int[,] nodes;
    AStar aStar;
    List<Vector2Int> path = new List<Vector2Int>();
    BoundsInt bounds;

    public Grid grid { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        grid = GetComponent<Grid>();
        aStar = new AStar(bounds.size.x, bounds.size.y);
    }
    public void CreateGrid()
    {
        nodes = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                    nodes[i, j] = new Vector3Int(x, y, 0);
                else
                    nodes[i, j] = new Vector3Int(x, y, 1);
            }
        }
    }

    public List<Vector2Int> getPath(Vector3 start, Vector3 end)
    {
        CreateGrid();
        Vector2Int endPosOnGrid = getPosOnGrid((Vector2)end);
        Vector2Int startPosOnGrid = getPosOnGrid((Vector2)start);

        path = aStar.getPath(nodes, startPosOnGrid, endPosOnGrid);
        if (path == null)
            return null;

        DrawRoad(path);
        return path;
    }

    private void DrawRoad(List<Vector2Int> vects)
    {
        for (int i = 0; i < vects.Count; i++)
        {
            pathTilemap.SetTile(new Vector3Int(vects[i].x, vects[i].y, 0), pathTile);
        }
    }

    public Vector2Int getPosOnGrid(Vector2 pos)
    {
        return (Vector2Int)tilemap.WorldToCell(pos);
    }
}

