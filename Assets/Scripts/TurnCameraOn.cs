using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enciende la cámara, mostrándola en una televisión en una de las salas
public class TurnCameraOn : MonoBehaviour {
    Renderer renderer;
    WebCamTexture webcamTexture;

    // Obtenemos el Renderer del objeto y le aplicamos la textura webcamTexture
    void Start() {
        renderer = GetComponent<Renderer>();
        webcamTexture = new WebCamTexture();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
