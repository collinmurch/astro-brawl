using UnityEngine;
using System.Collections;

public class SpawnParticle : MonoBehaviour {
	
	public GameObject redParticle;
	public GameObject blueParticle;

	public AudioClip boom;

	void Start () {
		Invoke("spawnParticle", 2f);
	}

	void spawnParticle () {
		float randomX = Random.Range(-15, 15);
		float randomY = Random.Range(-7, 7);

		if (Random.Range(0, 10) >= 5) {
			audio.PlayOneShot(boom);
			GameObject explosion = Instantiate(redParticle, new Vector3 (randomX, randomY, 30), Quaternion.identity) as GameObject;
			Destroy(explosion, 3f);
		}

		else {
			audio.PlayOneShot(boom);
			GameObject explosion = Instantiate(blueParticle, new Vector3 (randomX, randomY, 30), Quaternion.identity) as GameObject;
			Destroy(explosion, 3f);
		}

		Invoke("spawnParticle", 2f);
	}
}
