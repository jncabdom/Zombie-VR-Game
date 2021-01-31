using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCameraOn : MonoBehaviour {

    // if The user authorize to use the camera then we turn the camera on
    IEnumerator Start() {
        PlayCamera();
    }

    void PlayCamera() {
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
