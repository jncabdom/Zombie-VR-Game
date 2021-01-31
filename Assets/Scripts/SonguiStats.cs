using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonguiStats : MonoBehaviour
{
    private float health = 100f;
    private int round;

    void Start()
    {
        health *= (GameController.round * 0.16f + 1);
    }
    void damageSongui(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
