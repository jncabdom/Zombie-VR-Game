using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Se encarga de iniciar el juego o de cerrarlo
public class MainMenu : MonoBehaviour
{
    // Inicia el juego
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Cierra la aplicación
    public void QuitGame() {
        Application.Quit();
    }
}
