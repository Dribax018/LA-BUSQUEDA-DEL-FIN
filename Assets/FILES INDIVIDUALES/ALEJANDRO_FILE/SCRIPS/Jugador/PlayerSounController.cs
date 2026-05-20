using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerSounController : MonoBehaviour
{
    public AudioSource fuente;

    public AudioClip Mov1;
    public AudioClip Mov2;



    public void Paso1()
    {
        fuente.PlayOneShot(Mov1);
    }
    public void Paso2()
    {
        fuente.PlayOneShot(Mov2);
    }
}
