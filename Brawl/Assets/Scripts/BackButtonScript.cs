using UnityEngine;
using System.Collections;

public class BackButtonScript : MonoBehaviour {
	
	public Sprite upButton;
	public Sprite downButton;
	
	public SpriteRenderer renderer;

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
				if (touch[i].position.x > (Screen.width / 4) * 3 && touch[i].position.y > (Screen.height / 4) * 3) {
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
		deleteAll();
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
		deleteAll();
	}

	void deleteAll () {
		if (this.tag != "Back") {
			Destroy(GameObject.Find("Music"));
			Destroy(GameObject.Find("SceneSaver"));
			Destroy(GameObject.Find("ButtonR"));
			Destroy(GameObject.Find("ButtonB"));
			Destroy(GameObject.Find("ScoreR"));
			Destroy(GameObject.Find("ScoreB"));
			Destroy(GameObject.Find("WinText"));
			Destroy(GameObject.Find("Score"));
			Destroy(GameObject.FindWithTag("Ready"));
			
			Invoke("LoadLevel", 0.07f);
		}
		else if (this.tag == "Back") {
			if (camPan.shouldPanDown) {
				StopCoroutine(camPan.PanDown());
				camPan.shouldPanDown = false;
			}

			GameObject.Find("MusicText").GetComponent<GUIText>().enabled = false;
			GameObject.Find("FXText").GetComponent<GUIText>().enabled = false;
			GameObject.Find("PointsText").GetComponent<GUIText>().enabled = false;

			StartCoroutine(camPan.PanCenter());
		}
	}

	void LoadLevel () {
		Application.LoadLevel(0);
	}
}

