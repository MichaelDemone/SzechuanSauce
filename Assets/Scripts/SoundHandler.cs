using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHandler : MonoBehaviour {


    public NugSound[] input;
    public static NugSound[] sounds;
    private float vol;


    // Use this for initialization
    void Start() {
      //  sounds = input;


      /*  if (PlayerPrefs.GetInt("soundOn", 1) == 1)
            AudioPlayer.unMuteSound();
        else if (PlayerPrefs.GetInt("soundOn", 1) == 0)
            AudioPlayer.muteSound();

        if (PlayerPrefs.GetInt("musicOn", 1) == 1)
            AudioPlayer.unMuteMusic();
        else if (PlayerPrefs.GetInt("musicOn", 1) == 0)
            AudioPlayer.muteMusic();
            */
    }



    void Awake() {
        sounds = input;
        /*Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Main") {
            playSound("Game Music");
        } else if(currentScene.name == "MainMenu") {
            playSound("MenuMusic");
        }*/
    }

    // Update is called once per frame
    void Update() {
        if (PlayerPrefs.GetInt("soundOn", 1) == 1)
            AudioPlayer.unMuteSound();
        else if (PlayerPrefs.GetInt("soundOn", 1) == 0)
            AudioPlayer.muteSound();

        if (PlayerPrefs.GetInt("musicOn", 1) == 1)
            AudioPlayer.unMuteMusic();
        else if (PlayerPrefs.GetInt("musicOn", 1) == 0)
            AudioPlayer.muteMusic();
    }



    public static void playSound(string name) {

        //0 = off, 1 = on

        NugSound sound = findSound(name);
        if (sound.isMusic) {
           // if (PlayerPrefs.GetInt("musicOn", 1) == 1) {
                sound.playDaMusic();
           // }

        } else {
           // if (PlayerPrefs.GetInt("soundOn", 1) == 1) {
                sound.playDaNoise();
           // }
        }

    }


    static NugSound findSound(string name) {

        int i = 0;
        while (i<sounds.Length) {
            if (sounds[i].SoundName() == name) {
                return sounds[i];
            } else {
                i++;
            }
        }
        return sounds[0];
    }

}


