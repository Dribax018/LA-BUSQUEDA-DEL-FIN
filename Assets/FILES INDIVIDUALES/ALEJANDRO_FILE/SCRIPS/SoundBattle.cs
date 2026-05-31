using UnityEngine;

public class SoundBattle : MonoBehaviour
{
    public AudioSource fuente;

    public AudioClip Muerte;
    public AudioClip cuchillazo;
    public AudioClip golpedivino;
    public AudioClip flechazo;
    public AudioClip punetazo;



    public void Cuchillazo()
    {
        fuente.PlayOneShot(cuchillazo);
    }
    public void GolpeDivino()
    {
        fuente.PlayOneShot(golpedivino);
    }

    public void Flechazo()
    {
        fuente.PlayOneShot(flechazo);

    }
    public void Punetazo()
    {
        fuente.PlayOneShot(punetazo);

    }
}
