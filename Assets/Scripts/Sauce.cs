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
	
	public void Init()
	{
		IsOpen = false;
		NugDunked = false;
	}
	
	
	public void OpenLid()
	{
		IsOpen = true;
	}

	public void DunkTheNug()
	{
		NugDunked = true;
	}
	
}
