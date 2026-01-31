using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class Battle_System : MonoBehaviour
{
    public GameObject playerPrefab;



    public GameObject enemyPrefab;


//MAYBE DELETE TRANSFORM??? NOT SURE
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText; 
    //public TMPText dialogueText;
    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>(); 


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>(); 

        

        dialogueText.text = "text: enemy appeared!" + enemyUnit.unitName + "appraoches";

    }
}
