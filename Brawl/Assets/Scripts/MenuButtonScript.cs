using UnityEngine;
using System.Collections;

public class MenuButtonScript: MonoBehaviour {
	
	public Sprite upButton;
	public Sprite downButton;
	
	public SpriteRenderer renderer;
	
	public SceneSaver sceneSaverScript;
	
	public CamPan camPan;

	public AudioClip buttonSound;

	void Start () {
		if (renderer.sprite == null)
			renderer.sprite = upButton;
	}
	
	void Update () {

		#if UNITY_IOS
		
		if (Input.touchCount > 0) {
			Touch[] touch = Input.touches;
			
			for (int i = 0; i < Input.touchCount; i++) {
				if (touch[i].position.x < Screen.width / 3 && this.tag == "1P" && !camPan.isDown) {
					if (touch[i].phase == TouchPhase.Began) OnTouchDown();
					if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
					if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
				}
				else if (touch[i].position.x > (Screen.width / 3) * 2 && this.tag == "2P" && !camPan.isDown) {
					if (touch[i].phase == TouchPhase.Began) OnTouchDown();
					if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
					if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
				}
				else if (touch[i].position.x < (Screen.width / 3) * 2 && touch[i].position.x > Screen.width / 3 && this.tag == "Settings"  && !camPan.isDown) { 
					if (touch[i].phase == TouchPhase.Began) OnTouchDown();
					if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
					if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
				}
			}
		}
		
		#endif
		
	}
	
	void OnTouchDown () {
		renderer.sprite = downButton;
		audio.PlayOneShot(buttonSound);
	}
	
	void OnTouchUp () {
		renderer.sprite = upButton;

		if (this.tag == "1P")
			sceneSaverScript.player = 1;
		if (this.tag == "2P")
			sceneSaverScript.player = 2;
		if (this.tag == "Settings" && camPan.shouldPanDown == false && camPan.shouldPanCenter == false) {
			GameObject.Find("MusicText").GetComponent<GUIText>().enabled = true;
			GameObject.Find("FXText").GetComponent<GUIText>().enabled = true;
			GameObject.Find("PointsText").GetComponent<GUIText>().enabled = true;

			StartCoroutine(camPan.PanDown());
		}

		if (this.tag != "Settings")
			Invoke("LoadLevel", 0.07f);
	}
	
	void OnTouchStay () {
		renderer.sprite = downButton;
	}

	void OnMouseDown () {
		renderer.sprite = downButton;
		audio.PlayOneShot(buttonSound);
	}

	void OnMouseUp () {
		renderer.sprite = upButton;

		if (this.tag == "1P")
			sceneSaverScript.player = 1;
		if (this.tag == "2P")
			sceneSaverScript.player = 2;
		if (this.tag == "Settings" && camPan.shouldPanDown == false && camPan.shouldPanCenter == false) {
			GameObject.Find("MusicText").GetComponent<GUIText>().enabled = true;
			GameObject.Find("FXText").GetComponent<GUIText>().enabled = true;
			GameObject.Find("PointsText").GetComponent<GUIText>().enabled = true;

			StartCoroutine(camPan.PanDown());
		}

		if (this.tag != "Settings")
			Invoke("LoadLevel", 0.07f);
	}

	void LoadLevel () {
		Application.LoadLevel(Random.Range(1, Application.levelCount));
	}
}

