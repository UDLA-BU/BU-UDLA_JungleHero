using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsigneasManager : MonoBehaviour
{
    [Header("Insigneas")]
    public List<GameObject> imagenes = new List<GameObject>();
    private int imagenActualIndex = 0;

    private GameObject currentInsignea;

    private void Awake()
    {
        CollectibleManager.OnAnimalNames += MostrarImagenSiguiente;
    }

    private void OnDisable()
    {
        CollectibleManager.OnAnimalNames -= MostrarImagenSiguiente;
    }
    void Start()
    {
        currentInsignea = Insigneas[0];
        currentInsignea.SetActive(true);
    }

    public void MostrarImagenSiguiente()
    {
        // Apagar la imagen actual
        if (imagenActualIndex >= 0 && imagenActualIndex < imagenes.Count)
        {
            imagenes[imagenActualIndex].SetActive(false);
        }

        // Incrementar el índice y asegurarse de que esté dentro del rango de la lista
        imagenActualIndex++;
        if (imagenActualIndex >= imagenes.Count)
        {
            imagenActualIndex = 0;
        }

        // Encender la siguiente imagen
        if (imagenActualIndex >= 0 && imagenActualIndex < imagenes.Count)
        {
            imagenes[imagenActualIndex].SetActive(true);
        }
    }
}
