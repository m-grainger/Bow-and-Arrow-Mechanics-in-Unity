using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private bool isAttached = false;
	private bool isFired = false;

	void OnTriggerStay()
	{
		//During the event
		AttachArrow ();
	}
	void OnTriggerEnter()
	{
		//When the event happens
		AttachArrow ();
	}

	void Update()
	{
		if (isFired && transform.GetComponent<Rigidbody> ().velocity.magnitude > 5f) 
		{
			transform.LookAt (transform.position + transform.GetComponent<Rigidbody> ().velocity);
		}
	}

	public void Fired()
	{
		isFired = true;
	}
		

	 private void AttachArrow()
	{
	//code from SteamVR preloaded scripts
		var device = SteamVR_Controller.Input ((int)ArrowManager.Instance.trackedObj.index);
		if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			ArrowManager.Instance.AttachBowToArrow ();
			isAttached = true;

		}
	}
}

