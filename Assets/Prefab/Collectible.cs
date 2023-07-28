using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10;
    public TMP_Text textoLetra;
    [SerializeField] private char letraActual;
	private AudioSource collect;
    private GameObject soundObj;
    public string letra = "o";

    //public delegate void GetLetterDelegate(char letra);
    //public event GetLetterDelegate GetLetterEvent;

    private CollectibleManager collectibleManager;

    private void Awake()
    {
        collectibleManager = GetComponent<CollectibleManager>();
        soundObj = GameObject.Find("SoundManager/GameSoundContainer/CollectedLetter");
        collect = soundObj.GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    public void AsignarLetra(char letra)
    {
        letraActual = letra;
        Debug.Log("la letra es " + letraActual);
        textoLetra.text = letraActual.ToString();
    }
     
    public void Collect()
    {
        collect.Play();
        //GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
