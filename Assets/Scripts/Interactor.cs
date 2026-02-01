using UnityEngine;
using UnityEngine.InputSystem;
interface Interactable
{
    public void InteractAction (GameObject CallSource);

}

public class Interactor : MonoBehaviour
{
    public float InteractRange = 0.5f;
    public Transform InteractSource;
    public LayerMask InteractionLayer; 

    InputAction InteractButton;
   
    void Start()
    {
        InteractButton = InputSystem.actions.FindAction("Interact");

        
    }
    void Update()
    {
        if (InteractButton.WasPressedThisFrame())
        {
    
            CheckInteractions();
        }
        
    }
    void CheckInteractions()
    {
        Collider2D[] Colliders = 
        Physics2D.OverlapCircleAll(InteractSource.position, 
        InteractRange, InteractionLayer);

        foreach (var col in Colliders)
        {
            if (col.TryGetComponent(out Interactable Object))
            {
                Object.InteractAction(this.gameObject);
            }
            
        }
    }

}
