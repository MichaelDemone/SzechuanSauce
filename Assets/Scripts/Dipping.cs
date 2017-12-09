using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;




public class Dipping : MonoBehaviour {


    public static int highScore;

    public float TimeToChoose;
    public float NextTimeItsShorter;
    public float minTimeLimit;
    public Sauce[] Sauces;
    public Sauce[] SaucesMid;
    public Sauce[] SaucesFinal;
   // public Sauce[] NoneOfTheGoodStuff;


    public Text StatsText;
    public GameObject button;
    public GameObject cameraObject;
    public float ShakeIntensity;
    public float ShakeDuration;
    private float initialTimeToChoose;
    
    private int score;
    public Sauce currentSauce;
   // public Sauce lastSauce;
    private Sauce szechuanSauce;

    public SwipingHandler SwipingHandler;

    public Transform SpawnPoint1, SpawnPoint2;

    public float ForceMultiplier;
    public Vector2 MaxForce = new Vector2(100, 100);

    private bool notFailed = true;

    private int spawner = 1;
    
    public bool ShouldScaleWithSwipeLength = true;
    
	// Use this for initialization
	void Start () 
	{
        highScore = PlayerPrefs.GetInt("highscore", 0);

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
    int getHighscore() {
        return highScore;
    }
    void UserTapped()
    {
        if (notFailed) {
            if (currentSauce.IsSzechuan) {
                //currentSauce.BABY_YOU_TURN_ME_ON();
                GotItRight();
            } else {
                GotItWrong();
            }
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

        //currentSauce.GetComponent<AudioSource>().Play();
        if (notFailed) {

            if (currentSauce.IsSzechuan) {
                GotItWrong();
            } else {
                GotItRight();
                //currentSauce.BABY_YOU_TURN_ME_ON();
                SoundHandler.playSound("SwiperNoSwiping");
                
            }
        }
    }
    
    IEnumerator failTimer;

    void GiveNewSauce(float time)
    {
        failTimer = MyCoroutine(time);
        StartCoroutine(failTimer);

        //lastSauce=currentSauce;

         if (score<=50) {
             int randVal = Random.Range(0, Sauces.Length - 1);
             currentSauce = Sauces[randVal];
         } else if (score>50 && score <= 100) {
             int randVal = Random.Range(0, SaucesMid.Length - 1);
             currentSauce = SaucesMid[randVal];
         } else {
             int randVal = Random.Range(0, SaucesFinal.Length - 1);
             currentSauce = SaucesFinal[randVal];
         }

        //int randVal = Random.Range(0, NoneOfTheGoodStuff.Length - 1);
        //currentSauce = NoneOfTheGoodStuff[randVal];

        currentSauce.Init();
       // if(lastSauce!= null)
       // Physics.IgnoreCollision(currentSauce.GetComponent<BoxCollider>(), lastSauce.GetComponent<BoxCollider>());

        Vector3 rotation;
        rotation.x = 90;
        rotation.y = 0;
        rotation.z = -90;

        //if (spawner == 1) {
            currentSauce = Instantiate(currentSauce.gameObject, SpawnPoint1.position, Quaternion.identity).GetComponent<Sauce>();
         //   spawner = 2;
       // } else {
        //    currentSauce = Instantiate(currentSauce.gameObject, SpawnPoint2.position, Quaternion.identity).GetComponent<Sauce>();
       //     spawner = 1;
      //  }

        currentSauce.transform.Rotate(rotation);
    }

    void GotItRight()
    {
        if (TimeToChoose > minTimeLimit) {
            TimeToChoose -= NextTimeItsShorter;
        }


        if (currentSauce.IsSzechuan)
        {
            score++;
            StopCoroutine(failTimer);
            cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
            currentSauce.playSound();
            //currentSauce.BABY_YOU_TURN_ME_ON();
            //currentSauce.GetComponent<AudioSource>().Play();
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

    public static void resetScore() {
        highScore = 0;
    }

    void GotItWrong()
    {
        //game over
        notFailed = false;
        SoundHandler.playSound("Fail");
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
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
