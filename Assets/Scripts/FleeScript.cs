using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FleeScript : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

/*
    private void Start()
    {
        
        //yield return new WaitForSeconds(2f);

    }*/

    public void LoadFleeScene()
    {
        //loads map for now. Probably needs another Scene like map but is being chased or chasing the vampire.
        audioManager.PlaySFX(audioManager.flee);

        SceneManager.LoadSceneAsync(1);
    }
}
