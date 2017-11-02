using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScore : MonoBehaviour {
    public GameObject Text;
	// Use this for initialization
	void Start () {
        Text highscore = Text.GetComponentInChildren<Text>();
            highscore.text = "Highscore: " + Dipping.highScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
