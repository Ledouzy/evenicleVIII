using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    public int testVariable = 5;
    public void New_Game()
    {
        // Load game scene, need to set initial player position, but need level design first
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }

    public void OpenSettings()
    {
        //destroy(gameObject);
    }

    public void Load_Game()
    {
        //Depending on if multiple game scene we might need to modify this
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");

        PlayerDataManager playerDataManager = FindObjectOfType<PlayerDataManager>();
        if (playerDataManager != null)
        {
            playerDataManager.LoadGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #elif UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
}
