using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int max_HP, ATK, MATK, DEF, MDEF, SPD, ACC, AGI, LCK, DATE_HP, TYPE, LEVEL, exp, CAD, cur_HP, stability, damage;

    //I think the new is needed allocate memory.
    public LinkedList<(int hardness, int attack)> appendage = new LinkedList<(int, int)>();
    public string name;
    
    private IAttackBehaviour attackBehaviour;

    void Awake()
    {
        attackBehaviour = GetComponent<IAttackBehaviour>();
    }

    public int moveset()
    {
        return attackBehaviour.DoRandomAttack();
    }


    public bool TakeDamage(int dmg)
    {
        cur_HP-=dmg;

        if (cur_HP <= 0)
        {
            return true;
        }   
        return false;
    }
}
