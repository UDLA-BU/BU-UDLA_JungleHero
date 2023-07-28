using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float levelTime = 0f; // Duraci�n total del nivel en segundos
    private float currentTime = 0f; // Tiempo restante del nivel
    private int baseScore = 100; // Puntaje base por recolectar un coleccionable
    private int maxAdditionalScore = 200; // Puntos adicionales m�ximos por completar el nivel r�pidamente
    public Text textoTimer;
    private void Start()
    {
        currentTime = levelTime;
    }

    private void Update()
    {
        textoTimer.text = "" + currentTime.ToString("f1");
        // Actualizar el temporizador
        if (levelTime < 1f)
        {
            currentTime += Time.deltaTime;
            if (currentTime <= 0f)
            {
                // Aqu� puedes manejar lo que sucede cuando se acabe el tiempo (por ejemplo, reiniciar el nivel o mostrar una pantalla de game over).
            }
        }
    }

    public void OnCollectibleCollected()
    {
        // Aqu� puedes implementar cualquier l�gica adicional que desees cuando se recolecte un objeto coleccionable
        int additionalScore = CalculateAdditionalScore(currentTime);
        int totalScore = baseScore + additionalScore;
        // Agregar totalScore al puntaje actual del jugador y mostrarlo en la interfaz de usuario.
    }

    private int CalculateAdditionalScore(float timeRemaining)
    {
        // Calcula los puntos adicionales seg�n el tiempo restante.
        return Mathf.RoundToInt(maxAdditionalScore - (timeRemaining / levelTime) * maxAdditionalScore);
    }

    public void ResetTimer()
    {
        currentTime = levelTime;
        print(CalculateAdditionalScore(currentTime));
    }
}
