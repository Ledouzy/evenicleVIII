using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class Battle_System : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject archfriend;
    public GameObject bossPrefab;
    public static bool Boss = false;
    public Collider2D HunterCollider;


//MAYBE DELETE TRANSFORM??? NOT SURE
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Button attackButton;
    public Button itemButton;
    public Button fleeButton;

    public Button attackButton1;
    public Button attackButton2;
    public Button attackButton3;

    public int attackButton1_hp = 1;
    public int attackButton2_hp = 2;
    public int attackButton3_hp = 3;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText; 
    //public TMPText dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public AudioManager audioManager;

    void Start()
    {
        
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
        state = BattleState.START;
        //Wrap coroutine for time delay
        attackButton1.onClick.AddListener(Appendage1_dmg);
        attackButton2.onClick.AddListener(Appendage2_dmg);
        attackButton3.onClick.AddListener(Appendage3_dmg);
        StartCoroutine(SetupBattle());
        
    }

    void Appendage1_dmg()
    {
        attackButton1_hp -= 1;
    }
    void Appendage2_dmg()
    {
        attackButton2_hp -= 1;
    }  
    void Appendage3_dmg()
    {
        attackButton3_hp -= 1;
    }
    
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        //NAME PLAYER UNDER PLAYER COMPONENTS
        playerUnit = playerGO.GetComponent<Unit>(); 

        GameObject enemyGO;

        // select enemy
        enemyGO = Instantiate(archfriend, enemyBattleStation);
        /* if (Boss == true)
        {
            enemyGO = Instantiate(bossPrefab, enemyBattleStation);
            Boss = false;
        } else if (Random.Range(0, 2) == 0){
            enemyGO = Instantiate(enemy1Prefab, enemyBattleStation);
        } else
        {
            enemyGO = Instantiate(enemy2Prefab, enemyBattleStation);
        }
        */
        //NAME ENEMY UNDER ENEMY COMPONENTS
        enemyUnit = enemyGO.GetComponent<Unit>(); 

        
        Debug.Log("test:"+enemyUnit.name);
        dialogueText.text = "Enemy appeared! " + enemyUnit.name + " approaches!";
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
        void OnCollisionEnter2D(Collision2D other)
        {
        Boss = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("AttackScene");


        }

    IEnumerator PlayerAttack(float multiplier)
    {
        if (attackButton1_hp <= 0)
        {
            attackButton1.interactable = false;
        }
        if (attackButton2_hp <= 0)
        {
            attackButton2.interactable = false;
        } 
        if (attackButton3_hp <= 0)
        {
            attackButton3.interactable = false;
        }

        bool isDead = enemyUnit.TakeDamage((int)(playerUnit.damage*multiplier));

        //change hp from damage
        enemyHUD.SetHP(enemyUnit.cur_HP);
        if (multiplier > 1)
        {
            dialogueText.text = "Critical Hit! " + enemyUnit.name + " took " + playerUnit.damage*multiplier + " damage!";
        } else if (multiplier < 1)
        {
            dialogueText.text = enemyUnit.name + " blocked the attack! " + enemyUnit.name + " took " + playerUnit.damage*multiplier + " damage!";
        } else {
            dialogueText.text = enemyUnit.name + " took " + playerUnit.damage*multiplier + " damage!";
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            dialogueText.text = enemyUnit.name + " died. You won!";
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
        // exit battle here
        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator EnemyTurn()
    {
        attackButton.interactable = false;
        itemButton.interactable = false;
        fleeButton.interactable = false;
        if (attackButton1_hp != 0)
        {
            Debug.Log("The Archfriend looks playful!");
        }
        else if (attackButton2_hp != 0)
        {
            Debug.Log("The Archfriend looks angry!");
        }
        else if (attackButton3_hp != 0)
        {
            Debug.Log("The Archfriend looks sad.");
        }
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.cur_HP);

        audioManager.PlaySFX(audioManager.playerDealDmg);

        dialogueText.text = "It is now the enemy's turn. " + playerUnit.name + " took " + enemyUnit.damage + " damage!";
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
