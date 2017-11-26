using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("musicOn", 1) == 1)
            GetComponent<UnityEngine.UI.Toggle>().isOn = true;
        else
            GetComponent<UnityEngine.UI.Toggle>().isOn = false;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
