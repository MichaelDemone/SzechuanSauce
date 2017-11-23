﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {
    public GameObject CreditsPanel;
    public GameObject ThatAnnoyingNug;
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject TastyOptions;
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
        ThatAnnoyingNug.SetActive(false);
    }

    public void CreditsBegone() {
        CreditsPanel.SetActive(false);
        ThatAnnoyingNug.SetActive(true);
    }

    public void HowToPlay1() {
        Tutorial1.SetActive(true);
    }

    public void HowToPlay2() {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
    }

    public void HowToPlayBegone() {
        Tutorial2.SetActive(false);
    }

<<<<<<< HEAD
    public void PlayFast() {
        SceneManager.LoadScene("TimeAttack");
=======
    public void TastyOptionsComeHither() {
        TastyOptions.SetActive(true);
    }

    public void TastyOptionsBegone() {
        TastyOptions.SetActive(false);
>>>>>>> e5fb8dba8c61dc6467bfb384bf588d11af27368e
    }

}
