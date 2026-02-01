using UnityEngine;
using UnityEngine.SceneManagement;

public class FleeScript : MonoBehaviour
{
    public void LoadFleeScene()
    {
        //loads map for now. Probably needs another Scene like map but is being chased or chasing the vampire.
        SceneManager.LoadSceneAsync(1);
    }
}
