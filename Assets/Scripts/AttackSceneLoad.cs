using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackSceneLoad : MonoBehaviour
{
    public void AttackScene1()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
