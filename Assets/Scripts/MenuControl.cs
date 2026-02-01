using UnityEngine;

public class MenuControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EnterMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InventoryMenu");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MapScene");
    }
}
