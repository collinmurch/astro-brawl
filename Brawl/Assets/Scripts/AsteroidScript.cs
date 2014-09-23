using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	void Start () {
		if (Random.Range(0, 2) == 1) rigidbody2D.AddForce(new Vector2 (0.2f, 0.2f));
		else rigidbody2D.AddForce(new Vector2 (-0.2f, -0.2f));

		//Random number = asteroid level
	}
}
