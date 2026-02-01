using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PathFinding : MonoBehaviour
{
    [SerializeField] GameObject TheHunter;
    playerMovement playermove;
    public Vector3 playerPos;


    void Awake()
    {
        if (TheHunter == null)
        {
            Debug.LogError("TheHunter not assigned!");
            return;
        }

        playermove = TheHunter.GetComponent<playerMovement>();

        if (playermove == null)
        {
            Debug.LogError("playerMovement component not found!");
            return;
        }

        if (tilemap == null)
        {
            Debug.LogError("Tilemap not assigned!");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;

        gridWidth = bounds.size.x;
        gridHeight = bounds.size.y;

        BuildWallsFromTilemap();
    }

    [SerializeField] private int gridHeight = 16;
    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int cellHeight = 1;
    [SerializeField] private int cellWidth = 1;

    [SerializeField] private bool seeGrid;
    private Dictionary<Vector2, Cell> cells;



    [SerializeField] private List<Vector2> cellsToSearch;
    [SerializeField] private List<Vector2> cellsSearched;
    [SerializeField] private List<Vector2> foundPath;

    private float timeElapsed = 0.0f;
    [SerializeField] private int frameInterval = 100;

    [SerializeField] Tilemap tilemap;


    void Update()
    {
        // Null checks
        if (playermove == null || playermove.Hunter == null || cells == null)
            return;

        playerPos = playermove.Hunter.position;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= Time.deltaTime * frameInterval)
        {
            // Get ENEMY position (this GameObject)
            Vector3Int enemyCell = tilemap.WorldToCell(transform.position);
            Vector2 enemyGridPos = new Vector2(enemyCell.x, enemyCell.y);

            // Get PLAYER position
            Vector3Int playerCell = tilemap.WorldToCell(playerPos);
            Vector2 playerGridPos = new Vector2(playerCell.x, playerCell.y);

            if (!cells.ContainsKey(enemyGridPos))
            {
                Debug.LogWarning($"Enemy at {enemyGridPos} is outside grid bounds!");
                return;
            }

            if (!cells.ContainsKey(playerGridPos))
            {
                Debug.LogWarning($"Player at {playerGridPos} is outside grid bounds!");
                return;
            }

            FindPath(enemyGridPos, playerGridPos);
            timeElapsed = 0f;
        }
    }

    void BuildWallsFromTilemap()
    {
        cells = new Dictionary<Vector2, Cell>();
        BoundsInt bounds = tilemap.cellBounds;

        Debug.Log($"Building grid with bounds: {bounds}");

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector2 gpos = new Vector2(x, y);
                cells.Add(gpos, new Cell(gpos));

                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePos))
                {
                    cells[gpos].isWall = true;
                }
            }
        }

        Debug.Log($"Grid built with {cells.Count} cells");
    }


    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        // Safety checks
        if (!cells.ContainsKey(startPos))
        {
            Debug.LogError($"Start position {startPos} not in grid!");
            return;
        }

        if (!cells.ContainsKey(endPos))
        {
            Debug.LogError($"End position {endPos} not in grid!");
            return;
        }

        cellsSearched = new List<Vector2>();
        cellsToSearch = new List<Vector2> { startPos };
        foundPath = new List<Vector2>();

        Cell startCell = cells[startPos];
        startCell.gCost = 0;
        startCell.hCost = GetDistance(startPos, endPos);
        startCell.fCost = GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];

            foreach (Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if (c.fCost < cells[cellToSearch].fCost || c.fCost == cells[cellToSearch].fCost && c.hCost < cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }

            cellsToSearch.Remove(cellToSearch);
            cellsSearched.Add(cellToSearch);

            if (cellToSearch == endPos)
            {
                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    foundPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                foundPath.Add(startPos);
                return;
            }

            SearchCellNeighbors(cellToSearch, endPos);
        }
    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);
                if (cells.TryGetValue(neighborPos, out Cell c) && !cellsSearched.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbor = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbor < cells[neighborPos].gCost)
                    {
                        Cell neighborNode = cells[neighborPos];

                        neighborNode.connection = cellPos;
                        neighborNode.gCost = GcostToNeighbor;
                        neighborNode.hCost = GetDistance(neighborPos, endPos);
                        neighborNode.fCost = neighborNode.gCost + neighborNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private int GetDistance(Vector2 pos1, Vector2 pos2)
    {
        int distX = Mathf.Abs((int)pos1.x - (int)pos2.x);
        int distY = Mathf.Abs((int)pos1.y - (int)pos2.y);

        int lowest = Mathf.Min(distX, distY);
        int highest = Mathf.Max(distX, distY);
        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    private void OnDrawGizmos()
    {
        if (!seeGrid || cells == null)
        { return; }

        foreach (KeyValuePair<Vector2, Cell> kvp in cells)
        {
            if (!kvp.Value.isWall)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }

            if (foundPath != null && foundPath.Contains(kvp.Key))
            {
                Gizmos.color = Color.magenta;
            }

            Gizmos.DrawCube(kvp.Key + (Vector2)transform.position, new Vector3(cellWidth, cellHeight, 0));
        }
    }

    private class Cell
    {
        public Vector2 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector2 connection;
        public bool isWall;

        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}