using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    InputAction moveAction;
    public Rigidbody2D Hunter;

    public float speed = 10f; 
    enum CardinalDirection
    {
        EAST = 0,
        NORTH = 90,
        WEST = 180,
        SOUTH = 270,
    }
    private int steps = 0;
    [SerializeField] private int encounterRate=235;
    [SerializeField] Animator animator;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        Hunter = GetComponent<Rigidbody2D>();
        //This is the position retention section
        if (PlayerRetention.LoadedPlayerData != null)
        {
            var player_previous_position = PlayerRetention.LoadedPlayerData.positions;
            transform.position = new Vector3(player_previous_position[0], player_previous_position[1], player_previous_position[2]);
        }
        InvokeRepeating(nameof(Encounter), 2.0f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
    
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        Hunter.linearVelocity = movementVector * speed; 
        animator.SetFloat("Speed", Hunter.linearVelocity.magnitude);
        //Debug.Log("x: "+ Hunter.linearVelocity.x + "y: "+Hunter.linearVelocity.y);
        if (Hunter.linearVelocity.y > 0)
        {
            //Debug.Log("North");
            animator.SetInteger("Direction", 0); // North
        } else if (Hunter.linearVelocity.y < 0)
        {
            //Debug.Log("South");
            animator.SetInteger("Direction", 3); // South
        } else if (Hunter.linearVelocity.x > 0)
        {
            //Debug.Log("East");
            animator.SetInteger("Direction", 1); // East
        } else if (Hunter.linearVelocity.x < 0)
        {
            //Debug.Log("West");
            animator.SetInteger("Direction", 2); // West
        }
    }

    void Encounter()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        //Debug.Log(movementVector);
        if (movementVector != new Vector2(0, 0))
        {
            steps++;
            int randomNumber = Random.Range(steps,256);
            
            //Debug.Log("steps: "+steps);
            /*Debug.Log("random no:"+randomNumber);
            Debug.Log("encounterRate: "+encounterRate);
            */
            if (randomNumber > encounterRate)
            {
                Debug.Log("Encounter!");
                steps = 0;
                PlayerRetention.LoadedPlayerData.positions = new float[] { Hunter.position.x, Hunter.position.y, 0f };
                SceneManager.LoadSceneAsync(3);
            }
        }

    }
}
