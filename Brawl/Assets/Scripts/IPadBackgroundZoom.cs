using UnityEngine;
using System.Collections;

public class IPadBackgroundZoom : MonoBehaviour {
	
	void Update () {
		if ((iPhone.generation.ToString()).IndexOf("iPad") > -1 || iPhone.generation == iPhoneGeneration.iPhone || iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 || iPhone.generation == iPhoneGeneration.iPhone4S || iPhone.generation == iPhoneGeneration.iPodTouch1Gen || iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen || iPhone.generation == iPhoneGeneration.iPodTouch4Gen){
			transform.localScale = new Vector3 (1.5f, 1f, 1f);
		}
	}
}