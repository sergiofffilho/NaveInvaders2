using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {

	public float velocidade;

	void Start () {
	}
	
	void Update () {
		
		controladorVelocidade ();
		controladorCamera ();
	}

	void controladorCamera(){
		Vector3 moverCamera = transform.position - transform.forward * 10 + Vector3.up * 5.0f;
		float bias = 0.96f;

		Camera.main.transform.position = Camera.main.transform.position  * bias + moverCamera * (1.0f - bias);
		Camera.main.transform.LookAt (transform.position + transform.forward * 30 );

	}
	void controladorVelocidade(){
		transform.position += transform.forward * Time.deltaTime * velocidade;
		transform.Rotate(Input.GetAxis("Vertical"),0.0f, -Input.GetAxis("Horizontal"));

	} 
}
