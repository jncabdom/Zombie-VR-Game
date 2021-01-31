using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void DamagePlayer(int amount);

public class MoveTowardsPlayer : MonoBehaviour
{
    AudioSource[] audio;
    public Transform player;
    public float deathDistance = 1f;
    private NavMeshAgent navComponent;
    public static event DamagePlayer OnDamagePlayer;
    public int damage = 34;
    bool isAttacking = false;
    public float cooldown = 1.5f;

    // Start is called before the first frame update
    void Start() {
        audio = GetComponents<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        navComponent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < deathDistance && !isAttacking) {
            StartCoroutine(DamagePlayer());
            Roar();
        }       
        navComponent.destination = player.position;
    }

    void Roar() {
        audio[Random.Range(0, audio.Length - 1)].Play();
    }

    IEnumerator DamagePlayer() {
        isAttacking = true;
        OnDamagePlayer(damage);
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }
}
