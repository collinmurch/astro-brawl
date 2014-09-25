using UnityEngine;
using System.Collections;

public class IPadScoreZoom : MonoBehaviour {

	void Awake () {
		if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 && this.tag == "Score"){
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && this.tag == "Score") {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && transform.position.x > 0.5f) {
			transform.position = new Vector3 (transform.position.x + 0.01f, transform.position.y, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && transform.position.x < 0.5f) {
			transform.position = new Vector3 (transform.position.x - 0.01f, transform.position.y, transform.position.z);
		}
	}
}
