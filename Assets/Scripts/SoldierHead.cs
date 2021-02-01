using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla a través del acelerómetro la  rotación del gameObject
public class SoldierHead : MonoBehaviour
{
    // Rota el transform del gameObject asociado en función del acelerómetro
    void Update()
    {
        Debug.Log(new Vector3(Input.acceleration.y, Input.acceleration.x, 0));
        transform.Rotate(Input.acceleration * Time.deltaTime);
    }
}