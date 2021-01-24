using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMachineLogic : MonoBehaviour
{
    public string perkName;
    public GameObject target;
    public int price;
    public float detectionRadius = 2f;
    private bool purchased = false;
    private PlayerStats playerScript;
    bool print = false;


    void Start() {
        target =  GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update() {
        if (onRange(target)) {
            // Printear mensajito de compra usando perkName y price
            if(Input.GetButtonDown("Action")) {
                buyPerk();
            }
        }
    }

    private bool onRange(GameObject target) {
        return (Vector3.Distance(transform.position, target.transform.position) > detectionRadius);
    }
    
    void buyPerk() {
        // Falta implementar los iconos I guess?
        switch(perkName) {
            case "Quick Revive":
                // No sé como implementar esto aún
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