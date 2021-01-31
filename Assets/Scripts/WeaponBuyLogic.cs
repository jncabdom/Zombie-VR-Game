using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void SetWeapon(GameObject newWeapon);
public delegate void BuyBullets();

public class WeaponBuyLogic : MonoBehaviour
{
    public static event SetWeapon OnSetWeapon;
    public static event BuyBullets OnBuyBullets;
    GameObject message;
    Text messageText;

    public Transform CameraTransform;
    public GameObject weapon;
    public string weaponName;

    // Player Detection
    public float detectionRadius = 2f;
    public GameObject player;

    // Prices
    public int weaponPrice = 100;
    public int bulletsPrice = 25;

    bool bought = false;

    bool weaponMsg = true;
    bool bulletsMsg = false;
    bool insufficientMoney = false;

    bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) < detectionRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject reference = GameObject.Find("InteractionText");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
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

    void buyBullets() {
        
    }

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