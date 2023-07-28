using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int numCollectibles = 10;
    private int contador = 0;
    private int contadorCollectibles = 0;

    private GridLayout gridLayout;
    private GameObject currentCollectible;
    public Collectible collectible;

    public delegate void CollectibleCollectedEvent();
    public static event CollectibleCollectedEvent OnCollectibleCollected;
    public static event CollectibleCollectedEvent OnWordCollected;

    public delegate void AnimalsNames();
    public static event AnimalsNames OnAnimalNames;

    [Header("RangoXY")]
    public int RangoX;
    public int RangoY;

    [Header("Lista de Palabras")]
    public List<string> listaPalabras = new List<string>();
    private List<char> letrasIndividuales = new List<char>();
    private List<int> numLetras = new List<int>();
    private char currentLetter;

    [Header("Insigneas")]
    public List<GameObject> Insigneas = new List<GameObject>();
    public Transform insigneaPosicion;


    private int IndexInicial = 0;
    private int contadorPalabras = 0;

    private void Start()
    {
        gridLayout = GetComponentInParent<GridLayout>();
        foreach(string palabra in listaPalabras)
        {
            letrasIndividuales.AddRange(palabra.ToCharArray());
            numLetras.Add(palabra.Length);
        }

        currentLetter = letrasIndividuales[IndexInicial];
        Debug.Log(letrasIndividuales.Count);
        for (int i = 0; i < numCollectibles; i++)
        {
            GenerateCollectible(currentLetter);
        }
    }

    private void GenerateCollectible(char letra)
    {
        if (currentCollectible == null)
        {
            Vector3Int randomCell = new Vector3Int(Random.Range(-RangoX, RangoX), Random.Range(-RangoY, RangoY), 0);
            Vector3 collectiblePosition = gridLayout.CellToWorld(randomCell) + gridLayout.cellSize / 2f;
            collectible.AsignarLetra(letra);
            currentCollectible = Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity, transform);
        }
    }

    public void CollectibleCollected()
    {
        currentCollectible = null; // Indicamos que el objeto coleccionable ha sido recolectado
        contador++;
        contadorCollectibles++;
        Debug.Log(contador);
        if (contador < letrasIndividuales.Count)
        {
            GenerateCollectible(letrasIndividuales[contadorCollectibles]); // Generamos un nuevo objeto coleccionable
        }

        if (contador == numLetras[contadorPalabras])
        {
            Debug.Log("Palabra Completa! " + listaPalabras[contadorPalabras]);
            contador = 0;
            OnWordCollected?.Invoke();
            OnAnimalNames?.Invoke();
            if(contadorPalabras < Insigneas.Count)
            {
                contadorPalabras++;
                Instantiate(Insigneas[contadorPalabras-1], insigneaPosicion.position, Quaternion.identity);
            }
        }

        OnCollectibleCollected?.Invoke(); // Disparamos el evento para notificar a los suscriptores que se recolectó un objeto
    }
    public void Premio()
    {

    }
}
