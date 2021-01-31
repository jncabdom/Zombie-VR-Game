using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHead : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log(new Vector3(Input.acceleration.y, Input.acceleration.x, 0));
        transform.Rotate(Input.acceleration * Time.deltaTime);
    }
}