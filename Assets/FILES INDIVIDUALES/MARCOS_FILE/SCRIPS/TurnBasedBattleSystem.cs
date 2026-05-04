using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TurnBasedBattleSystem : MonoBehaviour
{
    private enum BattleState
    {
        Setup,
        PlayerTurn,
        Busy,
        BattleOver
    }
    [Header("Combatants")]
    [SerializeField] private BattleMonster playerMonster;
    [SerializeField] private BattleMonster enemyMonster;
    [Header("UI")]
    [SerializeField] private BattleHUD playerHUD;
    [SerializeField] private BattleHUD enemyHUD;
    [SerializeField] private Text messageText;
    [SerializeField] private Button[] moveButtons;
    [Header("Timing")]
    [SerializeField] private float actionDelay = 1.1f;
    private BattleState state = BattleState.Setup;
    private bool battleEndedThisAction;
    private void Start()
    {
        SetupBattle();
    }
    private void SetupBattle()
    {
        if (playerMonster == null || enemyMonster == null)
        {
            SetMessage("Asigna el monstruo del jugador y del enemigo.");
           
            state = BattleState.BattleOver;
            SetMoveButtonsInteractable(false);
            return;
        }
        playerMonster.ResetForBattle();
        enemyMonster.ResetForBattle();
        if (playerHUD != null)
        {
            playerHUD.Bind(playerMonster);
        }
        if (enemyHUD != null)
        {
            enemyHUD.Bind(enemyMonster);
        }
        RefreshMoveButtons();
        StartPlayerTurn();
    }
    public void OnMoveButtonPressed(int moveIndex)
    {
        if (state != BattleState.PlayerTurn ||
       !playerMonster.CanUseMove(moveIndex))
        {
            return;
        }
        StartCoroutine(ResolveRound(moveIndex));
    }
    private IEnumerator ResolveRound(int playerMoveIndex)
    {
        state = BattleState.Busy;
        SetMoveButtonsInteractable(false);
        BattleMove playerMove =
       playerMonster.GetMove(playerMoveIndex);
        BattleMove enemyMove = GetEnemyMove();
        bool playerActsFirst = enemyMove == null ||
       playerMonster.Speed >= enemyMonster.Speed;
        if (playerActsFirst)
        {
            yield return ExecuteAction(playerMonster, enemyMonster,
           playerMove, enemyHUD);
            if (battleEndedThisAction)
            {
                yield break;
            }
            if (enemyMove != null)
            {
                yield return ExecuteAction(enemyMonster,
               playerMonster, enemyMove, playerHUD);
                if (battleEndedThisAction)
                {
                    yield break;
                }
            }
        }
        else
        {
            yield return ExecuteAction(enemyMonster, playerMonster,
           enemyMove, playerHUD);
            if (battleEndedThisAction)
            {
                yield break;
            }
            yield return ExecuteAction(playerMonster, enemyMonster,
           playerMove, enemyHUD);
            if (battleEndedThisAction)
            {
                yield break;
            }
        }
        StartPlayerTurn();
    }
    private IEnumerator ExecuteAction(BattleMonster attacker,
   BattleMonster defender, BattleMove move, BattleHUD defenderHUD)
    {
        battleEndedThisAction = false;
        if (attacker == null || defender == null || move == null)
        {
            yield break;
        }
        SetMessage($"{attacker.MonsterName} usa {move.MoveName}.");
        yield return new WaitForSeconds(actionDelay);
        if (!attacker.TryHit(move))
        {
            SetMessage($"{attacker.MonsterName} falló el ataque.");
            yield return new WaitForSeconds(actionDelay);
            yield break;
        }
        int damage = attacker.CalculateDamage(move, defender);
        bool targetFainted = defender.ReceiveDamage(damage);
        if (defenderHUD != null)
        {
            defenderHUD.Refresh();
        }
        SetMessage($"{defender.MonsterName} recibe {damage} de daño.");
       
        yield return new WaitForSeconds(actionDelay);
        if (targetFainted)
        {
            SetMessage($"{defender.MonsterName} ha sido derrotado.");
           
            state = BattleState.BattleOver;
            battleEndedThisAction = true;
            yield return new WaitForSeconds(actionDelay);
            SetMessage(attacker == playerMonster ? "Has ganado el combate." : "Has perdido el combate.");
           
            yield break;
        }
    }
    private BattleMove GetEnemyMove()
    {
        if (enemyMonster == null || enemyMonster.Moves.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, enemyMonster.Moves.Count);
        return enemyMonster.GetMove(randomIndex);
    }
    private void StartPlayerTurn()
    {
        state = BattleState.PlayerTurn;
        SetMessage("Elige un ataque.");
        RefreshMoveButtons();
        SetMoveButtonsInteractable(true);
    }
    private void RefreshMoveButtons()
    {
        if (moveButtons == null)
        {
            return;
        }
        for (int i = 0; i < moveButtons.Length; i++)
        {
            bool hasMove = playerMonster != null &&
           playerMonster.CanUseMove(i);
            moveButtons[i].gameObject.SetActive(hasMove);
            if (!hasMove)
            {
                continue;
            }
            Text buttonText =
           moveButtons[i].GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = playerMonster.GetMove(i).MoveName;
            }
        }
    }
    private void SetMoveButtonsInteractable(bool isInteractable)
    {
        if (moveButtons == null)
        {
            return;
        }
        for (int i = 0; i < moveButtons.Length; i++)
        {
            if (moveButtons[i].gameObject.activeSelf)
            {
                moveButtons[i].interactable = isInteractable;
            }
        }
    }
    private void SetMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }
}