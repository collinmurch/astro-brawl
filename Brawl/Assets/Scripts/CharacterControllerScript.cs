using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public HingeJoint2D hinge;
	public float motorForce = 150f;

	private JointMotor2D motor;
	
	public Transform groundCheck;
	public LayerMask whatIsGround;
	private bool grounded = false;
	private float groundRadius = 0.2f;

	private bool anchorDone = false;

	private float rotateSpeed;

	private int shootJumpR;
	private int shootJumpB;

	public GameObject laserPrefab;
	public GameObject gunR;
	public GameObject gunB;
	public GameObject arrowR;
	public GameObject arrowB;

	public GameObject buttonR;
	public GameObject buttonB;
	private Button buttonScriptR;
	private Button buttonScriptB;

	public GUIText readyText;
	private ReadyScript readyScript;
	private bool isReadyScriptGoing;

	public AudioClip shoot;

	private GameObject[] lasers;

	public GameObject explosionR;
	public GameObject explosionB;

	private GameObject otherPlayer;

	public GameObject score;
	private ScoreScript scoreScript;

	private GameObject sceneSaver;
	private SceneSaver sceneSaverScript;

	private Vector3 respawnPos;

	private int scoreLimit;
	private bool canGround;

	public LayerMask cubes;
	private float rotateRadius = 1f;

	void Start () {
		respawnPos = transform.position;

		rotateSpeed = 450f;

		if (this.tag == "Blue")
			otherPlayer = GameObject.Find("PlayerR");
		else if (this.tag == "Red")
			otherPlayer = GameObject.Find("PlayerB");

		motor = hinge.motor;

		shootJumpB = 0;
		shootJumpR = 0;

		scoreLimit = PlayerPrefs.GetInt("SetPoints");

		buttonScriptR = buttonR.GetComponent<Button>();
		buttonScriptB = buttonB.GetComponent<Button>();

		score = GameObject.Find("Score");
		scoreScript = score.GetComponent<ScoreScript>();

		readyScript = readyText.GetComponent<ReadyScript>();

		isReadyScriptGoing = false;

		sceneSaver = GameObject.Find("SceneSaver");
		sceneSaverScript = sceneSaver.GetComponent<SceneSaver>();

		canGround = true;
	}

	void FixedUpdate () {
		//Ground checking
		if (canGround)
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		//Wobbling
		if(hinge.limitState == JointLimitState2D.UpperLimit) {
			motor.motorSpeed = -motorForce;
			
			hinge.motor = motor;
		} else if (hinge.limitState == JointLimitState2D.LowerLimit) {
			motor.motorSpeed = motorForce;
			
			hinge.motor = motor;
		}

		//Reset wobble
		if (transform.rotation == Quaternion.identity && grounded && !anchorDone) {
			//Anchor
			hinge.enabled = true;
			hinge.anchor = new Vector2 (0 , -0.7f);
			hinge.connectedAnchor = new Vector2 (groundCheck.position.x , groundCheck.position.y);;

			//Arrows
			if (this.tag == "Blue")
				arrowB.GetComponent<SpriteRenderer>().enabled = true;
			else if (this.tag == "Red")
				arrowR.GetComponent<SpriteRenderer>().enabled = true;

			//Reload
			if (this.tag == "Blue") shootJumpB = 0;
			else if (this.tag == "Red") shootJumpR = 0;

			anchorDone = true;

			if (!isReadyScriptGoing && !scoreScript.isFirstTimeLoading) {
				isReadyScriptGoing = true;
				readyScript.startReady();
			}
		}
	}

	void Update () {
		//Rotate
		if (rigidbody2D.velocity == new Vector2 (rigidbody2D.velocity.x, 0) && !anchorDone && transform.rotation != Quaternion.identity && !Physics2D.OverlapCircle(transform.position, rotateRadius, cubes)) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotateSpeed * Time.deltaTime);
		}

		//Reset player
		if (transform.position.y < -12) Respawn();

		//Bullets
		lasers = GameObject.FindGameObjectsWithTag("LaserB");
		for (int i = 0; i < lasers.Length; i++) {
			lasers[i].transform.Translate(Vector3.left * Time.deltaTime * 10);
		}

		//Lasers
		lasers = GameObject.FindGameObjectsWithTag("LaserR");
		for (int i = 0; i < lasers.Length; i++) {
			lasers[i].transform.Translate(Vector3.right * Time.deltaTime * 10);
		}

		//Ready players
		if ((buttonScriptB.readyB && buttonScriptR.readyR && !isReadyScriptGoing) || (buttonScriptB.readyB && !isReadyScriptGoing && sceneSaverScript.player == 1)) {
			isReadyScriptGoing = true;
			readyScript.startReady();
		}
	}

	public void Jump (int jumpForce) {
		if ((this.tag == "Blue" && shootJumpB < 2) || (this.tag == "Red" && shootJumpR < 2)) {
			hinge.enabled = false;

			anchorDone = false;
			grounded = false;
			canGround = false;

			StartCoroutine(WaitToAnchor(0.75f));

			//if ((this.tag == "Blue" && shootJumpB == 1) || (this.tag == "Red" && shootJumpR == 1))
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);

			rigidbody2D.AddForce(Vector3.up * jumpForce);

			if (this.tag == "Blue")
				arrowB.GetComponent<SpriteRenderer>().enabled = false;
			else if (this.tag == "Red")
				arrowR.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void ShootR () {
		if (grounded || shootJumpR < 2) {
			shootJumpR++;

			GameObject clone = Instantiate (laserPrefab, gunR.transform.position, transform.rotation) as GameObject;

			clone.tag = "LaserR";

			audio.PlayOneShot(shoot);
		}
	}

	public void ShootB () {
		if (grounded || shootJumpB < 2) {
			shootJumpB++;

			GameObject clone = Instantiate (laserPrefab, gunB.transform.position, transform.rotation) as GameObject;

			clone.tag = "LaserB";

			audio.PlayOneShot(shoot);
		}
	}

	public void Respawn () {
		if (scoreScript.scoreBInt < scoreLimit && scoreScript.scoreRInt < scoreLimit) {
			hinge.enabled = false;

			sceneSaverScript.isFirstScene = false;

			if (this.tag == "Blue") {
				GameObject explosion = Instantiate(explosionB, transform.position, Quaternion.identity) as GameObject;
				Destroy(explosion, 3f);
			}
			else if (this.tag == "Red") {
				GameObject explosion = Instantiate(explosionR, transform.position, Quaternion.identity) as GameObject;
				Destroy(explosion, 3f);
			}
				
			anchorDone = false;
			grounded = false;
			defaultRespawnPos();
			otherPlayer.GetComponent<CharacterControllerScript>().defaultRespawnPos();

			if (this.tag == "Blue")
				arrowB.GetComponent<SpriteRenderer>().enabled = false;
			else if (this.tag == "Red")
				arrowR.GetComponent<SpriteRenderer>().enabled = false;

			scoreScript.isFirstTimeLoading = false;

			readyText.text = "3";

			sceneSaverScript.hurtPlayBetweenScenes();
			Application.LoadLevel(Random.Range(1, Application.levelCount));
		}
	}	

	public void defaultRespawnPos () {
		transform.position = respawnPos;
		transform.localRotation = Quaternion.identity;
		rigidbody2D.velocity = new Vector2 (0, 0);
	}

	IEnumerator WaitToAnchor (float Time) {
		yield return new WaitForSeconds(Time);
		canGround = true;
	}
}
