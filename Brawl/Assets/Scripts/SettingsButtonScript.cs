using UnityEngine;
using System.Collections;

public class SettingsButtonScript : MonoBehaviour {
	
	public Sprite upButtonOn;
	public Sprite downButtonOn;
	public Sprite upButtonOff;
	public Sprite downButtonOff;
	
	public SpriteRenderer renderer;
	
	public AudioClip buttonSound;
	
	void Start () {
		if (!PlayerPrefs.HasKey("SetMusic") && !PlayerPrefs.HasKey("SetFX")) {
			PlayerPrefs.SetInt("SetFX", 1);
			PlayerPrefs.SetInt("SetMusic", 1);
		}

		if (renderer.sprite == null) {
			if (PlayerPrefs.GetInt("SetMusic") == 1 && this.tag == "SetMusic")
				renderer.sprite = upButtonOn;
			else if (PlayerPrefs.GetInt("SetMusic") == 0 && this.tag == "SetMusic")
				renderer.sprite = upButtonOff;
			else if (PlayerPrefs.GetInt("SetFX") == 1 && this.tag == "SetFX")
				renderer.sprite = upButtonOn;
			else if (PlayerPrefs.GetInt("SetFX") == 0 && this.tag == "SetFX")
				renderer.sprite = upButtonOff;
			else
				renderer.sprite = upButtonOn;
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
		if (renderer.sprite == upButtonOn)
			renderer.sprite = downButtonOn;
		else if (renderer.sprite == upButtonOff)
			renderer.sprite = downButtonOff;

		audio.PlayOneShot(buttonSound);
	}
	
	void OnTouchUp () {
		if (renderer.sprite == downButtonOn) {
			renderer.sprite = upButtonOff;

			if (this.tag == "SetMusic") {
				PlayerPrefs.SetInt("SetMusic", 0);
				GameObject.Find("Music").GetComponent<AudioSource>().enabled = false;
			}
			else if (this.tag == "SetFX") {
				PlayerPrefs.SetInt("SetFX", 0);
				FX (false);
			}
		}
		else if (renderer.sprite == downButtonOff) {
			renderer.sprite = upButtonOn;

			if (this.tag == "SetMusic") {
				PlayerPrefs.SetInt("SetMusic", 1);
				GameObject.Find("Music").GetComponent<AudioSource>().enabled = true;
			}
			else if (this.tag == "SetFX") {
				PlayerPrefs.SetInt("SetFX", 1);
				FX(true);
			}
		}

		PlayerPrefs.Save();
	}
	
	void OnTouchStay () {
		if (renderer.sprite == upButtonOn)
			renderer.sprite = downButtonOn;
		else if (renderer.sprite == upButtonOff)
			renderer.sprite = downButtonOff;
	}
	
	void OnMouseDown () {
		if (renderer.sprite == upButtonOn)
			renderer.sprite = downButtonOn;
		else if (renderer.sprite == upButtonOff)
			renderer.sprite = downButtonOff;

		audio.PlayOneShot(buttonSound);
	}
	
	void OnMouseUp () {
		if (renderer.sprite == downButtonOn) {
			renderer.sprite = upButtonOff;
			
			if (this.tag == "SetMusic") {
				PlayerPrefs.SetInt("SetMusic", 0);
				GameObject.Find("Music").GetComponent<AudioSource>().enabled = false;
			}
			else if (this.tag == "SetFX") {
				PlayerPrefs.SetInt("SetFX", 0);
				FX(false);
			}
		}
		else if (renderer.sprite == downButtonOff) {
			renderer.sprite = upButtonOn;
			
			if (this.tag == "SetMusic") {
				PlayerPrefs.SetInt("SetMusic", 1);
				GameObject.Find("Music").GetComponent<AudioSource>().enabled = true;
			}
			else if (this.tag == "SetFX") {
				PlayerPrefs.SetInt("SetFX", 1);
				FX(true);
			}
		}
		
		PlayerPrefs.Save();
	}

	void FX (bool OnOff) {
		if (OnOff) {
			GameObject.Find("1P").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("2P").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("backButton").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("Settings").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("SceneSaver").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("settingsButton1").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("settingsButton2").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("ParticleSpawner").GetComponent<AudioSource>().enabled = true;
		}
		else {
			GameObject.Find("1P").GetComponent<AudioSource>().enabled = false;
			GameObject.Find("2P").GetComponent<AudioSource>().enabled = false;
			GameObject.Find("backButton").GetComponent<AudioSource>().enabled = false;
			GameObject.Find("Settings").GetComponent<AudioSource>().enabled = false;
			GameObject.Find("SceneSaver").GetComponent<AudioSource>().enabled = false;
			Invoke("LongFXOff", 0.2f);
		}
	}

	void LongFXOff () {
		GameObject.Find("settingsButton1").GetComponent<AudioSource>().enabled = false;
		GameObject.Find("settingsButton2").GetComponent<AudioSource>().enabled = false;
		GameObject.Find("ParticleSpawner").GetComponent<AudioSource>().enabled = false;
	}
}

