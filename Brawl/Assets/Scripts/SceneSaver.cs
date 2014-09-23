using UnityEngine;
using System.Collections;

public class SceneSaver : MonoBehaviour {
	public int player;

	public bool isFirstScene;

	public AudioClip hurtContinuePlay;

	void Start () {
		player = 0;
		isFirstScene = true;
	}

	public void hurtPlayBetweenScenes () {
		audio.PlayOneShot(hurtContinuePlay);
	}
}
