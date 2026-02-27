using UnityEngine;
using UnityEngine.UI;

using TMPro; 
 
public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.name;
        levelText.text = ""+unit.LEVEL;
        hpSlider.maxValue = unit.max_HP;
        hpSlider.value = unit.cur_HP;


    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
