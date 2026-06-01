using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AnimationClip animationFinal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator AnimacionTransicion()
    {
        animator.SetTrigger("Transición");
        yield return new WaitForSeconds(animationFinal.length);
    }

    public void Cambiarescena(string nombre)
    {
        Debug.Log("Boton pulsado. Cargando escena: " + nombre);
        StartCoroutine(AnimacionTransicion());
        SceneManager.LoadScene(nombre);
    }
    public void salir()
    {
        Debug.Log("cuando el juego esté exportado");
        Application.Quit();
    }
}
