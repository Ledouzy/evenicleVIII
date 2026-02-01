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

    public Button attackButton;
    public Button itemButton;
    public Button fleeButton;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText; 
    //public TMPText dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    /**/


    void Start()
    {
        
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
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
        dialogueText.text = "Enemy appeared! " + enemyUnit.unitName + " approaches!";
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

    IEnumerator PlayerAttack(float multiplier)
    {
        bool isDead = enemyUnit.TakeDamage((int)(playerUnit.damage*multiplier));

        //change hp from damage
        enemyHUD.SetHP(enemyUnit.currentHP);
        if (multiplier > 1)
        {
            dialogueText.text = "Critical Hit! " + enemyUnit.unitName + " took " + playerUnit.damage*multiplier + " damage!";
        } else if (multiplier < 1)
        {
            dialogueText.text = enemyUnit.unitName + " blocked the attack! " + enemyUnit.unitName + " took " + playerUnit.damage*multiplier + " damage!";
        } else {
            dialogueText.text = enemyUnit.unitName + " took " + playerUnit.damage*multiplier + " damage!";
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            dialogueText.text = enemyUnit.unitName + " died. You won!";
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
            dialogueText.text = "You won.";
        }      
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost.";
        }      
    }

    IEnumerator EnemyTurn()
    {
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        audioManager.PlaySFX(audioManager.playerDealDmg);

        dialogueText.text = "It is now the enemy's turn. "+ playerUnit.unitName + " took "+ enemyUnit.damage + " damage!";
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
        attackButton.interactable = true;
        itemButton.interactable = true;
        fleeButton.interactable = true;
        dialogueText.text = "It is now the player's turn.";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return; 
        }
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
        StartCoroutine(PlayerAttack(1f));
    }

    public void OnAttackButtonCritical()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return; 
        }
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
        StartCoroutine(PlayerAttack(1.5f));
    }

    public void OnAttackButtonResistant()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return; 
        }
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
        StartCoroutine(PlayerAttack(0.5f));
    }
}
