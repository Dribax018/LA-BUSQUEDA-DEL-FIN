using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;


    // Con estas lineas de c�digo asigno el Rigibody del player autom�ticamente al script al ejecutar el c�digo, de esta forma no es necesario hacerlo en Unity de forma manual
    //--------------------------------------------
    //public void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}
    //--------------------------------------------



    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator= rb.GetComponent<Animator>();
    }
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); //Esto ya viene asignado a Unity a las teclas del eje horizontal (A, D, <-, ->)
        movementInput.y = Input.GetAxisRaw("Vertical"); //Esto ya viene asignado a Unity a las teclas del eje vertical (W, S, ^,v)

        movementInput = movementInput.normalized;

        //animator.SetFloat("Horizontal", movementInput.x);
        //animator.SetFloat("Vertical", movementInput.y);
        //animator.SetFloat("Speed", movementInput.magnitude);

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * velocidad;
    }

}
