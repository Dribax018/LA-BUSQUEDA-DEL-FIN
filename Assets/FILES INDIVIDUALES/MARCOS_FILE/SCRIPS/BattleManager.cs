using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleMonster Caballero;
    [SerializeField] private BattleMonster enemigo;

    void Start()
    {
        Debug.Log("Vida jugador: " + Caballero.CurrentHP);
        Debug.Log("Vida enemigo: " + enemigo.CurrentHP);
       
    }

}