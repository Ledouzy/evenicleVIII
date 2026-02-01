using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    InputAction moveAction;
    Rigidbody2D Hunter;

    public float speed = 10f;
    private int steps = 0;
    [SerializeField] private int encounterRate=235;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        Hunter = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(Encounter), 2.0f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        Hunter.linearVelocity = movementVector * speed; 
    }

    void Encounter()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        //Debug.Log(movementVector);
        if (movementVector != new Vector2(0, 0))
        {
            steps++;
            int randomNumber = Random.Range(steps,256);
            
            Debug.Log("steps: "+steps);
            /*Debug.Log("random no:"+randomNumber);
            Debug.Log("encounterRate: "+encounterRate);
            */
            if (randomNumber > encounterRate)
            {
                Debug.Log("Encounter!");
                steps = 0;
            }
        }
    }
}
