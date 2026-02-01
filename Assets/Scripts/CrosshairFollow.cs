using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairFolow : MonoBehaviour
{
    private Vector2 mousePosition;
    [SerializeField] private Camera mainCam;

    void start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        transform.position = mousePos;
    }
}
