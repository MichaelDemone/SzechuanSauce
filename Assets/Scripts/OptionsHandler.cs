using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHandler : MonoBehaviour {

    public GameObject areYouSure;
    public GameObject resetComplete;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iWantToDunkSomeFreshNugs() {
        areYouSure.SetActive(true);
    }

    public void nvmILikeMySoggyNugs() {
        areYouSure.SetActive(false);
    }

    public void hereAreYourFreshNugs() {
        PlayerPrefs.SetInt("highscore", 0);
        areYouSure.SetActive(false);
        resetComplete.SetActive(true);
    }

    public void okayGoddmnit() {
        resetComplete.SetActive(false);
    }
}
