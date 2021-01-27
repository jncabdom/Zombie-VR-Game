using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player;
    public float deathDistance = 0.5f;
    private NavMeshAgent navComponent;

    // Start is called before the first frame update
    void Start()
    {
        navComponent = gameObject.GetComponent<NavMeshAgent>();
        //navComponent.destination = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        navComponent.destination = player.position;
        if (dist < deathDistance) {
            //matalo puta
        }
    }
}
