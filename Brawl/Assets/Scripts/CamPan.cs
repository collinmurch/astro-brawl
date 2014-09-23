using UnityEngine;
using System.Collections;

public class CamPan : MonoBehaviour {
	
	public float panSpeed;
	public float pantime;

	public Transform targetCenter;
	public Transform targetDown;

	public bool shouldPanDown;
	public bool shouldPanCenter;

	public bool isDown;

	void Start () {
		shouldPanDown = false;
		shouldPanCenter = false;

		Destroy(GameObject.Find("ReadyText"));
		GameObject.Find("BGImage").GetComponent<Animator>().SetTrigger("StartGame");
	}

	void Update () {
		if (shouldPanDown)
			transform.position = Vector3.Lerp(this.transform.position, targetDown.position, Time.deltaTime * panSpeed);
		if (shouldPanCenter)
			transform.position = Vector3.Lerp(this.transform.position, targetCenter.position, Time.deltaTime * panSpeed);
	}

	public IEnumerator PanDown () {
		isDown = true;

		shouldPanDown = true;
		yield return new WaitForSeconds(pantime);
		shouldPanDown = false;
	}

	public IEnumerator PanCenter () {
		shouldPanCenter = true;
		yield return new WaitForSeconds(pantime);
		shouldPanCenter = false;

		isDown = false;
	}
}
