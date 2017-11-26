using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsSzechuan;

	
	public bool IsOpen = false;
	public bool NugDunked = false;

	public Rigidbody LidRigidbody;
    public GameObject Packet;
	
	public GameObject THANUG;
	public float DUNKSPEEEEED;
	
	public ParticleSystem Sawce;
	
	public void Init()
	{
		IsOpen = false;
		NugDunked = false;
	}
	
    public void playSound() {
        if (IsSzechuan) {
            SoundHandler.playSound("Dunk");
        } else {
            SoundHandler.playSound("SwiperNoSwiping");
        }
    }
	
	public void OpenLid()
	{
		IsOpen = true;
	}

	public void DunkTheNug(Action AfterDunk)
	{
		NugDunked = true;
		// here's the magic.
		FlipThatShit();
		FuckingDunkTheActualNugSoHard();
		StartCoroutine(Wait(0.25f, MakeAFuckingSauceBukkake));
		StartCoroutine(Wait(0.25f, () =>
		{
			AfterDunk();
			Destroy(this.gameObject);
		}));
	}

	public Vector3 force;
	private void FlipThatShit()
	{
		if (LidRigidbody == null) return;
		LidRigidbody.isKinematic = false;
		Vector3 localForcePos = new Vector3(1.609f, 0, -1f);
		Vector3 worldForcePos = transform.TransformPoint(localForcePos);
		LidRigidbody.AddForceAtPosition(new Vector3(0, 0, 1000), worldForcePos);
	}

	private void FuckingDunkTheActualNugSoHard()
	{
		StartCoroutine(DUNKTHATNUG());
	}

	IEnumerator DUNKTHATNUG()
	{
		while (THANUG.transform.position.z < 20)
		{
			var pos = THANUG.transform.localPosition;
			pos.z += DUNKSPEEEEED * Time.deltaTime;
			THANUG.transform.localPosition = pos;
			yield return null;
		}
	}
	
	private void MakeAFuckingSauceBukkake()
	{
		if (Sawce != null)
		{
            GameObject system = GameObject.Instantiate(Sawce.gameObject);
            system.GetComponent<Tracker>().SetObjectToTrack(gameObject);
			var emission = system.GetComponent<ParticleSystem>().emission;
			emission.enabled = true;
            system.GetComponent<ParticleSystem>().Emit(20);
			emission.enabled = false;
		}

	}
	
	IEnumerator Wait(float time, Action AfterAction)
	{
		yield return new WaitForSeconds(time);
		AfterAction();
	}
	
}
