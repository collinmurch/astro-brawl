using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public int jumpForce;

	public Transform playerFinder1;
	public Transform playerFinder2;
	public LayerMask playerBStuff;

	public GameObject otherPlayer;
	
	private CharacterControllerScript selfControllerScript;

	private bool isGoing;

	void Awake () {
		selfControllerScript = GetComponent<CharacterControllerScript>();
		isGoing = false;
	}

	void Update () {
		if (((transform.position.x < otherPlayer.transform.position.x && selfControllerScript.hinge.motor.motorSpeed > 0) || (transform.position.x > otherPlayer.transform.position.x && selfControllerScript.hinge.motor.motorSpeed < 0)) && !isGoing) {
			isGoing = true;

			StartCoroutine(waitThenGo (Random.value + 0.75f));
			StartCoroutine(waitThenGo ((Random.value + 0.25f) * 2.5f));
		}
	}

	IEnumerator waitThenGo (float seconds) {
		yield return new WaitForSeconds(0.65f);

		if (Physics2D.OverlapArea(playerFinder2.position, playerFinder1.position, playerBStuff) && !isGoing) {
			selfControllerScript.Jump(jumpForce);
			selfControllerScript.ShootR();
		} else {
			yield return new WaitForSeconds(seconds);
			selfControllerScript.Jump(jumpForce);
			selfControllerScript.ShootR();
		}

		isGoing = false;
	}
}
