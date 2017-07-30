using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private GameObject currentArrow;
	public GameObject arrowPrefab;
	public GameObject stringAttachPoint;
	public GameObject arrowStartPoint;
	public GameObject stringStartPoint;
	private bool isAttached = false;

	public static ArrowManager Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		AttachArrow ();
		PullString ();

	}

	private void PullString() 
	{
		if (isAttached) 
		{
			float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;
			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition  + new Vector3 (5f* dist, 0f, 0f);

			var device = SteamVR_Controller.Input((int)trackedObj.index);
			//GetTouchUp to signal we have released the controller
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) 
			{
				Fire ();
			}
		}
	}
		
	private void Fire() 
	{
		float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;

		currentArrow.transform.parent = null;
		currentArrow.GetComponent<Arrow> ().Fired ();

		Rigidbody r = currentArrow.GetComponent<Rigidbody> ();
		r.velocity = currentArrow.transform.forward * 25f * dist;
		r.useGravity = true;

		currentArrow.GetComponent<Collider> ().isTrigger = false;

		stringAttachPoint.transform.position = stringStartPoint.transform.position;

		currentArrow = null;
		isAttached = false;
	}

	private void AttachArrow() 
	{
		if (currentArrow == null) 
		{
			currentArrow = Instantiate (arrowPrefab);
			currentArrow.transform.parent = trackedObj.transform;
			//take whatever we just created and make it the child of our
			//tracked object
			currentArrow.transform.localPosition = new Vector3 (0f, 0f, .342f);
			currentArrow.transform.localRotation = Quaternion.identity;
		}
	}


	public void AttachBowToArrow() 
	{
		currentArrow.transform.parent = stringAttachPoint.transform;
		//set the position of the attached arrow to the arrow start point set up earlier
		currentArrow.transform.position = arrowStartPoint.transform.position;
		//make sure NOT to set to localRotation, as the arrow will be parallel with the string
		currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

		isAttached = true;
	}
		
}
