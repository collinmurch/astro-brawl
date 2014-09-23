using UnityEngine;
using System.Collections;

public class SoundEnabler : MonoBehaviour {
	
	void Start () {
		if (PlayerPrefs.GetInt("SetMusic") == 1 && this.tag == "SetMusic")
			GetComponent<AudioSource>().enabled = true;
		else if (PlayerPrefs.GetInt("SetMusic") == 0 && this.tag == "SetMusic")
			GetComponent<AudioSource>().enabled = false;

		if (PlayerPrefs.GetInt("SetFX") == 1 && this.tag != "SetMusic")
			GetComponent<AudioSource>().enabled = true;
		else if (PlayerPrefs.GetInt("SetFX") == 0 && this.tag != "SetMusic")
			GetComponent<AudioSource>().enabled = false;
	}
}
