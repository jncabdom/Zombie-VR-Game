using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Establece las distintas configuraciones del menú de opciones
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;                       // Audio mixer para controlar el volumen de los sonidos
    public TMPro.TMP_Dropdown resolutionDropdown;       // Desplegable con las resoluciones

    Resolution[] resolutions;

    // Obtenemos las distintas resoluciones para la pantalla actual y las añadimos al desplegable de resoluciones
    // y recogemos la resolución elegida
    void Start() {
    
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height) {
                    currentResolutionIndex = i;
                }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Controlamos el volumen del juego
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    // Establecemos la calidad de renderizado del juego
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Activamos la opción de pantalla completa
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

    // Dado un index de una resoluciones establecemos la resolución de la pantalla
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
