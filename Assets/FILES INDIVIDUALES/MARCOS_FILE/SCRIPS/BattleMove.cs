using System;

using UnityEngine;

[Serializable]

public class BattleMove

{

    [SerializeField] private string moveName = "Placaje";

    [SerializeField] private int power = 12;

    [SerializeField][Range(1, 100)] private int accuracy = 95;

    [SerializeField][Min(1)] private int maxUses = 10;

    private int remainingUses;

    public string MoveName => moveName;

    public int Power => power;

    public int Accuracy => accuracy;

    public int MaxUses => maxUses;

    public int RemainingUses => remainingUses;

    public bool HasUsesLeft => remainingUses > 0;

    public void ResetUses()

    {

        remainingUses = Mathf.Max(0, maxUses);

    }

    public bool TryConsumeUse()

    {

        if (!HasUsesLeft)

        {

            return false;

        }

        remainingUses--;

        return true;

    }

}