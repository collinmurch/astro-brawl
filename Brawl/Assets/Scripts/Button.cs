using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public int jumpForce;

	public Sprite upButton;
	public Sprite downButton;
	public Sprite upButton2;
	public Sprite downButton2;
	
	public SpriteRenderer renderer;
	
	public GameObject playerR;
	public GameObject playerB;

	Animator anim;

	CharacterControllerScript charControlR;
	CharacterControllerScript charControlB;

	private GameObject sceneSaver;
	private SceneSaver sceneSaverScript;

	public bool readyR;
	public bool readyB;

	public bool canGo;

	void Start () {
		anim = GetComponent<Animator>();

		sceneSaver = GameObject.Find("SceneSaver");
		sceneSaverScript = sceneSaver.GetComponent<SceneSaver>();

		if (this.tag == "Blue") anim = playerB.GetComponent<Animator>();
		else if (this.tag == "Red") anim = playerR.GetComponent<Animator>();

		charControlB = playerB.GetComponent<CharacterControllerScript>();
		charControlR = playerR.GetComponent<CharacterControllerScript>();

		readyB = false;
		readyR = false;

		canGo = false;

		if (this.tag == "Red" && sceneSaverScript.player == 1)
			Destroy(this.gameObject);

#if UNITY_EDITOR
		
		if (renderer.sprite == null) {
			renderer.sprite = upButton2;
		}
		
#endif
		
		if (renderer.sprite == null) {
			renderer.sprite = upButton;
		}
	}
	
	void Update () {
		if (this.tag == "Blue") {
			if (Input.GetKeyDown(KeyCode.I)) {
				renderer.sprite = downButton2;

				if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo)) {
					charControlB.Jump(jumpForce);
					Shoot();
				}
				readyB = true;
			}
			if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo))
				if (Input.GetKeyUp(KeyCode.I)) renderer.sprite = upButton2;
		}

		else if (this.tag == "Red") {
			if (Input.GetKeyDown(KeyCode.W)) {
				renderer.sprite = downButton2;

				if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo)) {
					charControlR.Jump(jumpForce);
					Shoot();
				}
				readyR = true;
			}
			if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo))
				if (Input.GetKeyUp(KeyCode.W)) renderer.sprite = upButton2;
		}

#if UNITY_IOS

		if (Input.touchCount > 0) {
			Touch[] touch = Input.touches;

			for (int i = 0; i < Input.touchCount; i++) {
				if (touch[i].position.x > Screen.width / 2 && touch[i].position.y < (Screen.height / 4) * 3 && this.tag == "Blue") {
					if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo)) {
						if (touch[i].phase == TouchPhase.Began) OnTouchDown();
						if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
						if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
					}
					if ((!readyR && !readyB) || (!readyB && sceneSaverScript.player == 1 && canGo)) {
						if (touch[i].phase == TouchPhase.Began) renderer.sprite = downButton;
						if (touch[i].phase == TouchPhase.Stationary) renderer.sprite = downButton;
						if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
					}
					readyB = true;
					}
				else if (touch[i].position.x < Screen.width / 2 && touch[i].position.y < (Screen.height / 4) * 3 && this.tag == "Red") {
					if ((readyR && readyB) || (readyB && sceneSaverScript.player == 1 && canGo)) {
						if (touch[i].phase == TouchPhase.Began) OnTouchDown();
						if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
						if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
					}
					if ((!readyR && !readyB) || (!readyB && sceneSaverScript.player == 1 && canGo)) {
						if (touch[i].phase == TouchPhase.Began) renderer.sprite = downButton;
						if (touch[i].phase == TouchPhase.Stationary) renderer.sprite = downButton;
						if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
					}
					readyR = true;
				}
			}
		}

#endif

	}
	
	void OnTouchDown () {
		renderer.sprite = downButton;

		if (this.tag == "Blue") charControlB.Jump(jumpForce);
		else if (this.tag == "Red")charControlR.Jump(jumpForce);
		Shoot();
	}
	
	public void OnTouchUp () {

#if UNITY_IOS

		renderer.sprite = upButton;

#endif

	}
	
	void OnTouchStay () {
		renderer.sprite = downButton;
	}

	void Shoot () {
		anim.SetTrigger ("Shoot");
		if (this.tag == "Blue") charControlB.ShootB();
		else if (this.tag == "Red") charControlR.ShootR();
	}	

	public void upButtonMouse () {

#if UNITY_EDITOR

		renderer.sprite = upButton2;

#endif

	}
}

