using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CambiarEscenaTrigger : MonoBehaviour
{
    [Header("Cambio de escena")]
    [SerializeField] private string pelea;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(pelea);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            SceneManager.LoadScene(pelea);
        }
    }
}
