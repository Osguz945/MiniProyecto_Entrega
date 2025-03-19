using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoCaidaVacio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vacio")) // Asegúrate de que tu jugador tenga la etiqueta "Player"
        {
            Destroy(gameObject); // Elimina al jugador
        }
    }
}