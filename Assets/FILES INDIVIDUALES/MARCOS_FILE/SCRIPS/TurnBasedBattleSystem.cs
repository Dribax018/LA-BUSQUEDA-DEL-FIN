using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

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

    [Header("Cambio de escena")]
    [SerializeField] private string victoria;
    [SerializeField] private string derrota;

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

        if (state != BattleState.PlayerTurn || !playerMonster.CanUseMove(moveIndex))

        {

            return;

        }

        StartCoroutine(ResolveRound(moveIndex));

    }

    private IEnumerator ResolveRound(int playerMoveIndex)

    {

        state = BattleState.Busy;

        SetMoveButtonsInteractable(false);

        BattleMove playerMove = playerMonster.GetMove(playerMoveIndex);

        BattleMove enemyMove = GetEnemyMove();

        bool playerActsFirst = enemyMove == null || playerMonster.Speed >= enemyMonster.Speed;

        if (playerActsFirst)

        {

            yield return ExecuteAction(playerMonster, enemyMonster, playerMove, enemyHUD);

            if (battleEndedThisAction)

            {

                yield break;

            }

            if (enemyMove != null)

            {

                yield return ExecuteAction(enemyMonster, playerMonster, enemyMove, playerHUD);

                if (battleEndedThisAction)

                {

                    yield break;

                }

            }

        }

        else

        {

            yield return ExecuteAction(enemyMonster, playerMonster, enemyMove, playerHUD);

            if (battleEndedThisAction)

            {

                yield break;

            }

            yield return ExecuteAction(playerMonster, enemyMonster, playerMove, enemyHUD);

            if (battleEndedThisAction)

            {

                yield break;

            }

        }

        StartPlayerTurn();

    }

    private IEnumerator ResolveEnemyOnlyTurn()

    {

        state = BattleState.Busy;

        SetMoveButtonsInteractable(false);

        BattleMove enemyMove = GetEnemyMove();

        if (enemyMove == null)

        {

            state = BattleState.BattleOver;

            SetMessage("Ningun monstruo tiene ataques con usos restantes.");

            yield break;

        }

        SetMessage($"{playerMonster.MonsterName} no tiene ataques con usos restantes.");

        yield return new WaitForSeconds(actionDelay);

        yield return ExecuteAction(enemyMonster, playerMonster, enemyMove, playerHUD);

        if (battleEndedThisAction)

        {

            yield break;

        }

        StartPlayerTurn();

    }

    private IEnumerator ExecuteAction(BattleMonster attacker, BattleMonster defender, BattleMove move, BattleHUD defenderHUD)

    {

        battleEndedThisAction = false;

        if (attacker == null || defender == null || move == null)

        {

            yield break;

        }

        if (!move.TryConsumeUse())

        {

            SetMessage($"{attacker.MonsterName} ya no puede usar {move.MoveName}.");

            yield return new WaitForSeconds(actionDelay);

            yield break;

        }

        if (attacker == playerMonster)

        {

            RefreshMoveButtons();

            SetMoveButtonsInteractable(false);

        }

        SetMessage($"{attacker.MonsterName} usa {move.MoveName}.");

        yield return new WaitForSeconds(actionDelay);

        if (!attacker.TryHit(move))

        {

            SetMessage($"{attacker.MonsterName} fallo el ataque.");

            yield return new WaitForSeconds(actionDelay);

            yield break;

        }

        int damage = attacker.CalculateDamage(move, defender);

        bool targetFainted = defender.ReceiveDamage(damage);

        if (defenderHUD != null)

        {

            defenderHUD.Refresh();

        }

        SetMessage($"{defender.MonsterName} recibe {damage} de danio.");

        yield return new WaitForSeconds(actionDelay);

        if (targetFainted)

        {

            SetMessage($"{defender.MonsterName} ha sido derrotado.");

            state = BattleState.BattleOver;

            battleEndedThisAction = true;

            yield return new WaitForSeconds(actionDelay);

            if (attacker == playerMonster)
            {
                SetMessage("Has ganado el combate.");
                yield return new WaitForSeconds(actionDelay);
                SceneManager.LoadScene(victoria);
            }
            else
            {
                SetMessage("Has perdido el combate.");
                yield return new WaitForSeconds(actionDelay);
                SceneManager.LoadScene(derrota);
            }

        }

    }

    private BattleMove GetEnemyMove()

    {

        if (enemyMonster == null || !enemyMonster.HasAnyUsableMove())

        {

            return null;

        }

        List<BattleMove> availableMoves = new List<BattleMove>();

        for (int i = 0; i < enemyMonster.Moves.Count; i++)

        {

            BattleMove move = enemyMonster.GetMove(i);

            if (move != null && move.HasUsesLeft)

            {

                availableMoves.Add(move);

            }

        }

        if (availableMoves.Count == 0)

        {

            return null;

        }

        int randomIndex = Random.Range(0, availableMoves.Count);

        return availableMoves[randomIndex];

    }

    private void StartPlayerTurn()

    {

        RefreshMoveButtons();

        if (playerMonster == null)

        {

            state = BattleState.BattleOver;

            SetMessage("No hay monstruo del jugador asignado.");

            SetMoveButtonsInteractable(false);

            return;

        }

        if (!playerMonster.HasAnyUsableMove())

        {

            if (enemyMonster == null || !enemyMonster.HasAnyUsableMove())

            {

                state = BattleState.BattleOver;

                SetMessage("Ningun monstruo tiene ataques con usos restantes.");

                SetMoveButtonsInteractable(false);

                return;

            }

            StartCoroutine(ResolveEnemyOnlyTurn());

            return;

        }

        state = BattleState.PlayerTurn;

        SetMessage("Elige un ataque.");

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

            if (moveButtons[i] == null)

            {

                continue;

            }

            bool hasMove = playerMonster != null && playerMonster.HasMove(i);

            moveButtons[i].gameObject.SetActive(hasMove);

            if (!hasMove)

            {

                continue;

            }

            BattleMove move = playerMonster.GetMove(i);

            Text buttonText = moveButtons[i].GetComponentInChildren<Text>();

            if (buttonText != null && move != null)

            {

                buttonText.text = $"{move.MoveName} ({move.RemainingUses}/{move.MaxUses})";

            }

            moveButtons[i].interactable = state == BattleState.PlayerTurn && playerMonster.CanUseMove(i);

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

            if (moveButtons[i] == null || !moveButtons[i].gameObject.activeSelf)

            {

                continue;

            }

            bool canUseMove = playerMonster != null && playerMonster.CanUseMove(i);

            moveButtons[i].interactable = isInteractable && canUseMove;

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