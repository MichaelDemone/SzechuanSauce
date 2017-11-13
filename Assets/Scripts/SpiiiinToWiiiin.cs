using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiiiinToWiiiin : MonoBehaviour {
    public GameObject nugget;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		nugget.GetComponent<Transform>().Rotate(new Vector3(0, 0, 100*Time.deltaTime));
	}
}
