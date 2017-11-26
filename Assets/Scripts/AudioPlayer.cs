using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour {

    private static AudioPlayer instance;
    private static AudioSource audioSourceSound;
    private static AudioSource audioSourceMusic;
    public AudioSource sound;
    public AudioSource music;


    void Start() {
        audioSourceSound = sound;
        audioSourceMusic = music;


        //I want this in its own script but when I try to do that it has a bad time so its here. 
       Scene currentScene = SceneManager.GetActiveScene();
        print(currentScene.name);
        if (currentScene.name == "Main") {
          SoundHandler.playSound("Game Music");
         } else if (currentScene.name == "MainMenu") {
            SoundHandler.playSound("main menu");
        }

       /* if (currentScene.name == "MainMenu") {
            SoundHandler.playSound("main menu"); 
                }*/
        }


    public static void PlayFileMusic(AudioClip clip) {
        audioSourceMusic.PlayOneShot(clip);
    }

    public static void PlayFileSound(AudioClip clip) {
        audioSourceSound.PlayOneShot(clip);
    }

    // Use this for initialization
    void Awake() {
        instance = this;
      //  audioSource = GetComponent<AudioSource>();

    }	

    public static void muteMusic() {
        audioSourceMusic.volume = 0f;
    }
    public static void muteSound() {
        audioSourceSound.volume = 0f;
    }

    public static void unMuteMusic() {
        audioSourceMusic.volume = 1f;
    }
    public static void unMuteSound() {
        audioSourceSound.volume = 1f;
    }
}
