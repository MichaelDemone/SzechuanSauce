using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {
    public GameObject CreditsPanel;
    private static int CountdownToPayday = 0;
    // Use this for initialization

    public void PressPlay()
    {
        if (CountdownToPayday < 6) {
            CountdownToPayday++;
            SceneManager.LoadScene("Main");
        } else {
            Advertisement.Show();
            CountdownToPayday = 0;
        }

    }

    public void BackToMenu()
    {
        if (CountdownToPayday < 6) {
            CountdownToPayday++;
            SceneManager.LoadScene("MainMenu");
        } else {
            Advertisement.Show();
            CountdownToPayday = 0;
        }
        
    }
    
    public void WhoIsResponsible() 
    {
        CreditsPanel.SetActive(true);
    }

    public void CreditsBegone() {
        CreditsPanel.SetActive(false);
    }
}
