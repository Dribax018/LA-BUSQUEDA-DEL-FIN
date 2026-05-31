using UnityEngine;

public class EspadaTrigger : MonoBehaviour
{
    public GameObject Pausaparaleer;
    public bool Pausado = false;

    void Start()
    {
        if (Pausaparaleer != null)
        {
            Pausaparaleer.SetActive(false);
        }
    }

    public void Continuar()
    {
        Debug.Log("CLICK EN CONTINUAR");
        if (Pausaparaleer != null)
        {
            Pausaparaleer.SetActive(false);
        }

        Time.timeScale = 1;
        Pausado = false;
    }

    public void Parar()
    {
        if (Pausaparaleer != null)
        {
            Pausaparaleer.SetActive(true);
        }

        Time.timeScale = 0;
        Pausado = true;
    }
}
