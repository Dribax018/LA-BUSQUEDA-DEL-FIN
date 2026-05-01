using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Tp : MonoBehaviour
{
    // Posici n a la que se teletransportar el jugador
    public Vector2 teleportPosition;
    // Referencia al jugador (arrastrar desde el Inspector)
    public GameObject Player;
    // Detecta cuando algo entra en el collider (aseg rate que el collider este como ISTRIGGER
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Teletransporta al jugador a la posicion deseada

            Player.transform.position = teleportPosition;
        }
    }

}
