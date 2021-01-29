using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonguiStats : MonoBehaviour
{
    private float health = 100f;

    void damageSongui(float damage) {
        Debug.Log("Current health: " + health);
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
