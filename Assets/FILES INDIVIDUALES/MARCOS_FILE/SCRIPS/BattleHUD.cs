using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text hpText;
    [SerializeField] private Slider hpSlider;
    private BattleMonster boundMonster;
    public void Bind(BattleMonster monster)
    {
        boundMonster = monster;
        Refresh();
    }
    public void Refresh()
    {
        if (boundMonster == null)
        {
            return;
        }
        if (nameText != null)
        {
            nameText.text = boundMonster.MonsterName;
        }
        if (hpText != null)
        {
            hpText.text =
           $"{boundMonster.CurrentHP}/{boundMonster.MaxHP} HP";
        }
        if (hpSlider != null)
        {
            hpSlider.maxValue = boundMonster.MaxHP;
            hpSlider.value = boundMonster.CurrentHP;
        }
    }
}