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
	
	public void Init()
	{
		IsOpen = false;
		NugDunked = false;
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
		StartCoroutine(Wait(AfterDunk));
	}

	public Vector3 force;
	private void FlipThatShit()
	{
		LidRigidbody.isKinematic = false;
		Vector3 localForcePos = new Vector3(1.609f, 0, -1f);
		Vector3 worldForcePos = transform.TransformPoint(localForcePos);
		LidRigidbody.AddForceAtPosition(new Vector3(0, 0, 1000), worldForcePos);
	}

	private void FuckingDunkTheActualNugSoHard()
	{
		
	}

	IEnumerator Wait(Action AfterDunk)
	{
		yield return new WaitForSeconds(1f);
		AfterDunk();
	}
	
}
