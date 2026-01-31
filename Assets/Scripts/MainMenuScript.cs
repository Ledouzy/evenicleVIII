using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    public int testVariable = 5;

    public GameObject UselessButton;
    public GameObject Dommy_Button;
    public GameObject Dommy_mommy;
    public GameObject Speech_Bubble;
    public GameObject QuitButton;

    void Start ()
    {
      UselessButton.SetActive (true);
      Dommy_Button.SetActive (false);
      QuitButton.SetActive (true);
      Dommy_mommy.SetActive (false);
      Speech_Bubble.SetActive (false);
    }

    //Don't mind this part, it's the setting thing joke
    public void clickThisButton()
    {
      UselessButton.SetActive (false);
      QuitButton.SetActive (false);
      Dommy_Button.SetActive (true);
      Dommy_mommy.SetActive (true);
      Speech_Bubble.SetActive (true);
    }

    public void Dommy_Button_click()
    {
      UselessButton.SetActive (true);
      QuitButton.SetActive (true);
      Dommy_Button.SetActive (false);
      Dommy_mommy.SetActive (false);
      Speech_Bubble.SetActive (false);
    }

    public void New_Game()
    {
        // Load game scene, need to set initial player position, but need level design first
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }

    public void OpenSettings()
    {
        //destroy(gameObject);
        GameObject targetObject = GameObject.Find("Button_setting");
        targetObject.SetActive(false);


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
