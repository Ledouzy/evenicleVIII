using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Controller : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public int panicMultiplier = 1;
    public Node currentNode;
    public List<Node> path = new List<Node>();

    public Collider2D HunterCollider;

    public enum StateMachine
    {
        Patrol,
        Engage,
        Evade
    }

    public StateMachine currentState;
    public playerMovement player;
    public float speed = 3f;

    private void Start()
    {
        curHealth = maxHealth;

        // Auto-find player if not assigned
        if (player == null)
        {
            player = FindObjectOfType<playerMovement>();
        }
        if (currentNode == null)
        {
            Node[] allNodes = FindObjectsOfType<Node>();
            if (allNodes != null && allNodes.Length > 0)
            {
                currentNode = allNodes[Random.Range(0, allNodes.Length)];
                transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, transform.position.z);
            }
        }
    }

    private void Update()
    {
        if (currentNode == null)
        {
            Node[] allNodes = FindObjectsOfType<Node>();
            if (allNodes != null && allNodes.Length > 0)
            {
                currentNode = allNodes[Random.Range(0, allNodes.Length)];
                transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, transform.position.z);
            }


            if (player == null || currentNode == null)
            return;

            }

        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            case StateMachine.Engage:
                Engage();
                break;
            case StateMachine.Evade:
                Evade();
                break;
        }

        bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 5.0f;

        if (!playerSeen && currentState != StateMachine.Patrol && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Patrol;
            path.Clear();
        }
        else if (playerSeen && currentState != StateMachine.Engage && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Engage;
            path.Clear();
        }
        else if (currentState != StateMachine.Evade && curHealth <= (maxHealth * 20) / 100)
        {
            panicMultiplier = 2;
            currentState = StateMachine.Evade;
            path.Clear();
        }

        CreatePath();
    }

    void Patrol()
    {
        if (AStarManager.instance == null)
            return;

        if (path.Count == 0)
        {
            Node[] allNodes = AStarManager.instance.AllNodes();
            if (allNodes != null && allNodes.Length > 0)
            {
                Node targetNode = allNodes[Random.Range(0, allNodes.Length)];
                if (targetNode != null)
                {
                    path = AStarManager.instance.GeneratePath(currentNode, targetNode);
                    if (path == null)
                        path = new List<Node>();
                }
            }
        }
    }

    void Engage()
    {
        if (AStarManager.instance == null || player == null)
            return;

        if (path.Count == 0)
        {
            Node targetNode = AStarManager.instance.FindNearestNode(player.transform.position);
            if (targetNode != null)
            {
                path = AStarManager.instance.GeneratePath(currentNode, targetNode);
                if (path == null)
                    path = new List<Node>();
            }
        }
    }

    void Evade()
    {
        if (AStarManager.instance == null || player == null)
            return;

        if (path.Count == 0)
        {
            Node targetNode = AStarManager.instance.FindFurthestNode(player.transform.position);
            if (targetNode != null)
            {
                path = AStarManager.instance.GeneratePath(currentNode, targetNode);
                if (path == null)
                    path = new List<Node>();
            }
        }
    }

    public void CreatePath()
    {
        if (path != null && path.Count > 0)
        {
            int x = 0;
            if (path[x] != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), (speed * panicMultiplier) * Time.deltaTime);
                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
                {
                    currentNode = path[x];
                    path.RemoveAt(x);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
         UnityEngine.SceneManagement.SceneManager.LoadScene("BattleSceneBoss");
    }
}