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
    public int vidas = 3; // Valor inicial por defecto

    private Rigidbody2D rb;
    private bool isGrounded;
    private TextMeshProUGUI vidaText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Cargar vidas guardadas
        if (PlayerPrefs.HasKey("Vidas"))
        {
            vidas = PlayerPrefs.GetInt("Vidas");
        }
        else
        {
            PlayerPrefs.SetInt("Vidas", vidas);
        }

        // Buscar el texto en el Canvas
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            vidas--;
            PlayerPrefs.SetInt("Vidas", vidas); // Guardar vidas actualizadas
            ActualizarVidaText();

            if (vidas <= 0)
            {
                ReiniciarJuego();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recargar escena sin reiniciar vidas
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
        PlayerPrefs.DeleteKey("Vidas"); // Resetear vidas
        SceneManager.LoadScene("Menu"); // Reiniciar el juego desde la primera escena
    }
}