using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dipping : MonoBehaviour {
    
    public float TimeToChoose;
    public float NextTimeItsShorter;
    public Sauce[] Sauces;

    public Text StatsText;
    
    private float initialTimeToChoose;
    
    private int score;
    private int highScore;
    private Sauce currentSauce;
    private Sauce szechuanSauce;

    public SwipingHandler SwipingHandler;
    
	// Use this for initialization
	void Start () 
	{
        score = 0;
	    SwipingHandler.UserSwiped += UserSwiped;
	    SwipingHandler.UserSwiped += UserSwipedStats;
	    
	    initialTimeToChoose = TimeToChoose;
	    
	    GiveNewSauce(TimeToChoose);
	}

    void Update()
    {
        StatsText.text = "Time: " + TimeToChoose;
        StatsText.text += "\nScore: " + score;
        StatsText.text += "\nHighscore: " + highScore;
        StatsText.text += "\nCurrent sauce: " + currentSauce.name;
        StatsText.text += "\nLast swipe: " + lastSwipeDirection;
    }

    private string lastSwipeDirection = "";

    private void UserSwipedStats(SwipingHandler.SwipeDirection direction)
    {
        switch (direction)
        {
                case SwipingHandler.SwipeDirection.Up:
                    lastSwipeDirection = "Up";
                    break;
                case SwipingHandler.SwipeDirection.Down:
                    lastSwipeDirection = "Down";
                    break;
                case SwipingHandler.SwipeDirection.Right:
                    lastSwipeDirection = "Right";
                    break;
                case SwipingHandler.SwipeDirection.Left:
                    lastSwipeDirection = "Left";
                    break;
        }
        
    }
    
    void UserSwiped(SwipingHandler.SwipeDirection direction)
    {
        if (direction == SwipingHandler.SwipeDirection.Up)
        {
            // open lid
            currentSauce.OpenLid();
        }
        else if (direction == SwipingHandler.SwipeDirection.Down)
        {
            // dunk that shit
            if (!currentSauce.IsOpen)
            {
                GotItWrong();
            }
            else 
            {
                currentSauce.DunkTheNug();
                if (currentSauce.IsSzechuan)
                {
                    GotItRight();
                }
                else
                {
                    GotItWrong();
                }
            }
        }
        else if (direction == SwipingHandler.SwipeDirection.Left || direction == SwipingHandler.SwipeDirection.Right)
        {
            // Get the shit away from me
            if (currentSauce.IsSzechuan)
            {
                GotItWrong();
            }
            else
            {
                TimeToChoose -= NextTimeItsShorter;
                GotItRight();
            }
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
        
        currentSauce = Instantiate(currentSauce.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Sauce>();
    }

    void GotItRight()
    {
        score++;
        StopCoroutine(failTimer);
        Destroy(currentSauce.gameObject);
        GiveNewSauce(TimeToChoose);
    }

    void GotItWrong()
    {
        //game over
        if(score > highScore)
        {
            highScore = score;
        }
        print("YOU LOSE. Highscore " + highScore + ". Your score " + score);

        StopCoroutine(failTimer);
        Destroy(currentSauce.gameObject);

        TimeToChoose = initialTimeToChoose;
        
        score = 0;
        GiveNewSauce(TimeToChoose);
    }

    
    IEnumerator MyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GotItWrong();
    }
}
