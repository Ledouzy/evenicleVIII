using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNodeGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Node nodePrefab;
    public List<Node> nodeList = new List<Node>();

    void Start()
    {
        GenerateNodesFromTilemap();
    }

    void GenerateNodesFromTilemap()
    {
        nodeList.Clear();

        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(cellPosition))
                {
                    Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);
                    Node node = Instantiate(nodePrefab, worldPosition, Quaternion.identity);
                    nodeList.Add(node);
                }
            }
        }

        // Connect adjacent nodes
        ConnectNodes();
    }

    void ConnectNodes()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = i + 1; j < nodeList.Count; j++)
            {
                float distance = Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position);

        
                if (distance <= 1.1f)
                {
                    nodeList[i].connections.Add(nodeList[j]);
                    nodeList[j].connections.Add(nodeList[i]);
                }
            }
        }
    }
}