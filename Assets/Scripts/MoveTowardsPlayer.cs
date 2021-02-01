using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void DamagePlayer(int amount);

// Establece la inteligencia artificial de los zombies
public class MoveTowardsPlayer : MonoBehaviour
{
    AudioSource[] audio;                                // Audio que realizan los zombies al golpear al jugador
    public Transform player;                            // Transform del jugador
    public float deathDistance = 1f;                    // Distancia ante la cual el zombie hará daño
    private NavMeshAgent navComponent;                  // Componente de navegación
    public static event DamagePlayer OnDamagePlayer;    // Evento para bajarle la vida al jugador
    public int damage = 34;                             // Daño por golpe del zombie
    bool isAttacking = false;                           // Nos permite bloquear el ataque para que no sea continuo
    public float cooldown = 1.5f;                       // Enfriamiento entre ataques

    // Obtenemos los audios, al jugador y el navComponent
    void Start() {
        audio = GetComponents<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        navComponent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Calculamos la distancia entre el jugador y el zombie, si es menor que la establecida y no ha atacado recientemente, atacará y gritará
    // Por último cambiamos el destino del zombie en base a la posición del jugador
    void Update() {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < deathDistance && !isAttacking) {
            StartCoroutine(DamagePlayer());
            Roar();
        }       
        navComponent.destination = player.position;
    }
    
    // Escogemos un grito de manera aleatoria y lo ejecutamos
    void Roar() {
        audio[Random.Range(0, audio.Length - 1)].Play();
    }

    // Daña al jugador y establece un enfriamiento para que el daño no se produzca de manera muy continuada
    IEnumerator DamagePlayer() {
        isAttacking = true;
        OnDamagePlayer(damage);
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }
}
