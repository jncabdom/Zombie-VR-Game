using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void SetWeapon(GameObject newWeapon);
public delegate void BuyBullets();


// Controla la lógica de la compra de armas 
public class WeaponBuyLogic : MonoBehaviour
{
    public static event SetWeapon OnSetWeapon;          // Evento para cambiar el arma del jugador
    public static event BuyBullets OnBuyBullets;        // Evento para llenar de balas el arma
    GameObject message;                                 // Mensaje de compra de arma o balas 
    Text messageText;

    public Transform CameraTransform;                   // Transform de la cámara para obtener la rotación de esta
    public GameObject weapon;                           // Arma que queremos comprar
    public string weaponName;                           // Nombre del arma

    // Player Detection
    public float detectionRadius = 2f;                  // Radio dentro del cual se considera que se encuentra dentro de la zona de compra
    public GameObject player;                           // Jugador 

    // Prices   
    public int weaponPrice = 100;                       // Precio del arma
    public int bulletsPrice = 25;                       // Precio de las balas

    bool bought = false;                                // Controla si el arma se ha comprado

    bool weaponMsg = true;                              // Muestra el mensaje de comprar arma
    bool bulletsMsg = false;                            // Muestra el mensaje de comprar balas
    bool insufficientMoney = false;                     // Muestra el mensaje de dinero insuficiente

    // Devuelve verdadero si el jugador se encuentra cerca de la zona de compra(arma)
    bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) < detectionRadius);
    }

    // Obtenemos el texto de la UI y el jugador
    void Start()
    {
        GameObject reference = GameObject.Find("InteractionText");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
        player = GameObject.Find("Player");
    }

    // SI el jugador está dentro del rango y no ha comprado el arma, tiene suficiente dinero se muestra un mensaje
    // Si además pulsa el botón de acción compra el arma de la pared, si no tiene suficiente muestra el mensaje
    // de dinero insuficiente, en caso de que la haya comprado se realiza de forma parecida pero mostrando el mensaje
    // de comprar balas
    void Update()
    {
        if(onRange(player)) {
            if(!bought) {
                if(PlayerStats.money >= weaponPrice) {
                    showMessage("weapon");
                    if(Input.GetButtonDown("Action")) {
                        buyWeapon();
                    }
                }
                else showMessage("money");
            }
            else {
                if(PlayerStats.money >= bulletsPrice) {
                    showMessage("bullets");
                    if(Input.GetButtonDown("Action")) {
                        OnBuyBullets();
                    }
                }
                else showMessage("money");
            }
        }
        else showMessage("none");
    }

    // Al comprar un arma Instanciamos el arma y la situamos dentro de la cámara para que obtenga la rotación y movimiento de esta,
    // eliminamos el arma que anteriormente tenía el jugador
    void buyWeapon() {
        bought = true;
        PlayerStats.money -= weaponPrice;
        player.SendMessage("destroyWeapon");

        GameObject gun = Instantiate(weapon, new Vector3(player.transform.position.x,
                                                     player.transform.position.y + 1f,
                                                     player.transform.position.z), Quaternion.identity);
        gun.GetComponent<GunScript>().walled = false;
        gun.name = weaponName;
        OnSetWeapon(weapon);
        gun.transform.SetParent(CameraTransform);
        gun.transform.rotation = CameraTransform.rotation;
        gun.transform.localPosition = new Vector3(0.55f, -0.5f, 2f);
    }

    // Mostramos el mensaje correspondiente dependiendo de los valores booleanos 
    // Establecidos como variables de la clase
    void showMessage(string msg) {
        switch(msg) {
            case "weapon":
                messageText.text = "Press Action to buy " + weaponName + "(" + weaponPrice + ")";
                break;
            case "bullets":
                messageText.text = "Press Action to buy " + weaponName + " bullets (" + weaponPrice + ")";
                break;
            case "money":
                messageText.text = "Insufficient funds";
                break;
            case "none":
                messageText.text = "";
                break;
        }
    }
}