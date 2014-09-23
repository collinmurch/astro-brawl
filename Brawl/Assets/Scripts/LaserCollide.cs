using UnityEngine;
using System.Collections;

public class LaserCollide : MonoBehaviour {

	public Transform hitCheck;

	public LayerMask playerRHit;
	public LayerMask playerBHit;
	public LayerMask platforms;

	private float hitRadius = 0.4f;

	public GameObject playerR;
	public GameObject playerB;

	public GameObject score;
	private ScoreScript scoreScript;

	void Start () {
		playerR = GameObject.Find("PlayerR");
		playerB = GameObject.Find("PlayerB");

		score = GameObject.Find("Score");
		scoreScript = score.GetComponent<ScoreScript>();
	}

	void Update () {
		if (Physics2D.OverlapCircle(hitCheck.position, hitRadius, playerRHit) && this.tag != "LaserR") {
			playerR.GetComponent<CharacterControllerScript>().Respawn();
			scoreScript.addScoreB();
			Destroy(gameObject);
		}

		if (Physics2D.OverlapCircle(hitCheck.position, hitRadius, playerBHit) && this.tag != "LaserB") {
			playerB.GetComponent<CharacterControllerScript>().Respawn();
			scoreScript.addScoreR();
			Destroy(gameObject);
		}

		else if (Physics2D.OverlapCircle(hitCheck.position, hitRadius, platforms)) {
			Destroy(gameObject);
		}
	}
}
