using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject canvasPausa; // Arrastra aquí el Canvas de pausa
    public AudioSource sonidoPausa; // Arrastra aquí el sonido de pausa

    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Return))
        {
            TogglePausa();
        }
    }

    void TogglePausa()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            Time.timeScale = 0; // Pausa el juego
            canvasPausa.SetActive(true); // Muestra el canvas
            sonidoPausa.Play(); // Reproduce el sonido
        }
        else
        {
            Time.timeScale = 1; // Reanuda el juego
            canvasPausa.SetActive(false); // Oculta el canvas
        }
    }
}
