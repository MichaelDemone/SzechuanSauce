using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {
    public Text classicHighScoreText;
    public Text speedHighScoreText;
    public GameObject speedMode;
    public GameObject classicMode;
    public GameObject classicNug;
    public GameObject speedNug;
	// Use this for initialization
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
		classicHighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
	}

    public void SpeedModeActive() {
        classicMode.SetActive(false);
        classicNug.SetActive(false);
        speedMode.SetActive(true);
        speedNug.SetActive(true);
        speedHighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("speedHighscore", 0);
    }

    public void ClassicModeActive() {
        speedMode.SetActive(false);
        speedNug.SetActive(false);
        classicMode.SetActive(true);
        classicNug.SetActive(true);
        classicHighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
    }
}
