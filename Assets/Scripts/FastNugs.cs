﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;


public class FastNugs : MonoBehaviour {


    public static int timeHighScore;

    public float TimeToChoose;
    //public float NextTimeItsShorter;
    public Sauce[] Sauces;

    public Text StatsText;
    public GameObject button;
    public GameObject cameraObject;
    public float ShakeIntensity;
    public float ShakeDuration;
    private float initialTimeToChoose;
    public int nugsToDunk = 10;
    
    private int score;
    private int nugsDunked;
    private float deltaTime;
    private float totalTime = 1000;
    private float startTime;
    private float endTime;
    private Sauce currentSauce;
    private Sauce szechuanSauce;

    public SwipingHandler SwipingHandler;

    public Transform SpawnPoint;

    public float ForceMultiplier = 0.001f;
    public Vector2 MaxForce = new Vector2(100, 100);
    
    
    public bool ShouldScaleWithSwipeLength = true;
    
	// Use this for initialization
	void Start () 
	{
        timeHighScore = PlayerPrefs.GetInt("speedHighscore", 0);
        nugsDunked = 0;
        score = 0;
	    SwipingHandler.UserSwiped += UserSwiped;
	    SwipingHandler.UserTapped += UserTapped;
	    
	    initialTimeToChoose = TimeToChoose;
	    
	    GiveNewSauce(TimeToChoose);

        failTimer = MyCoroutine(TimeToChoose);
        StartCoroutine(failTimer);

    }

    void Update()
    {
        //StatsText.text = "Time: " + TimeToChoose;
        StatsText.text = "\nScore: " + score;
        //StatsText.text += "\nHighscore: " + highScore;
        //StatsText.text += "\nCurrent sauce: " + currentSauce.name;
        //StatsText.text += "\nLast swipe: " + lastSwipeDirection;
    }

    void UserTapped()
    {
        /* if (currentSauce.IsSzechuan)
         {
             GotItRight();
         }
         else
         {
             GotItWrong();
         } */

        GotItRight(true);
    }
    
    private Vector3 forceDir = new Vector3();
    void UserSwiped(Vector2 direction)
    {
        
        forceDir.x = direction.x;
        forceDir.z = direction.y;
        if (ShouldScaleWithSwipeLength) forceDir.Normalize();
        forceDir *= ForceMultiplier;
        //forceDir.x = Math.Min(forceDir.x, MaxForce.x);
        //forceDir.y = Math.Min(forceDir.y, MaxForce.y);
        currentSauce.GetComponent<Rigidbody>().AddForce(forceDir);
        currentSauce.GetComponent<AudioSource>().Play();

        /*if (currentSauce.IsSzechuan)
        {
            GotItWrong();
        }
        else
        {
            GotItRight();
        }*/

        GotItRight(false);
    }
    
    IEnumerator failTimer;

    void GiveNewSauce(float time)
    {
        //failTimer = MyCoroutine(time);
        //StartCoroutine(failTimer);
        startTime = Time.time;
        int randVal = Random.Range(0, Sauces.Length - 1);
        currentSauce = Sauces[randVal];
        currentSauce.Init();
        Vector3 rotation;
        rotation.x = 90;
        rotation.y = 0;
        rotation.z = -90;
        currentSauce = Instantiate(currentSauce.gameObject, SpawnPoint.position, Quaternion.identity).GetComponent<Sauce>();
        currentSauce.transform.Rotate(rotation);
    }

