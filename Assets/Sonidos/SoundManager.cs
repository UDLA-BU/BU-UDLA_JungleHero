using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   //Singleton - Asegurar que hay una instancia de esa clase en hora mismo(?)
   //Make sure there is one instance of this class at any given time
   public static SoundManager Instance;
   
   [SerializeField] private AudioSource _musicSource, _effectsSource;
   
   void Awake() {
   		if (Instance == null) {
   			Instance = this;
			DontDestroyOnLoad(gameObject);
   			}
		else {
			Destroy(gameObject);
		}
   }
   
   public void PlaySound(AudioClip clip) {
   		_effectsSource.PlayOneShot(clip);
   }
}
