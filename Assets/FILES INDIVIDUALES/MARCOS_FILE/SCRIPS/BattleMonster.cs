using System.Collections.Generic;
using UnityEngine;
public class BattleMonster : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private string monsterName = "Monstruo";
    [Header("Stats")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int attack = 18;
    [SerializeField] private int defense = 10;
    [SerializeField] private int speed = 12;
    [Header("Moves")]
    [SerializeField]
    private List<BattleMove> moves = new
   List<BattleMove>();
    public string MonsterName => monsterName;
    public int MaxHP => maxHP;
    public int CurrentHP { get; private set; }
    public int Attack => attack;
    public int Defense => defense;
    public int Speed => speed;
    public IReadOnlyList<BattleMove> Moves => moves;
    private void Awake()
    {
        ResetForBattle();
    }
    public void ResetForBattle()
    {
        CurrentHP = maxHP;

        foreach (BattleMove move in moves)
        {
            move.ResetUsos();
        }
    }
    public bool CanUseMove(int moveIndex)
    {
        return moveIndex >= 0 && moveIndex < moves.Count;
    }
    public BattleMove GetMove(int moveIndex)
    {
        return CanUseMove(moveIndex) ? moves[moveIndex] : null;
    }
    public int CalculateDamage(BattleMove move, BattleMonster
   target)
    {
        if (move == null || target == null)
        {
            return 0;
        }
        int rawDamage = attack + move.Power - target.Defense;
        return Mathf.Max(1, rawDamage);
    }
    public bool TryHit(BattleMove move)
    {
        if (move == null)
        {
            return false;
        }
        return Random.Range(1, 101) <= move.Accuracy;
    }
    public bool ReceiveDamage(int damage)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - Mathf.Max(0, damage));
        return CurrentHP <= 0;
    }
    public bool UseMove(BattleMove move)
    {
        if (move == null)
        {
            return false;
        }

        return move.Use();
    }
  
}