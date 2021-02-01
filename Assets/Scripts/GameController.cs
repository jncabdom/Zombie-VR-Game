using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// La clase gameController se encarga de manejar el funcionamiento principal del juego, de aparecer los zombies en sus respectivos lugares, de controlar las rondas y de controlar cuando
// muere el jugador entre otras funciones
public class GameController : MonoBehaviour
{
    GameObject message;                 // Mensaje que usaremos para mostrar las rondas         
    Text messageText;           
    public GameObject[] spawners;       // Localizaciones en las que aparecerán los zombies    
    public static int round = 1;        // Contador de rondas
    public int songuisPerRound = 4;     // Número de zombies por ronda
    public int remainingSonguis;        // Los zombies que faltan por morir en la ronda
    public int maxWaitTime = 12;        // Tiempo máximo de espera para aparecer al siguiente zombie
    public int minWaitTime = 5;         // Tiempo mínimo de espera para aparecer al siguiente zombie
    bool spawn = true;                  // Comtrola si ha aparecido un zombie en la escena 

    // Obtenemos el texto "Rounds" para poder establecer las rondas en la UI
    void Start() {
        remainingSonguis = 0;
        GameObject reference = GameObject.Find("Rounds");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
    }

    // En caso de que no queden más zombies aumentamos la ronda, llamamos a playRound y establecemos el valor de los zombies por ronda de manera aleatoria
    // Si el jugador pierde toda la vida iniciamos la corutina backToMenu
    void Update()
    {
        if (remainingSonguis == 0) {
            messageText.text = round.ToString();
            remainingSonguis = songuisPerRound;
            StartCoroutine(playRound());
            songuisPerRound += (Random.Range(4, 6) * (round/2));
        }
        if(PlayerStats.health <= 0) {
            StartCoroutine(backToMenu());
        }
    }
    
    // Muestra un video indicando que el jugador a muerto y después de unos segundo lo devuelve al menú principal
    IEnumerator backToMenu() {
        GameObject.Find("You Died").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("MainMenu");
    }   

    // Se encarga de hacer aparecer los zombies esperando entre aparición un tiempo aleatorio
    IEnumerator playRound() {
        while(remainingSonguis != 1) {
            SpawnSongui();
            remainingSonguis--;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
        round++;
        SpawnSongui();
        while(GameObject.Find("Songui Base(Clone)") != null) {
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(10);
        remainingSonguis--;
    }

    // Escoge una localización de aparición aleatoria y ejecuto el método "spawn" para que aparezca un zombie
    void SpawnSongui() {
        spawners[Random.Range(0, spawners.Length - 1)].SendMessage("spawn");
    }
}