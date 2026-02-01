using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction moveAction;
    Transform Tr;
    void Start()
    {
        
        //Debug.Log("Hello World!");
        moveAction = InputSystem.actions.FindAction("Move");
        Tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 speedValue = moveValue * 10f * Time.deltaTime;
        Tr.Translate(speedValue);
        
    }
}
