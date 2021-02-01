using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpsLocation : MonoBehaviour
{
  private string gps; // Guardamos los valores que nos devuelve Input.Location

  IEnumerator Start() {
    // Si la localización no está activa termina
    if (!Input.location.isEnabledByUser)
      yield break;

    Input.location.Start();

    // Mientras está iniciando Input.location espera un máximo de 20 segundos
    int maxWait = 20;
    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
      yield return new WaitForSeconds(1);
      maxWait--;
    }

    // Si no se inicializa guardamos Timed out para mostrarlo por GUI
    if (maxWait < 1) {
      gps = "Timed out";
      yield break;
    }

    // Si la conexión falla
    if (Input.location.status == LocationServiceStatus.Failed) {
      gps = "Unable to determine device location";
      yield break;
    } else {
      // Accedemos a los valores de latitud, longitud...
      gps = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
      transform.Find("GpsLocation").GetComponent<Text>().text = gps;
      Debug.Log("gps:" +  gps);
    }

    // Paramos el servicio
    Input.location.Stop();
  }
}
