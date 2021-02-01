using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla las posiciones en las que aparece los zombies
public class SpawnController : MonoBehaviour
{
    public GameObject songui;               // Zombie
    public Vector3 spawnValues;             // posición mínima y máxima de aparición en los distintos ejes de coordenadas

    // Instancia un zombie dentro de los límites dados
    public void spawn()
    {
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 1, Random.Range (-spawnValues.z, spawnValues.z));
        Instantiate (songui, spawnPosition + transform.TransformPoint (0, 0 ,0), gameObject.transform.rotation);
    }
}