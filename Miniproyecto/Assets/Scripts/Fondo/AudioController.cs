using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Asegura que el volumen se inicialice correctamente
        volumeSlider.minValue = 1;
        volumeSlider.maxValue = 100;
        volumeSlider.value = 100;

        // Asigna el método OnVolumeChange al evento del Slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        // Ajusta el volumen inicial
        audioSource.volume = volumeSlider.value / 100f;
    }

    void OnVolumeChange(float value)
    {
        // Convierte de 1-100 a 0.01-1.0
        audioSource.volume = value / 100f;
    }
}
