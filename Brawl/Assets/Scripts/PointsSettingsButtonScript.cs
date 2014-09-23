using UnityEngine;
using System.Collections;

public class PointsSettingsButtonScript : MonoBehaviour {

	public Sprite upPoints1;
	public Sprite downPoints1;
	public Sprite upPoints2;
	public Sprite downPoints2;
	public Sprite upPoints3;
	public Sprite downPoints3;

	public SpriteRenderer renderer;
	
	public AudioClip buttonSound;
	
	void Start () {
		if (!PlayerPrefs.HasKey("SetPoints"))
			PlayerPrefs.SetInt("SetPoints", 3);
		
		if (renderer.sprite == null) {
			if (PlayerPrefs.GetInt("SetPoints") == 3)
				renderer.sprite = upPoints1;
			else if (PlayerPrefs.GetInt("SetPoints") == 5)
				renderer.sprite = upPoints2;
			else if (PlayerPrefs.GetInt("SetPoints") == 10)
				renderer.sprite = upPoints3;
			else
				renderer.sprite = upPoints1;
		}
	}
	
	void Update () {
		
		#if UNITY_IOS
		
		if (Input.touchCount > 0) {
			Touch[] touch = Input.touches;
			
			for (int i = 0; i < Input.touchCount; i++) {
				if (touch[i].position.y > this.transform.position.y - 7 && touch[i].position.y < this.transform.position.y + 7) {
					if (touch[i].phase == TouchPhase.Began) OnTouchDown();
					if (touch[i].phase == TouchPhase.Stationary) OnTouchStay();
					if (touch[i].phase == TouchPhase.Ended || touch[i].phase == TouchPhase.Canceled) OnTouchUp();
				}
			}
		}
		
		#endif
		
	}
	
	void OnTouchDown () {
		if (renderer.sprite == upPoints1)
			renderer.sprite = downPoints1;
		else if (renderer.sprite == upPoints2)
			renderer.sprite = downPoints2;
		else if (renderer.sprite == upPoints3)
			renderer.sprite = downPoints3;
		
		audio.PlayOneShot(buttonSound);
	}
	
	void OnTouchUp () {
		if (renderer.sprite == downPoints1) {
			renderer.sprite = upPoints2;
			
			PlayerPrefs.SetInt("SetPoints", 5);
		}
		else if (renderer.sprite == downPoints2) {
			renderer.sprite = upPoints3;
			
			PlayerPrefs.SetInt("SetPoints", 10);
		}
		else if (renderer.sprite == downPoints3) {
			renderer.sprite = upPoints1;
			
			PlayerPrefs.SetInt("SetPoints", 3);
		}
		
		PlayerPrefs.Save();
	}
	
	void OnTouchStay () {
		if (renderer.sprite == upPoints1)
			renderer.sprite = downPoints1;
		else if (renderer.sprite == upPoints2)
			renderer.sprite = downPoints2;
		else if (renderer.sprite == upPoints3)
			renderer.sprite = downPoints3;
	}
	
	void OnMouseDown () {
		if (renderer.sprite == upPoints1)
			renderer.sprite = downPoints1;
		else if (renderer.sprite == upPoints2)
			renderer.sprite = downPoints2;
		else if (renderer.sprite == upPoints3)
			renderer.sprite = downPoints3;
		
		audio.PlayOneShot(buttonSound);
	}
	
	void OnMouseUp () {
		if (renderer.sprite == downPoints1) {
			renderer.sprite = upPoints2;

			PlayerPrefs.SetInt("SetPoints", 5);
		}
		else if (renderer.sprite == downPoints2) {
			renderer.sprite = upPoints3;

			PlayerPrefs.SetInt("SetPoints", 10);
		}
		else if (renderer.sprite == downPoints3) {
			renderer.sprite = upPoints1;

			PlayerPrefs.SetInt("SetPoints", 3);
		}
		
		PlayerPrefs.Save();
	}
}
