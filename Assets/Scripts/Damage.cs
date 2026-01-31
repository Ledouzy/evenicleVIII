using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Damage : MonoBehaviour
{

    public InputAction MoveAction;
    public PlayerDataManager PlayerDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerDataManager = FindObjectOfType<PlayerDataManager>();
        PlayerDataManager.health -= 1;
    }


}
