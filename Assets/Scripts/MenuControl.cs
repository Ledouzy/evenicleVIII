using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public Rigidbody2D Hunter;

    void Start() {
    
        if (PlayerRetention.LoadedPlayerData == null) {
        PlayerRetention.LoadedPlayerData = new PlayerData();
        }
    }
    public void EnterMenu()
    {
        PlayerRetention.LoadedPlayerData.positions = new float[] { Hunter.position.x, Hunter.position.y, 0f };

        UnityEngine.SceneManagement.SceneManager.LoadScene("InventoryScene");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MapScene");
    }
}
