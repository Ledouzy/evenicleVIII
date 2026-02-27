using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class EnemyStatsData : MonoBehaviour
{
public int HP;
 
 public int ATK;
 
 public int MATK;
 
 public int DEF;
 
 public int MDEF;
 
 public int SPD;
 
 public int ACC;
 
 public int AGI;
 
 public int LCK;
 
 public int date_hp;

 public int stability;

//I think the new is needed allocate memory.
public LinkedList<(int hardness, int attack)> appendage = new LinkedList<(int, int)>();

public string name;

public int type;

public int level;

public int exp;

public int CAD;

public int moveset;
}
