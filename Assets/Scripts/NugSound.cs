using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NugSound : MonoBehaviour{


    public string soundName;
    public AudioClip sound;
    public bool isMusic;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public NugSound(string name, AudioClip sound, bool isMusic) {
        this.sound = sound;
        this.soundName = name;
        this.isMusic=isMusic;
    }
    public NugSound() {

    }

    public string SoundName() {
        return soundName;
    }

    public void muteMusic() {
        AudioPlayer.muteMusic();
    }

    public void muteSound() {
        AudioPlayer.unMuteSound();
    }

    public void unMuteMusic() {
        AudioPlayer.muteMusic();
    }

    public void unMuteSound() {
        AudioPlayer.unMuteSound();
    }


    public void playDaNoise() {

        AudioPlayer.PlayFileSound(sound); 
    }

    public void playDaMusic() {
        AudioPlayer.PlayFileMusic(sound);
    }

    


}
