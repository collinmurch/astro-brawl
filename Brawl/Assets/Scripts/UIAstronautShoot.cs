using UnityEngine;
using System.Collections;

public class UIAstronautShoot : MonoBehaviour {

	public Animator anim;
	
	void Start () {
		anim = GetComponent<Animator>();
		Shoot();
	}

	void Shoot () {
		if (Random.value < 0.2)
			anim.SetTrigger("Shoot");
		Invoke("Shoot", 1f);
	}
}
