using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkMachineLogic : MonoBehaviour
{
    GameObject message;
    Text messageText;
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
        GameObject reference = GameObject.Find("InteractionText");
        message = Instantiate(reference, reference.transform.position, reference.transform.rotation);
        message.transform.SetParent(GameObject.Find("PlayerUI").GetComponent<Transform>());
        message.transform.localScale = reference.transform.localScale;
        messageText = message.GetComponent<Text>();
        audio = GetComponent<AudioSource>();
    }

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