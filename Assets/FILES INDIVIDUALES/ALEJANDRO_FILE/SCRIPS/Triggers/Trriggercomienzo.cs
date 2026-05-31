using UnityEngine;

public class Trriggercomienzo : MonoBehaviour
{
    public GameObject Pausacomienzo;
    public bool Pausado = false;


    public void Empezar()
    {
        Pausacomienzo.SetActive(false);
        Time.timeScale = 1;
        Pausado = false;
    }

}
