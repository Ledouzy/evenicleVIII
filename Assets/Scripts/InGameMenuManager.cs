using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InGameMenuManager : MonoBehaviour
{

    public GameObject Inventory_Button;
    public GameObject Item_Buttom;
    public GameObject Status_Button;
    public GameObject Equip_Button;
    public GameObject Save_Button;
    public GameObject Load_Button;
    public GameObject Exit_Button;
    public GameObject placeholder_button;
    public GameObject FUCK_GO_BACK;
    //public playerMovement saveData;
    public Transform playerTransform;

    public GameObject Hunter;
    //Use Save for position and load that in new scene

    //void Awake()
    //{
    //    saveData = Hunter.GetComponent<playerMovement>();
    //}


    //public InputAction EscapeSequence;

    
    void enableall()
    {
        Inventory_Button.SetActive(true);
        Item_Buttom.SetActive(true);
        Status_Button.SetActive(true);
        Equip_Button.SetActive(true);
        Save_Button.SetActive(true);
        Load_Button.SetActive(true);
        Exit_Button.SetActive(true);
        placeholder_button.SetActive(false);
    }
    void disableall()
    {
        Inventory_Button.SetActive(false);
        Item_Buttom.SetActive(false);
        Status_Button.SetActive(false);
        Equip_Button.SetActive(false);
        Save_Button.SetActive(false);
        Load_Button.SetActive(false);
        Exit_Button.SetActive(false);
        placeholder_button.SetActive(true);
    }


    void MenuInitialize()
    {
        enableall();
    }

    void Start()
    {
        //saveData.positions = new float[] { transform.position.x, transform.position.y,0f };
        //EscapeSequence.Enable();
        enableall();
        placeholder_button.SetActive(false);
    }

    public void Inventory_Button_Pressed()
    {
        disableall();
    }

    public void Item_Buttom_Pressed()
    {
        disableall();
    }

    public void Status_Button_Pressed()
    {
        disableall();
    }

    public void Equip_Button_Pressed()
    {
        disableall();
    }

    public void Save_Button_Pressed()
    {
        PlayerDataManager playerDataManager = FindObjectOfType<PlayerDataManager>();
        if (playerDataManager != null)
        {
            playerDataManager.SaveGame();
        }
    }

    public void Load_Button_Pressed()
    {
        PlayerDataManager playerDataManager = FindObjectOfType<PlayerDataManager>();
        if (playerDataManager != null)
        {
            playerDataManager.LoadGame();
        }
    }

    public void Exit_Button_Pressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

    public void Placeholder_Button_Pressed()
    {
        enableall();
    }

    public void F()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        //playerTransform.position = new Vector3(saveData.positions[0], saveData.positions[1], saveData.positions[2]);
    }


}
