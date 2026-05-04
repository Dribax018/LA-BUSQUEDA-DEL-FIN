using System;
using UnityEngine;
[Serializable]
public class BattleMove
{
    [SerializeField] private string moveName = "Placaje";
    [SerializeField] private int power = 12;
    [SerializeField][Range(1, 100)] private int accuracy = 95;
    [SerializeField] public int maxUsos = 10;
    private int usosActuales;

    public string MoveName => moveName;
    public int Power => power;
    public int Accuracy => accuracy;
    public int MaxUsos => maxUsos;
    public int UsosActuales => usosActuales;

    public void ResetUsos()
    {
        usosActuales = maxUsos;
    }

    public bool Use()
    {
        if (usosActuales <= 0)
        {
            Debug.Log(moveName + " no tiene mas usos");
            return false;
        }

        usosActuales--;
        return true;
    }
    
}

