using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class Battle_System : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerBattleStation;


    public GameObject enemyPrefab;
    public Transform enemyBattleStation;

    void Start()
    {
        
    }

}
