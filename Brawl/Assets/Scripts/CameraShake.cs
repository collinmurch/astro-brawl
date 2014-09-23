using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	private Vector3 originPosition;
	private Quaternion originRotation;

	private float shake_decay;
	private float shake_intensity;

	private bool shaking;
	private Transform _transform;

	private GameObject sceneSaver;
	private SceneSaver sceneSaverScript;
	
	void OnEnable() {
		_transform = transform;

		sceneSaver = GameObject.Find("SceneSaver");
		sceneSaverScript = sceneSaver.GetComponent<SceneSaver>();

		if (!sceneSaverScript.isFirstScene)
			Shake();
	}
	
	void Update (){
		if(!shaking)
			return;
		
		if (shake_intensity > 0f){
			_transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;

			_transform.localRotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			
			shake_intensity -= shake_decay;
		} else {
			shaking = false;
			
			_transform.localPosition = originPosition;
			_transform.localRotation = originRotation;
		}
	}
	
	public void Shake (){
		if(!shaking) {
			originPosition = _transform.localPosition;
			originRotation = _transform.localRotation;	
		}
		
		shaking = true;
		shake_intensity = .05f;
		shake_decay = 0.001f;
	}
}