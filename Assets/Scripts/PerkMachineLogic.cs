using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMachineLogic : MonoBehaviour
{
    public string perkName;
    public GameObject target;
    public int price;
    public float detectionRadius = 4f;
    private bool purchased = false;
    private PlayerStats playerScript;
    bool print = false;
    bool displayMsg = false;
    AudioSource audio;


    void Start() {
        target =  GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerStats>();
        audio = GetComponent<AudioSource>();
    }

    void OnGUI () {
         if (displayMsg) {
            if(PlayerStats.money >= price) {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Press Action to buy " + perkName + "(" + price + ")");
            }
            else GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Insufficient money");
        }
    }

    void Update() {
        if (onRange(target)) {
            if(!purchased) {
                displayMsg = true;
                if((Input.GetButtonDown("Action")) && (PlayerStats.money >= price)) {
                    purchased = true;
                    audio.Play();
                    buyPerk();
                }
            }
            else displayMsg = false;
        }
        else displayMsg = false;
    }

    private bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) < detectionRadius);
    }
    
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
                playerScript.increaseReloadSpeed(1.5f);
            break;
            case "JuggerNog":
                playerScript.increaseHealth(1.5f);
            break;
        }
    }
}