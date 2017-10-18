using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dipping : MonoBehaviour {
    public float timeToChoose;
    public float nextTimeItsShorter;
    private int score;
    private int highScore;
    private GameObject[] sauces;
    private GameObject currentSauce;
    private bool readyForSauce;
    private bool timesUp = false;

	// Use this for initialization
	void Start () 
	{
        score = 0;
        readyForSauce = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
        while(readyForSauce == true)
        {
            timeToChoose -= nextTimeItsShorter;
            newSauce(timeToChoose);
        }
        
    }
    IEnumerator failTimer;

    void newSauce(float time)
    {
        failTimer = MyCoroutine(time);
        StartCoroutine(failTimer);
        readyForSauce = false;
        int randVal = Random.Range(0, sauces.Length - 1);
        currentSauce = sauces[randVal];
        Instantiate(currentSauce, Vector3.zero, Quaternion.identity);

        while (timesUp == false)
        {
            if (sauces[randVal] = sauces[0]) // if szechuan
            {
                if (/*player opens and dips*/)
                {
                    gotItRight();
                    return;
                }             
            }
            else if (sauces[randVal] != sauces[0]) // other sauce
            {
                if (/*player swipes right or left*/)
                {
                    gotItRight();
                    return;
                }
            }
        }
        gotItWrong();
    
    }

    void gotItRight()
    {
        score++;
        StopCoroutine(failTimer);
        Destroy(currentSauce);
        readyForSauce = true;
    }

    void gotItWrong()
    {
        //game over
        if(score > highScore)
        {
            highScore = score;
        }
    }

    IEnumerator MyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timesUp = true;
    }
}
