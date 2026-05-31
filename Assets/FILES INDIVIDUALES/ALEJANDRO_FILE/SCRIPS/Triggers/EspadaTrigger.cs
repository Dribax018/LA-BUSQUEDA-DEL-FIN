using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EspadaTrigger : MonoBehaviour
{
    public GameObject Pausaparaleer;
    public bool Pausado = false;


    void Start()
    {
        Pausaparaleer.SetActive(false);
    }
    public void Continuar()
    {
        Pausaparaleer.SetActive(false);
        Time.timeScale = 1;
        Pausado = false;
    }

    public void Parar()
    {
        Pausaparaleer.SetActive(true);
        Time.timeScale = 0;
        Pausado = true;
    }

}
