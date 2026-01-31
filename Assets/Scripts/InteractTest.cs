using UnityEngine;

public class InteractTest : MonoBehaviour, Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InteractAction(GameObject CallSource)
    {
        Debug.Log(Random.Range(0.0f, 100.0f));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
