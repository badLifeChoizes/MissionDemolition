using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	static public GameObject POI;//The static point of interest

	[Header("Set in Inspector")]
	public float easing = 0.05f;
	public Vector2 minXY = Vector2.zero;

	[Header("Set Dynamically")]
	public float camZ;//the Z position of the camera

	void Awake() {
		camZ = this.transform.position.z;
	}

	void FixedUpdate(){

		//--if (POI == null)
		//--	return;

		//--//Get the position of the poi
		//--Vector3 destination = POI.transform.position;

		Vector3 destination;
		//if there is no POI, return ro P[0,0,0]
		if (POI == null) {
			destination = Vector3.zero;
		} else {
			//Get the position of the POI
			destination = POI.transform.position;
			//if POI is a projectile, check to see if its at rest
			if (POI.tag == "Projectile") {
				//if it is sleeping(not moving)
				if (POI.GetComponent<Rigidbody> ().IsSleeping ()) {
					//return to default view
					POI = null;
					//in the next updte
					return;
				}
			}
		}

		//Limit the x & y to minimum values
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);
		//Interpolate from the current Camera position toward destination
		destination = Vector3.Lerp(transform.position, destination, easing);
		//Force destination.z to e camZ to keep the camera far enough away
		destination.z = camZ;
		//set the camera to the destination
		transform.position = destination;
		Camera.main.orthographicSize = destination.y + 10;
	}

}
