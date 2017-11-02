using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dipping : MonoBehaviour {
    
    public float TimeToChoose;
    public float NextTimeItsShorter;
    public Sauce[] Sauces;

    public Text StatsText;
    public GameObject button;
    private float initialTimeToChoose;
    
    private int score;
    public static int highScore;
    private Sauce currentSauce;
    private Sauce szechuanSauce;

    public SwipingHandler SwipingHandler;

    public Transform SpawnPoint;

    public float ForceMultiplier;
    public Vector2 MaxForce = new Vector2(100, 100);
    
    
    public bool ShouldScaleWithSwipeLength = true;
    
	// Use this for initialization
	void Start () 
	{
        score = 0;
	    SwipingHandler.UserSwiped += UserSwiped;
	    SwipingHandler.UserTapped += UserTapped;
	    
	    initialTimeToChoose = TimeToChoose;
	    
	    GiveNewSauce(TimeToChoose);
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
        if (currentSauce.IsSzechuan)
        {
            GotItRight();
        }
        else
        {
            GotItWrong();
        } 
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

        if (currentSauce.IsSzechuan)
        {
            GotItWrong();
        }
        else
        {
            GotItRight();
        }
    }
    
    IEnumerator failTimer;

    void GiveNewSauce(float time)
    {
        failTimer = MyCoroutine(time);
        StartCoroutine(failTimer);
        
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

    void GotItRight()
    {
        TimeToChoose -= NextTimeItsShorter;

        if (currentSauce.IsSzechuan)
        {
            score += 5;
            StopCoroutine(failTimer);
            currentSauce.GetComponent<AudioSource>().Play();
            //Destroy(currentSauce.gameObject);
            currentSauce.DunkTheNug(() => GiveNewSauce(TimeToChoose));
            //GiveNewSauce(TimeToChoose);
        }
        else
        {
            score++;
            StopCoroutine(failTimer);
            //Destroy(currentSauce.gameObject);
            GiveNewSauce(TimeToChoose);
        }
       
    }

    void GotItWrong()
    {
        //game over
        if(score > highScore)
        {
            highScore = score;
        }
        print("YOU LOSE. Highscore " + highScore + ". Your score " + score + " Time to choose: " + TimeToChoose);

        StopCoroutine(failTimer);
        if(currentSauce != null) Destroy(currentSauce.gameObject);

        //TimeToChoose = initialTimeToChoose;
        
        
        //GiveNewSauce(TimeToChoose);
        button.SetActive(true);
        Text buttonText = button.GetComponentInChildren<Text>();
        buttonText.text = "Your Score: " + score + "\n"
                            + "Highscore: " + highScore;
        score = 0;
    }

    
    IEnumerator MyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GotItWrong();
    }
}
