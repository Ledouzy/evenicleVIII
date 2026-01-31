using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{

    public GameObject Inventory_Button;
    public GameObject Item_Buttom;
    public GameObject Status_Button;
    public GameObject Equip_Button;
    public GameObject Save_Button;
    public GameObject Load_Button;
    public GameObject Exit_Button;


    void enableall()
    {
        Inventory_Button.SetActive(true);
        Item_Buttom.SetActive(true);
        Status_Button.SetActive(true);
        Equip_Button.SetActive(true);
        Save_Button.SetActive(true);
        Load_Button.SetActive(true);
        Exit_Button.SetActive(true);
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
    }


    void Start()
    {
        enableall();
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
        
    }

    public void Load_Button_Pressed()
    {
        
    }

    public void Exit_Button_Pressed()
    {
        
    }
}
