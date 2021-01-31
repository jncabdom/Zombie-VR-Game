using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCameraOn : MonoBehaviour {

    // if The user authorize to use the camera then we turn the camera on
    IEnumerator Start() {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam)) {
            PlayCamera();
        }
        else {
            Debug.Log("webcam not found");
        }
    }

    void PlayCamera() {
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
