using System.Data;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class Battle_System : MonoBehaviour
{
    public GameObject playerPrefab;



    public GameObject enemyPrefab;


//MAYBE DELETE TRANSFORM??? NOT SURE
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(playerPrefab, playerBattleStation);
        Instantiate(enemyPrefab, enemyBattleStation);

    }
}
