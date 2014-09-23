using UnityEngine;
using System.Collections;

public class IPadButtonZoom : MonoBehaviour {
	
	void Awake () {
		if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 && this.tag != "Red" && this.tag != "Blue"){
			transform.position = new Vector3 (13f, 14f, transform.position.z);
		}
		else if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 && this.tag == "Red"){
			transform.position = new Vector3 (-12.5f, -4f, transform.position.z);
		}
		else if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 && this.tag == "Blue" ){
			transform.position = new Vector3 (12.5f, -4f, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && this.tag != "Red" && this.tag != "Blue") {
			transform.position = new Vector3 (transform.position.x - 1.5f, transform.position.y - 1f, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && this.tag == "Red") {
			transform.position = new Vector3 (transform.position.x + 3.5f, transform.position.y, transform.position.z);
		}
		else if ((iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen) && this.tag == "Blue") {
			transform.position = new Vector3 (transform.position.x - 3.5f, transform.position.y, transform.position.z);
		}
	}
}
