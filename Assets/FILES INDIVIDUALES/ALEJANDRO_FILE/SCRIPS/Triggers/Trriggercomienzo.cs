using UnityEngine;

public class Trriggercomienzo : MonoBehaviour
{
    public MovimientoJugador movimientoJugador;

    public GameObject Pausacomienzo;
    public bool Pausado = false;


    public void Empezar()
    {
        Debug.Log("CLICK EN COMENZAR");

        

        Pausacomienzo.SetActive(false);
        Time.timeScale = 1;
        Pausado = false;
        movimientoJugador.ActivarMovimiento();
    }

}
