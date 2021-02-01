using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate bool EnoughMoney(int price);
public delegate void DecreaseMoney(int price);


// Nos permite las puertas del mapa, cuando el jugador está cerca de una puerta se le muestra un texto para que pulse el botón de acción
// y se abra la puerta, se le sustrae el dinero al jugador
public class BuyDoor : MonoBehaviour
{
    public Text messageText;                                 // Texto que se le muestra al jugador
    public static event DecreaseMoney OnDecreaseMoney;       // Evento para quitarle dinero al jugador
    public static event EnoughMoney OnEnoughMoney;           // Evento que nos permite controlar si el jugador tiene suficiente dinero
    public GameObject player;                                // Jugador
    public float radius = 2f;                                // Distancia máxima ante la que se mostrará el mensaje
    public int cost = 1000;                                  // Coste asociado a abrir la puerta
    public int y = -1;                                       // Muestra la posición en el eje vertical al que se moverá la puerta una vez comprada, se usará para animar la puerta
    public float speed = 200f;                               // Velocidad de movimiento en cada iteración de la animación
    private float distanceToTarget = 1f;                     // Distancia mínima ante la cual se considera que llegó a la posición final y se deja de animar la puerta
    AudioSource audioOpen;                                   // Audio de la puerta que se reproduce al abrir
    bool bought = false;                                     // Booleano usado para conocer si se abrió una puerta

    // Obtenemos todos los AudioSource del gameObject
    void Start()
    {
        audioOpen = GetComponent<AudioSource>();
    }

    // Devuelve verdadero o falso en función de si el jugador está en rango o no
    bool InRange() {
        return Vector3.Distance(player.transform.position, transform.position) <= radius;
    }

    // Si el jugador está en rango y no ha comprado aún la puerta se le mostrará un mensaje, si además pulsa el botón de acción le quitaremos el dinero al jugador y empezaremos la animación
    // para que desaparezca la puerta
    void Update()
    {
        if (InRange()) {
            if (!bought)
                messageText.text = "Press Action to clear debris (" + cost + ")";
            if (Input.GetButtonDown("Action") && OnEnoughMoney(cost)) {
                bought = true;
                OnDecreaseMoney(cost);
                transform.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(Disappear());
                messageText.text = "";
            }
        }
        else messageText.text = "";
    }

    // Esta corrutina se encarga de mover la puerta hacia debajo, una vez este fuera de la vista del jugador desactivamos la puerta
    IEnumerator Disappear() {
        audioOpen.Play();
        Vector3 target = new Vector3(transform.position.x, transform.position.y * y, transform.position.z);
        while (Vector3.Distance(transform.position, target) > distanceToTarget) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForSeconds(2.5f);
        }
        gameObject.SetActive(false);
    }
}