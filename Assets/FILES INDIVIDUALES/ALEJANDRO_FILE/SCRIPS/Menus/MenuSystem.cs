using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class NewMonoBehaviourScript : MonoBehaviour
{
    public void Cambiarescena(string nombre)
    {
        Debug.Log("Boton pulsado. Cargando escena: " + nombre);
        SceneManager.LoadScene(nombre);
    }
    public void salir()
    {
        Debug.Log("cuando el juego esté exportado");
        Application.Quit();
    }
}
