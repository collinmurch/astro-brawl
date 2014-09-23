using UnityEngine;
using System.Collections;

public class IPadCameraZoom : MonoBehaviour {
	
	void Update () {
		if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 && this.tag == "MainCamera"){
			transform.position = new Vector3 (transform.position.x, transform.position.y, -20f);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && this.tag == "MainCamera") {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -17.5f);
		}
	}
}
