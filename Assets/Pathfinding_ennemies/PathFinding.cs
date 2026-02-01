using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [SerializeField] private int gridHeight = 16;
    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int cellHeight = 1;
    [SerializeField] private int cellWidth = 1;

    [SerializeField] private bool generatePath;
    [SerializeField] private bool seeGrid;
    private Dictionary<Vector2, Cell> cells;

    private bool pathGenerated;


    [SerializeField] private List<Vector2> cellsToSearch;
    [SerializeField] private List<Vector2> cellsSearched;
    [SerializeField] private List<Vector2> foundPath;

    private float timeElapsed = 0.0f;
    [SerializeField] private int frameInterval = 5;

    [SerializeField] private Vector2 desiredposition = new Vector2(5, 7);
    [SerializeField] private Vector2 actualposition = new Vector2(0,1);


    // Update is called once per frame
    void Update()
    {
            timeElapsed += Time.deltaTime;
            if (generatePath && !pathGenerated)
            {
                GenerateGrid();
                if (timeElapsed >= Time.deltaTime * frameInterval)
                {
                    FindPath(actualposition, desiredposition);
                    timeElapsed = 0.0f;
                }
                pathGenerated = true;
            }
            else if (!generatePath)
            {
                pathGenerated = false;
            }

    }



    private void GenerateGrid()
    {
        cells = new Dictionary<Vector2, Cell>();
        for (float x = 0; x < gridWidth; x += cellWidth)
        {
            for (float y = 0; y < gridHeight; y += cellHeight)
            {
                Vector2 pos = new Vector2(x, y);
                cells.Add(pos, new Cell(pos));
            }
        }

        // This needs to be replaced with the actual wall positions (so i need the array of walls from somewhere)
        for (int i = 0; i <786; i++)
        {
            Vector2 pos = new Vector2(Random.Range(1,gridWidth-1), Random.Range(1,gridHeight-1));
            cells[pos].isWall = true;
        }
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        cellsSearched = new List<Vector2>();
        cellsToSearch = new List<Vector2>{ startPos };
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

            if  (cellToSearch == endPos)
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
        for (float x = cellPos.x -cellWidth; x <= cellWidth +cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellWidth; y <= cellWidth + cellPos.y; y += cellWidth)
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
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x) - Mathf.Abs((int)pos2.x), Mathf.Abs((int)pos1.y) - Mathf.Abs((int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;


        return lowest * 14 + horizontalMovesRequired * 10;

    }

    private void OnDrawGizmos()
    {
        if (!seeGrid || cells ==null)
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

            if (foundPath.Contains(kvp.Key))
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

        //constructor
        public Cell(Vector2 pos)
        {
            position = pos;
        }
    }
}
