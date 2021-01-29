using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyLogic : MonoBehaviour
{
    public Transform CameraTransform;
    public GameObject weapon;
    public string name;

    // Player Detection
    public float detectionRadius = 2f;
    public GameObject player;

    // Prices
    int weaponPrice = 100;
    int bulletsPrice = 25;

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
                        buyBullets();
                    }
                }
                else showMessage("money");
            }
        }
        else showMessage("none");
    }

    void OnGUI() {
        if(weaponMsg) {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Press Action to buy " + name + "(" + weaponPrice + ")");
        }
        if(bulletsMsg) {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Press Action to buy " + name + " bullets (" + weaponPrice + ")");

        }
        if(insufficientMoney) {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Insufficient funds");
        }
    }

    void buyWeapon() {
        bought = true;
        PlayerStats.money -= weaponPrice;

        GameObject gun = Instantiate(weapon, new Vector3(player.transform.position.x,
                                                     player.transform.position.y + 1f,
                                                     player.transform.position.z), Quaternion.identity);
        gun.transform.SetParent(CameraTransform);
        gun.transform.rotation = CameraTransform.rotation;
        gun.transform.localPosition = new Vector3(0.55f, -0.5f, 0.9f);
    }

    void buyBullets() {

    }

    void showMessage(string msg) {
        switch(msg) {
            case "weapon":
                weaponMsg = true;
                bulletsMsg = false;
                insufficientMoney = false;
                break;
            case "bullets":
                weaponMsg = false;
                bulletsMsg = true;
                insufficientMoney = false;
                break;
            case "money":
                weaponMsg = false;
                bulletsMsg = false;
                insufficientMoney = true;
                break;
            case "none":
                weaponMsg = false;
                bulletsMsg = false;
                insufficientMoney = false;
                break;
        }
    }
}
