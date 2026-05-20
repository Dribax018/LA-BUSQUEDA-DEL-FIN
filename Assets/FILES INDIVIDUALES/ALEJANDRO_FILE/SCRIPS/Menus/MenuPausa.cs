using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPausa : MonoBehaviour
{
    public GameObject MenuPause;
    public bool Pausado = false;

    void Start()
    {
        MenuPause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        MenuPause.SetActive(false);
        Time.timeScale = 1;
        Pausado = false;
    }

    public void Pausar()
    {
        MenuPause.SetActive(true);
        Time.timeScale = 0;
        Pausado = true;
    }
}