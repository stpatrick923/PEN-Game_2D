using UnityEngine;
using System.Collections;

public class Proton : Ball {

	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		setBeingDragged (true);

		screenPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		screenPoint.z = gameObject.transform.position.z;

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseUp()
	{
		setBeingDragged (false);
	}

	void OnMouseDrag()
	{
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + offset;

		if (this.transform.parent != null) 
		{
			this.transform.parent.position = currentPosition;
		} 

		else 
		{
			gameObject.transform.position = currentPosition;
		}
	}


	void OnCollisionEnter(Collision collision)
	{
		sendToConsole ("Collision Entered");
		if (this.isBeingDragged ()) 
		{
			sendToConsole("Being dragged");
			GameObject otherObject = collision.gameObject;
			combineObjects (otherObject);
		}
	}

	void combineObjects(GameObject otherObject)
	{
		sendToConsole ("Combining objects");
		//want: an empty parent object that will move and take the children objects with it

		//if the other object already has a parent, combine all the other objects in that group with this one.
		//otherwise, attach this object to that parent
		if (gameObject.transform.parent != null) 
		{
			sendToConsole ("This gameObject's parent is not null");
			if (otherObject.transform.parent != null)
			{
				sendToConsole ("The other object's parent is not null");
				otherObject.transform.SetParent(gameObject.transform.parent);
			}
		}
		else
		{
			sendToConsole("This gameObject's parent is null");
			if (otherObject.transform.parent != null)
			{
				sendToConsole("The other gameObject's parent is not null");
				gameObject.transform.parent = otherObject.transform.parent;
			}
			else
			{
				sendToConsole("Neither gameObject has a parent");
				GameObject emptyParent = new GameObject();
				emptyParent.transform.position = gameObject.transform.position;
				gameObject.transform.parent = emptyParent.transform;

				if (gameObject.transform.parent != null)
				{
					sendToConsole("This object now has a parent.");
				}
				otherObject.transform.parent = emptyParent.transform;
			}
		}
	}

}

