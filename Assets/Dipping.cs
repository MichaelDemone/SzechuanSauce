using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dipping : MonoBehaviour {
    public float timeToChoose;
    public float nextTimeItsShorter;
    private int score;
    private int highScore;
    private GameObject[] sauces;
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
        while(readyForSauce == true && timesUp == false)
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
        Instantiate(sauces[randVal], Vector3.zero, Quaternion.identity);

        while (timesUp == false)
        {
            if (sauces[randVal] = sauces[0]) // if szechuan
            {
                if (/*player opens and dips*/)
                {
                    gotItRight();
                    return;
                }
                else { gotItWrong(); }


            }
            else if (sauces[randVal] != sauces[0]) // other sauce
            {
                if (/*player swipes right or left*/)
                {
                    gotItRight();
                    return;
                }
                else { gotItWrong(); }
            }
        }
    
    }

    void gotItRight()
    {
        score++;
        StopCoroutine(failTimer);
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
