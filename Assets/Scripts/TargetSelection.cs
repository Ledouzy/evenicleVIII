using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Select()
    {
        Cursor.visible = true;
        Debug.Log("click");
    }
}
