using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 7f;
    public int vidas = 3;
    public bool Corte = false;

    private Rigidbody2D rb;
    public Animator animator;
    private TextMeshProUGUI vidaText;

    public LayerMask queEsSuelo;
    public Transform controladorSuelo;
    public Vector3 dimensionesCaja;
    public bool enSuelo;
    private bool mirandoDerecha = true;
    private GameObject algaDetectada;

    public AudioClip jumpClip;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


        if (PlayerPrefs.HasKey("Vidas"))
        {
            vidas = PlayerPrefs.GetInt("Vidas");
        }
        else
        {
            PlayerPrefs.SetInt("Vidas", vidas);
        }

        GameObject vidaObj = GameObject.Find("VidaText");
        if (vidaObj != null)
        {
            vidaText = vidaObj.GetComponent<TextMeshProUGUI>();
            ActualizarVidaText();
        }
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        float moveSpeed = Input.GetKey(KeyCode.O) ? sprintSpeed : speed;
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpClip);
        }

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        // Control de animaciones

        if (Input.GetKey(KeyCode.P) && (animator.GetBool("Corte") == false) && (move != 0) && (Input.GetKey(KeyCode.O)))
        {
            animator.SetBool("Corte", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.P) && (animator.GetBool("Corte") == false) && (move != 0))
        {
            animator.SetBool("Corte", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
        else if (Input.GetKey(KeyCode.P)&& (animator.GetBool("Corte") == false)&& (move == 0))
        {
            animator.SetBool("Corte", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        else if (Input.GetKey(KeyCode.O)&&(move != 0))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            animator.SetBool("Corte", false);
        }
        else if (move != 0)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
            animator.SetBool("Corte", false);
        }
        else 
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Corte", false);
        }

        // Girar el personaje según la dirección del movimiento
        if (move > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (move < 0 && mirandoDerecha)
        {
            Girar();
        }
    }

    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            vidas--;
            PlayerPrefs.SetInt("Vidas", vidas);
            ActualizarVidaText();

            if (vidas <= 0)
            {
                ReiniciarJuego();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void ActualizarVidaText()
    {
        if (vidaText != null)
        {
            vidaText.text = "Vidas: " + vidas;
        }
    }

    void ReiniciarJuego()
    {
        PlayerPrefs.DeleteKey("Vidas");
        SceneManager.LoadScene("GameOver");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
    public void EndAttack()
    {
        animator.SetBool("Corte", false);
    }
    
    

}
