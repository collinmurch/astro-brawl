using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	public GameObject scoreR;
	public GameObject scoreB;
	public GameObject score;

	public GUIText scoreRText;
	public GUIText scoreBText;
	public GUIText scoreText;

	public int scoreRInt;
	public int scoreBInt;

	public bool isFirstTimeLoading;
	
	public GameObject winTextGameObject;
	public GUIText winText;

	void Start () {
		isFirstTimeLoading = true;

		scoreRInt = 0;
		scoreBInt = 0;

		scoreR = GameObject.Find("ScoreR");
		scoreB = GameObject.Find("ScoreB");
		score = this.gameObject;
		
		scoreRText = scoreR.GetComponent<GUIText>();
		scoreBText = scoreB.GetComponent<GUIText>();
		scoreText = score.GetComponent<GUIText>();
	}

	public void addScoreB () {
		scoreBInt++;
		if (scoreBInt >= PlayerPrefs.GetInt("SetPoints")) {
			winTextGameObject = GameObject.Find("WinText");
			winText = winTextGameObject.GetComponent<GUIText>();

			StartCoroutine(EndGame("Blue"));
		}
		scoreBText.text = "" + scoreBInt;
	}

	public void addScoreR () {
		scoreRInt++;
		if (scoreRInt >= PlayerPrefs.GetInt("SetPoints")) {
			winTextGameObject = GameObject.Find("WinText");
			winText = winTextGameObject.GetComponent<GUIText>();

			StartCoroutine(EndGame("Red"));
		}
		scoreRText.text = "" + scoreRInt;
	}

	IEnumerator EndGame (string winner) {
		if ((scoreBInt >= PlayerPrefs.GetInt("SetPoints") && scoreRInt == 0) || (scoreRInt >= PlayerPrefs.GetInt("SetPoints") && scoreBInt == 0))
			winText.text = winner + " wins\nflawlessly!";
		else
			winText.text = winner + " wins!";

		GameObject.Find("BGImage").GetComponent<Animator>().SetTrigger("StartGame");

		winText.enabled = true;
		
		yield return new WaitForSeconds(5);

		Destroy(GameObject.Find("Music"));
		Destroy(GameObject.Find("SceneSaver"));
		Destroy(GameObject.Find("ButtonR"));
		Destroy(GameObject.Find("ButtonB"));
		Destroy(scoreR);
		Destroy(scoreB);
		Destroy(winText.gameObject);

		Application.LoadLevel(0);
		Destroy(this.gameObject);
	}
}
