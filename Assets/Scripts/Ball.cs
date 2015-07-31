using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	protected bool beingDragged = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setBeingDragged(bool dragState)
	{
		beingDragged = dragState;
	}

	public bool isBeingDragged()
	{
		return beingDragged;
	}

	protected void sendToConsole(string message)
	{
		print (message);
	}
}
