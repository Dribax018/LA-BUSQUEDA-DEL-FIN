using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TPareas : MonoBehaviour
{
    // Posici n a la que se teletransportar el jugador
    public Vector2 teleportPosition;
    public Vector3 teleportPosicion;
    // Referencia al jugador (arrastrar desde el Inspector)
    public GameObject Player;
    // Detecta cuando algo entra en el collider (aseg rate que el collider este como ISTRIGGER
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            // Teletransporta al jugador a la posicion deseada
            //if ()
            {
                Player.transform.position = teleportPosition;
            }
            //else 
            //{
            //    Player.transform.position = teleportPosicion;
            //}
        }
    }
}
