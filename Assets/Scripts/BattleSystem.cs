using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

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

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        //Wrap coroutine for time delay
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        //NAME PLAYER UNDER PLAYER COMPONENTS
        playerUnit = playerGO.GetComponent<Unit>(); 


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        //NAME ENEMY UNDER ENEMY COMPONENTS
        enemyUnit = enemyGO.GetComponent<Unit>(); 

        
        Debug.Log("test:"+enemyUnit.unitName);
        dialogueText.text = "Enemy appeared!" + enemyUnit.unitName + " approaches";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);


        //time delay
        yield return new WaitForSeconds(2f);


        //begins the turns
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        //state = BattleState.ENEMYTURN;
        //EnemyTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        //change hp from damage
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = enemyUnit.unitName + " took damage.";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            dialogueText.text = enemyUnit.unitName + " died. you won.";
            EndBattle();

            //won
        }
        else
        {
            //enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won";
        }      
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost";
        }      
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "it is now the enemy's turn. Enemy attacked ~~~~";
        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();

        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void PlayerTurn()
    {
        dialogueText.text = "it is now the player's turn.";

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return; 
        }

        StartCoroutine(PlayerAttack());
    }



}
