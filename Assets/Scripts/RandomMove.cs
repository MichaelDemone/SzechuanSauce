using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {

    public Vector3 Somewhere;
	public float Force;
	
	// Use this for initialization
	void Start () {
        Somewhere.x = Random.Range(-Force, Force);
        Somewhere.y = Random.Range(-Force, Force);
        Somewhere.z = Random.Range(-Force, Force);

        GetComponent<Rigidbody>().AddForce(Somewhere);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
