using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Almacena las distintas estadísticas del zombie
public class SonguiStats : MonoBehaviour
{
    public static event EarnMoney OnEarnMoney;  // Si se muere el zombie el jugador gana dinero

    private float health = 100f;                // Vida del zombie
    //private int round;                          // ronda
    public int amount = 150;                    // Cantidad de dinero que gana el jugador

    // Obtenemos la vida en función de la ronda en la que se encuentra el jugador
    void Start()
    {
        health *= (GameController.round * 0.16f + 1);
    }

    // Dañamos al zombie, en caso de que la vida descienda de 0 destruimos el objeto y ejecutamos el evento
    void damageSongui(float damage) {
        health -= damage;
        if (health <= 0) {
            OnEarnMoney(amount);
            Destroy(gameObject);
        }
    }
}
