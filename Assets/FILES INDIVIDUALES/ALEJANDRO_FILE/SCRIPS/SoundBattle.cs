using UnityEngine;

public class SoundBattle : MonoBehaviour
{
    public AudioSource fuente;

    public AudioClip Muerte;
    public AudioClip cuchillazo;
    public AudioClip golpedivino;



    public void Cuchillazo()
    {
        fuente.PlayOneShot(cuchillazo);
    }
    public void GolpeDivino()
    {
        fuente.PlayOneShot(golpedivino);
    }
}
