using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCameraOn : MonoBehaviour {
    Renderer renderer;
    WebCamTexture webcamTexture;

    // if The user authorize to use the camera then we turn the camera on
    void Start() {
        renderer = GetComponent<Renderer>();
        webcamTexture = new WebCamTexture();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
