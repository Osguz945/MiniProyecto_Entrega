using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // Direcci�n del movimiento
    public float speed = 5f; // Velocidad del objeto
    public string[] collisionTags = { "Ground", "Player" }; // Tags que activan la animaci�n y reinicio
    public Animator animator; // Referencia al Animator
    private Vector3 spawnPoint; // Posici�n inicial

    void Start()
    {
        spawnPoint = transform.position; // Guarda la posici�n de spawn
        if (animator != null)
        {
            animator.SetBool("IsMoving", true); // Activar animaci�n de movimiento
        }
    }

    void Update()
    {
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in collisionTags)
        {
            if (other.CompareTag(tag))
            {
                StartCoroutine(Respawn());
                break;
            }
        }
    }

    IEnumerator Respawn()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false); // Detener animaci�n de movimiento
            animator.SetTrigger("Collision"); // Disparar la animaci�n de colisi�n
            transform.position = spawnPoint; // Volver al punto de spawn
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Esperar a que termine
            animator.SetBool("IsMoving", true); // Reactivar animaci�n de movimiento
        }
        
    }
}


