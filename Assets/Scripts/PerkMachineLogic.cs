using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Establece las distintas lógicas para cada máquina de ventajas que hay en el mapa y las modificaciones en el jugador
public class PerkMachineLogic : MonoBehaviour
{
    public Text messageText;            // Mensaje de compra 
    public string perkName;             // Nombre de la bebida
    public GameObject target;           // Objeto del jugador
    public int price;                   // Precio de la bebida
    public float detectionRadius = 4f;  // Radio ante el cual muestra el mensaje
    private bool purchased = false;     // Controla si ya se ha comprado la bebida
    private PlayerStats playerScript;   // Script para acceder a distintas variables del player
    AudioSource audio;                  // Audio que se reproduce cuando se compra la bebida


    // Obtenemos los audios y al jugador
    void Start() {
        audio = GetComponent<AudioSource>();
        target =  GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Si el jugador está cerca y no ha comprado la bebida le mostramos un mensaje si pulsa el botón de acción 
    // comprará la bebida y se reproduce el audio
    void Update() {
        if (onRange(target)) {
            if(!purchased) {
                if(PlayerStats.money >= price) {
                    messageText.text = "Press Action to buy " + perkName + "(" + price + ")";
                    if(Input.GetButtonDown("Action")) {
                        messageText.text = "";
                        purchased = true;
                        audio.Play();
                        buyPerk();
                    }
                }
                else messageText.text = "Insufficient funds";
            }
        }
        else messageText.text = "";
    }

    // Si el jugador está en rango devuelve verdadero
    private bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) < detectionRadius);
    }
    
    // Establece el efecto de la bebida dependiendo del nombre
    void buyPerk() {
        PlayerStats.money -= price;
        switch(perkName) {
            case "Quick Revive":
                playerScript.increaseRecovery(1.5f);
            break;
            case "Double Tap":
                playerScript.increaseDmg(1.5f);
            break;
            case "Speed Cola":
                playerScript.increaseMagazineSize(2f);
            break;
            case "JuggerNog":
                playerScript.increaseHealth(1.5f);
            break;
        }
    }
}