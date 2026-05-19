using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovimientoJugador : MonoBehaviour
{
    public PlayerSounController playerSounController;
    bool step = false;
    
    public float timeByStep = 0.5f;
    float cont = 0;

    public float velocidad = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public Animator animator;
    public SpriteRenderer spriteRenderer;


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
        animator= rb.GetComponent<Animator>();
    }
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime; //Esto ya viene asignado a Unity a las teclas del eje horizontal (A, D, <-, ->)
        movementInput.y = Input.GetAxisRaw("Vertical") * Time.deltaTime; //Esto ya viene asignado a Unity a las teclas del eje vertical (W, S, ^,v)


        if (movementInput.x != 0)
        {

            cont += Time.deltaTime;
            if (cont >= timeByStep)
            {
                cont = 0f;
                if (step)
                {
                    playerSounController.Paso1();
                }
                else
                {
                    playerSounController.Paso2();
                }
                step = !step;
            }
            movementInput.y = 0;
        }




        movementInput = movementInput.normalized;

        animator.SetFloat("Movimiento D I", movementInput.x);

        if (movementInput.x > 0)
            spriteRenderer.flipX = false;
        else if (movementInput.x < 0)
            spriteRenderer.flipX = true;


        Vector3 posicion = transform.position;

        animator.SetFloat("Movimiento arriba", movementInput.y);
        

        // animator.SetFloat("Movimiento arriba", movementInput.y);
        //animator.SetFloat("Speed", movementInput.magnitude);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * velocidad * Time.fixedDeltaTime);
    }

    /* private void FixedUpdate()
     {
         rb.linearVelocity = movementInput * velocidad * Time.fixedDeltaTime;
     }*/

}
