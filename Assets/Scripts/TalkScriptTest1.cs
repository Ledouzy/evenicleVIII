using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkScriptTest1 : MonoBehaviour
{
    public void TalkScene1()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
