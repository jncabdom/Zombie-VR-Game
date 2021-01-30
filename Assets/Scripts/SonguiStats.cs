using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonguiStats : MonoBehaviour
{
    private float health = 100f;

    void damageSongui(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
