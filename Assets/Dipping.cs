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

    void newSauce(float time)
    {
        readyForSauce = false;
        int randVal = Random.Range(0, sauces.Length - 1);
        Instantiate(sauces[randVal], Vector3.zero, Quaternion.identity);

        if (sauces[randVal] = sauces[0]) // if szechuan
        {
            //check if player opens and dips
            //give points
            score+=5;
        }
        else
        {
            // check if player swipes right or left
            score++;
        }
    }

    void gotItRight()
    {

    }

    void gotItWrong()
    {
        //game over
        if(score > highScore)
        {
            highScore = score;
        }
    }
}
