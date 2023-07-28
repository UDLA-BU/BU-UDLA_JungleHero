using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insigneas : MonoBehaviour
{
    public string ID;
	public AudioSource clip;
    private GameObject soundObj;
    // Start is called before the first frame update
    void Start()
    {
        soundObj = GameObject.Find("SoundManager/GameSoundContainer/CollectedLetter");
        clip = soundObj.GetComponent<AudioSource>();
        clip.Play();
        Destroy(gameObject, 2f);
    }   
}
