using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class  Vidas : MonoBehaviour
{
    public int currentLives = 3; // Comienza con 3 vidas, pero puede aumentar sin límite

    public TMP_Text livesText; // Referencia al texto del UI

    void Start()
    {
        UpdateLivesUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigos")) // Si toca un enemigo
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            RestartGame();
        }
    }

    public void AddLife() // Función para agregar vidas
    {
        currentLives++;
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        livesText.text = "Vidas: " + currentLives;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}