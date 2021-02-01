using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In case that pc controller is select this script set to Active
// Este Script fue utilizado con motivo de Debuggear la aplicación 
// permitiendo mover la cámara con el ratón
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;     // Sensibilidad del ratón
    public Transform playerTransform;         // Transform del jugador
    float xRotation = 0f;                     // Rotación en el eje X

    // Establecemos que el cursor se bloquée
    void Start() {
      Cursor.lockState = CursorLockMode.Locked;
    }

    // Movemos el transform del jugador dependiendo de los valores que nos devuelve los Axis del ratón
    void Update()
    {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -90f, 90f);
      transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
      playerTransform.Rotate(Vector3.up * mouseX);
    }
}