    /* void GotItRight()
     {
         endTime = Time.time;
         deltaTime = endTime - startTime;
         totalTime = 1000 - deltaTime*1000;
         if (currentSauce.IsSzechuan)
         {
             score+=Mathf.RoundToInt(totalTime);
             nugsDunked++;
             StopCoroutine(failTimer);
             cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);

             if (nugsDunked <= nugsToDunk) {
                 cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
                 currentSauce.GetComponent<AudioSource>().Play();
                 currentSauce.DunkTheNug(() => GiveNewSauce(TimeToChoose));
             } 
             else {
                 if (score > timeHighScore) {
                     timeHighScore = score;
                     PlayerPrefs.SetInt("speedHighscore", timeHighScore);
                     PlayerPrefs.Save();
                 }
                 cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
                 currentSauce.GetComponent<AudioSource>().Play();
                 currentSauce.DunkTheNug(() => 
                 {
                     button.SetActive(true);
                     Text buttonText = button.GetComponentInChildren<Text>();
                     buttonText.text = "Your Score: " + "\n" + score + "\n" + "Highscore: " + timeHighScore;
                 score = 0; });

             }

         }
         else
         {
             nugsDunked++;
             score+=Mathf.RoundToInt(totalTime);
             StopCoroutine(failTimer);
             if (nugsDunked <= nugsToDunk) {
                 GiveNewSauce(TimeToChoose);
             } 
             else {
                 if (score > timeHighScore) {
                     timeHighScore = score;
                     PlayerPrefs.SetInt("speedHighscore", timeHighScore);
                     PlayerPrefs.Save();
                 }
                 button.SetActive(true);
                 Text buttonText = button.GetComponentInChildren<Text>();
                 buttonText.text = "Your Score: " + score + "\n"
                                     + "Highscore: " + timeHighScore;
                 score = 0;
             }
         }

     }*/

    void GotItRight(bool dunk) {
        endTime = Time.time;
        deltaTime = endTime - startTime;
        totalTime = 1000 - deltaTime*1000;
        if (totalTime < 0)
            totalTime=0;
        if (dunk) {
            if (currentSauce.IsSzechuan) {
                score+=Mathf.RoundToInt(totalTime)*10;
                nugsDunked++;
                // StopCoroutine(failTimer);
                cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);

                if (nugsDunked <= nugsToDunk) {
                    cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
                    currentSauce.GetComponent<AudioSource>().Play();
                    currentSauce.DunkTheNug(() => GiveNewSauce(TimeToChoose));
                } else {
                    StopCoroutine(failTimer);
                    if (score > timeHighScore) {
                        timeHighScore = score;
                        PlayerPrefs.SetInt("speedHighscore", timeHighScore);
                        PlayerPrefs.Save();
                    }
                    cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
                    currentSauce.GetComponent<AudioSource>().Play();
                    currentSauce.DunkTheNug(() => {
                        button.SetActive(true);
                        Text buttonText = button.GetComponentInChildren<Text>();
                        buttonText.text = "Your Score: " + "\n" + score + "\n" + "Highscore: " + timeHighScore;
                        score = 0;
                    });

                }

            } else {
                nugsDunked++;
                score+=Mathf.RoundToInt(totalTime)*3;
                // StopCoroutine(failTimer);
                if (nugsDunked <= nugsToDunk) {
                    currentSauce.DunkTheNug(() => GiveNewSauce(TimeToChoose)); ;
                } else {
                    StopCoroutine(failTimer);
                    if (score > timeHighScore) {
                        timeHighScore = score;
                        PlayerPrefs.SetInt("speedHighscore", timeHighScore);
                        PlayerPrefs.Save();
                    }
                    currentSauce.DunkTheNug(() => {
                        button.SetActive(true);
                        Text buttonText = button.GetComponentInChildren<Text>();
                        buttonText.text = "Your Score: " + score + "\n"
                                        + "Highscore: " + timeHighScore;
                        score = 0;
                    });
                }
            }
        } else {
            if (currentSauce.IsSzechuan) {
                currentSauce.transform.Find("THANUG").gameObject.SetActive(false);
                currentSauce.transform.Find("Quad").gameObject.SetActive(false);
                score -= Mathf.RoundToInt(Mathf.Max(1000f, totalTime*10));               
                GiveNewSauce(TimeToChoose);                                        

            } else {
                score+=Mathf.RoundToInt(totalTime);
                GiveNewSauce(TimeToChoose); 
            }
        }

    }


    void GotItWrong()
    {
        //game over
        if(score > timeHighScore)
        {
            timeHighScore = score;
            PlayerPrefs.SetInt("speedHighscore", timeHighScore);
            PlayerPrefs.Save();
        }
        print("YOU LOSE. Highscore " + timeHighScore + ". Your score " + score + " Time to choose: " + TimeToChoose);

        StopCoroutine(failTimer);
        if(currentSauce != null) Destroy(currentSauce.gameObject);

        button.SetActive(true);
        Text buttonText = button.GetComponentInChildren<Text>();
        buttonText.text = "Game Over!";
        score = 0;
    }

    
    IEnumerator MyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GotItWrong();
    }
}
