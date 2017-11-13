using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    GameObject tracking;
    public void SetObjectToTrack(GameObject go) {
        tracking = go;
    }

    // Update is called once per frame
    void Update() {
        if (tracking == null) return;
        transform.position = tracking.transform.position + new Vector3(0, 0.5f, 0);
    }
}
