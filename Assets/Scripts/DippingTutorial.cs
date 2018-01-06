using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;




public class DippingTutorial : MonoBehaviour {


    public static int highScore;
    private int count = 0;

    public float TimeToChoose;
    public float NextTimeItsShorter;
    public float minTimeLimit;

 //   public static bool menuActive = false;

    public Sauce[] Sauces;
    public Sauce[] NoneOfTheGoodStuff;


    public Text StatsText;
    public GameObject SwipeText;
    public GameObject TapText;
    public GameObject button;
    public GameObject button2;
    public GameObject TryAgainButton;
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
    void Start() {
        //highscore = playerprefs.getint("highscore", 0);

        //score = 0;
        SwipingHandler.UserSwiped += UserSwiped;
        SwipingHandler.UserTapped += UserTapped;

        //initialtimetochoose = timetochoose;

        GiveNewSauce(TimeToChoose);

    }

    void Update() {
        //StatsText.text = "Time: " + TimeToChoose;
        //StatsText.text = "\nScore: " + score;
        //StatsText.text += "\nHighscore: " + highScore;
        //StatsText.text += "\nCurrent sauce: " + currentSauce.name;
        //StatsText.text += "\nLast swipe: " + lastSwipeDirection;
    }
    int getHighscore() {
        return highScore;
    }
    void UserTapped() {
      //  if (!menuActive) {
            if (notFailed) {
                if (currentSauce.IsSzechuan) {
                    //currentSauce.BABY_YOU_TURN_ME_ON();
                    GotItRight();
                } else {
                    GotItWrong();
                }
            }
       // }
    }

    private Vector3 forceDir = new Vector3();
    void UserSwiped(Vector2 direction) {
      //  if (!menuActive) {

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
        //}
    }

    IEnumerator failTimer;

    void GiveNewSauce(float time) {
        if (count < 5) {
            int randVal = Random.Range(0, NoneOfTheGoodStuff.Length - 1);
            currentSauce = NoneOfTheGoodStuff[randVal];
        } else {
            int randVal = Random.Range(0, Sauces.Length - 1);
            currentSauce = Sauces[randVal];

        }





        currentSauce.Init();

        if (currentSauce.IsSzechuan) {
            SwipeText.SetActive(false);
            TapText.SetActive(true);
        }

        Vector3 rotation;
        rotation.x = 90;
        rotation.y = 0;
        rotation.z = -90;
        currentSauce = Instantiate(currentSauce.gameObject, SpawnPoint1.position, Quaternion.identity).GetComponent<Sauce>();
        currentSauce.transform.Rotate(rotation);
    }

    IEnumerator timeDatDUNK;

    void GotItRight() {


        if (currentSauce.IsSzechuan) {


            cameraObject.GetComponent<CameraShake>().ShakeCamera(ShakeIntensity, ShakeDuration);
            currentSauce.playSound();
            currentSauce.DunkTheNug(() => doNothing());
            timeDatDUNK = JustWait(2f);
            StartCoroutine(timeDatDUNK);


        } else { 
        count++;
            print(count);

            GiveNewSauce(TimeToChoose);
        }

            


        

    }

    void doNothing() {

    }

    public static void resetScore() {
        highScore = 0;
    }

    void GotItWrong() {

        if (currentSauce.IsSzechuan) {
            GiveNewSauce(TimeToChoose);
        //    button.SetActive(true);
        //    Text buttonText = button.GetComponentInChildren<Text>();
        //    buttonText.text = "Whoops! Thats a Szechuan \n Sauce! Try tapping on it!";
        //  //  menuActive=true;
        } 
        //else {
        //    button.SetActive(true);
        //    Text buttonText = button.GetComponentInChildren<Text>();
        //    buttonText.text = "Whoops! Thats not a Szechuan \n Sauce! Try swiping it!";
        // //   menuActive=true;
        //}

    }


    IEnumerator MyCoroutine(float seconds) {
        yield return new WaitForSeconds(seconds);
        GotItWrong();
    }

    IEnumerator JustWait(float seconds) {
        yield return new WaitForSeconds(seconds);
        button2.SetActive(true);
        Text buttonText = button2.GetComponentInChildren<Text>();
        buttonText.text = "You have finished the \n tutorial!";
    }

   public void TryAgain() {
        //TryAgainButton.SetActive(false);
        button.SetActive(false);
    }
}
