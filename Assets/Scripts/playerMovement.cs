using UnityEngine;
using UnityEngine.InputSystem;

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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
      
        Hunter = GetComponent<Rigidbody2D>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
    
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        
        Hunter.linearVelocity = movementVector * speed;
       
        
    }
}
