using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {

	public float velocidade;
	public float distanciaCamera;

	JoyStick joystick;

	void Start () {
		distanciaCamera = 8;
		velocidade = 30;

		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<JoyStick>();
	}
	
	void Update () {

		controladorVelocidade ();
		controladorCamera ();
	}

	void controladorCamera(){
		Vector3 moverCamera = transform.position - transform.forward * distanciaCamera + Vector3.up * 5.0f;
		float bias = 0.97f;

		Camera.main.transform.position = Camera.main.transform.position  * bias + moverCamera * (1.0f - bias);
		Camera.main.transform.LookAt (transform.position + transform.forward * 30 );

	}
	void controladorVelocidade(){
		transform.position += transform.forward * Time.deltaTime * velocidade;
		transform.Rotate (-joystick.Vertical(),  joystick.Horizontal(),0.0f);


	} 
}
