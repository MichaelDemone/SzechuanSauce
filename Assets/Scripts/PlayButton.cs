using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    public GameObject CreditsPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressPlay()
    {
        SceneManager.LoadScene("Main");

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void WhoIsResponsible() 
    {
        CreditsPanel.SetActive(true);
    }

    public void CreditsBegone() {
        CreditsPanel.SetActive(false);
    }
}
