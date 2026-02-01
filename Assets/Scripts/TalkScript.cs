using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkScript : MonoBehaviour
{
    public void LoadTalkScene()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
