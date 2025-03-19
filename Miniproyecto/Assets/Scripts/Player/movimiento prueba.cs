using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoprueba : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float runMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool isRunning;

    public bool mirandoDerecha = true;
    public LayerMask queEsSuelo;
    public Transform controladorDeSuelo;
    public Vector3 caja;
    public bool enSuelo = true;

    Vector2 startposition;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startposition = transform.position;
    }

    void Update()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        if (Input.GetKey(KeyCode.JoystickButton8)) // Derecha
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.JoystickButton9)) // Izquierda
        {
            horizontalInput = -1f;
        }


        rb.velocity = new Vector2(horizontalInput * speed * (isRunning ? runMultiplier : 1), rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (Input.GetKey(KeyCode.O))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.JoystickButton1) && isGrounded))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
        /*direccion = .Cangrejo.Movimiento.ReadValue<Vector2>();
        enSuelo = Physics2D.OverlapBox(controladorDeSuelo.position, caja, 0f, queEsSuelo);*/

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        animator.SetBool("isGrounded", true);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        animator.SetBool("isGrounded", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorDeSuelo.position, caja);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vacio")) // Asegúrate de que tu jugador tenga la etiqueta "Player"
        {
            Destroy(gameObject); // Elimina al jugador
        }
    }*/

    /*private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OntriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vacio"))
        {
            Die();
        }  
    }
    void Die()  
    {
        StartCoroutine(Respawn(0.5f));  
    }
    IEnumerator Respawn(float duration)
    {
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = startposition;
        transform.localScale = new Vector3(1, 1, 1);    

    }*/
    private void AjustraRotacion(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            Girar();

        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            Girar();
        }

    }
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector2 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private void Saltar()
    {
        if (enSuelo)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

    }
}
