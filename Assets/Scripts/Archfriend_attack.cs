using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Archfriend_attack : MonoBehaviour, IAttackBehaviour
{
    
    public Unit enemyData;

    // generalized attack calculation
    private int ComputeAttack(float multiplier)
    {
        float baseDamage = enemyData.ATK * multiplier;
        int stabilityModifier = UnityEngine.Random.Range(-enemyData.stability, enemyData.stability + 1);
        return Mathf.RoundToInt(baseDamage + baseDamage * stabilityModifier / 100f);
    }

    public int weakAttack() {
        Debug.Log("Performing weak attack");
        return ComputeAttack(0.8f);
    }

    public int normalAttack() {
        Debug.Log("Performing normal attack");
        return ComputeAttack(1.0f);
    }

    public int strongAttack() {
        Debug.Log("Performing strong attack");
        return ComputeAttack(1.2f);
    
    }

    public int DoRandomAttack()
    {
    int attack_choice = UnityEngine.Random.Range(1, 4);

    switch (attack_choice)
        {
            case 1:
                return weakAttack();
            case 2:
                return normalAttack();
            case 3:
                return strongAttack();
            default:
                return normalAttack();
        }
    }
}
