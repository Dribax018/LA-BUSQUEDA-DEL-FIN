using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Condicion : MonoBehaviour
{

    public int SegundaMitadNec = 1; //Cuando creemos la entidad espada, esto hay que cambiarlo
    public int SegundaMitad = 0;

    public void ComprobarMitad()
    {
        if (SegundaMitad== SegundaMitadNec)
        {
            Avanzar();
        }
    }

    void Avanzar()
    {
        Debug.Log("Se comprueba");
    }
}
