using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {

    public Vector3 somewhere;
	// Use this for initialization
	void Start () {
        somewhere.x = Random.Range(-100, 100);
        somewhere.y = Random.Range(-100, 100);
        somewhere.z = Random.Range(-100, 100);

        GetComponent<Rigidbody>().AddForce(somewhere*100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
