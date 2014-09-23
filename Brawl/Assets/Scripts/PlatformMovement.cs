using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	public SliderJoint2D joint;

	public float xTargetPosL;
	public float xTargetPosR;

	void Update () {
		if (transform.localPosition.x <= xTargetPosL)
			joint.angle = -180;
		else if (transform.localPosition.x >= xTargetPosR)
			joint.angle = 0;
	}
}
