using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alga : MonoBehaviour
{
    private bool enContactoConCangrejo = false; // Para verificar si está en contacto con Crab
    private GameObject cangrejo; // Guarda la referencia al objeto Crab

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crab"))
        {
            enContactoConCangrejo = true;
            cangrejo = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crab"))
        {
            enContactoConCangrejo = false;
            cangrejo = null;
        }
    }

    void Update()
    {
        if (enContactoConCangrejo && Input.GetKeyDown(KeyCode.P))
        {
            Destroy(gameObject); // Destruye el objeto Alga
        }
    }
}
