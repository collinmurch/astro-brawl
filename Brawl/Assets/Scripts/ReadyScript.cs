using UnityEngine;
using System.Collections;

public class ReadyScript : MonoBehaviour {

	public GameObject buttonR;
	public GameObject buttonB;
	private Button buttonScriptR;
	private Button buttonScriptB;

	public GameObject playerR;
	public GameObject playerB;

	public Sprite upButtonB;
	public Sprite upButtonR;

	public GUIText readyText;

	public GameObject scoreR;
	public GameObject scoreB;
	public GameObject score;
	private ScoreScript scoreScript;

	public GUIText scoreRText;
	public GUIText scoreBText;
	public GUIText scoreText;

	public AudioClip ready1;
	public AudioClip ready2;

	public GameObject AIPlayer;
	private AI AIScript;

	public GameObject sceneSaver;
	private SceneSaver sceneSaverScript;

	private int scoreLimit;

	void Start () {
		scoreR = GameObject.Find("ScoreR");
		scoreB = GameObject.Find("ScoreB");
		score = GameObject.Find("Score");

		scoreLimit = PlayerPrefs.GetInt("SetPoints");

		scoreRText = scoreR.GetComponent<GUIText>();
		scoreBText = scoreB.GetComponent<GUIText>();
		scoreText = score.GetComponent<GUIText>();

		scoreScript = score.GetComponent<ScoreScript>();

		sceneSaver = GameObject.Find("SceneSaver");
		sceneSaverScript = sceneSaver.GetComponent<SceneSaver>();

		if (sceneSaverScript.isFirstScene) {
			GameObject.Find("Directions1").GetComponent<GUIText>().enabled = true;
			GameObject.Find("Directions2").GetComponent<GUIText>().enabled = true;
			GameObject.Find("Directions3").GetComponent<GUIText>().enabled = true;
		}

		if (buttonR != null) buttonScriptR = buttonR.GetComponent<Button>();
		buttonScriptB = buttonB.GetComponent<Button>();

		AIScript = AIPlayer.GetComponent<AI>();
	}

	void Update () {
		if (scoreScript.scoreRInt >= scoreLimit || scoreScript.scoreBInt >= scoreLimit)
			Destroy(this.gameObject);
	}

	public void startReady () {
		StartCoroutine(readyWait());
	}

	IEnumerator readyWait () {
		if (scoreScript.scoreBInt < scoreLimit && scoreScript.scoreRInt < scoreLimit) {
			if (sceneSaverScript.player == 1)
				AIScript.enabled = false;
			if (scoreScript.isFirstTimeLoading)
				yield return new WaitForSeconds(0.5f);

			Destroy(GameObject.Find("Directions1"));
			Destroy(GameObject.Find("Directions2"));
			Destroy(GameObject.Find("Directions3"));

			GameObject.Find("BGImage").GetComponent<Animator>().SetTrigger("StartGame");

			audio.PlayOneShot(ready1);
			readyText.text = "3";
			yield return new WaitForSeconds(1);
			audio.PlayOneShot(ready1);
			readyText.text = "2";
			yield return new WaitForSeconds(1);
			audio.PlayOneShot(ready1);
			readyText.text = "1";
			yield return new WaitForSeconds(1);
			audio.PlayOneShot(ready2);
			readyText.text = "GO!";

			buttonScriptB.GetComponent<Button>().OnTouchUp();
			if (buttonR != null) buttonScriptR.GetComponent<Button>().OnTouchUp();
			buttonScriptB.GetComponent<Button>().upButtonMouse();
			if (buttonR != null) buttonScriptR.GetComponent<Button>().upButtonMouse();
			
			buttonScriptB.readyR = true;
			buttonScriptB.readyB = true;
			if (buttonR != null) buttonScriptR.readyR = true;
			if (buttonR != null) buttonScriptR.readyB = true;

			if (buttonR == null) buttonScriptB.canGo = true;

			if (sceneSaverScript.player == 1)
				AIScript.enabled = true;

			yield return new WaitForSeconds(1);
			readyText.enabled = false;

			scoreRText.enabled = true;
			scoreBText.enabled = true;
			scoreText.enabled = true;
		}
	}
}
