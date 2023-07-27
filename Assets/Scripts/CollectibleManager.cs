using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int numCollectibles = 10;
    private int contador = 0;

    private GridLayout gridLayout;
    private GameObject currentCollectible;
    public Collectible collectible;

    public delegate void CollectibleCollectedEvent();
    public static event CollectibleCollectedEvent OnCollectibleCollected;

    [Header("Lista de Palabras")]
    public List<string> listaPalabras;
    private List<char> letrasIndividuales;
    private char currentLetter;

    private int IndexInicial = 0;
    private int contadorPalabras = 0;

    private void Start()
    {
        gridLayout = GetComponentInParent<GridLayout>();
        letrasIndividuales = new List<char>(listaPalabras[IndexInicial].ToCharArray());
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
            Vector3Int randomCell = new Vector3Int(Random.Range(-4, 4), Random.Range(-4, 4), 0);
            Vector3 collectiblePosition = gridLayout.CellToWorld(randomCell) + gridLayout.cellSize / 2f;
            collectible.AsignarLetra(letra);
            currentCollectible = Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity, transform);
        }
    }

    public void CollectibleCollected()
    {
        currentCollectible = null; // Indicamos que el objeto coleccionable ha sido recolectado
        contador++;
        Debug.Log(contador);
        while (contadorPalabras < listaPalabras.Count)
        {
            if(contador < letrasIndividuales.Count)
            {
                GenerateCollectible(letrasIndividuales[contador]); // Generamos un nuevo objeto coleccionable
            }

            if (contador == letrasIndividuales.Count)
            {
                Debug.Log("Palabra Completa!");
                letrasIndividuales.Clear();
                letrasIndividuales = new List<char>(listaPalabras[IndexInicial++].ToCharArray());
            }
                contadorPalabras++;
        }

        if (OnCollectibleCollected != null)
        {
            OnCollectibleCollected(); // Disparamos el evento para notificar a los suscriptores que se recolectó un objeto
        }
    }
    public void Premio()
    {

    }
}
