using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {

	public float velocidade;
	public float distanciaCamera;
	float aceleracao;
	bool acelerando;

	JoyStick joystick;

	void Start () {
		distanciaCamera = 8;
		velocidade = 30;
		aceleracao = 0.1f;

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

	public void acelerarBotao(){
		aceleracao = 0.1f;
		acelerando = true;
		InvokeRepeating("acelerar", 0, 0.1f);
	}

	public void desacelerarBotao(){
		acelerando = false;
		InvokeRepeating("desacelerar", 0, 0.1f);
	}

	void acelerar(){
		Debug.Log ("a - "+aceleracao);
		if (aceleracao >= 0 && acelerando) {
			transform.Translate(Vector3.forward  * aceleracao * Time.deltaTime);
			aceleracao += 20f * Time.deltaTime;

		} 
		if (aceleracao <= 0 && !acelerando) {
			transform.Translate(Vector3.forward  * aceleracao * Time.deltaTime);
			aceleracao += 20f * Time.deltaTime;
		}
		if (aceleracao >= 30 ) {
			InvokeRepeating("desacelerar", 0, 0.1f);
			CancelInvoke ("acelerar");
		}
		if (aceleracao >= 0 && !acelerando) {
			aceleracao = 0.1f;
			CancelInvoke ("acelerar");
		}
	}

	void desacelerar(){
		Debug.Log ("d - "+aceleracao);
		if (aceleracao <= 32 && acelerando) {
			transform.Translate(Vector3.forward  * aceleracao * Time.deltaTime);
			aceleracao -= 20f * Time.deltaTime;
		}
		if (aceleracao <= 0 && acelerando) {
			//aceleracao = 0.1f;
			CancelInvoke ("desacelerar");
		}
		if (aceleracao <= 0.2f && !acelerando) {
			transform.Translate(Vector3.forward  * aceleracao * Time.deltaTime);
			aceleracao -= 20f * Time.deltaTime;
		}
		if (aceleracao <= -32) {
			InvokeRepeating("acelerar", 0, 0.1f);
			CancelInvoke ("desacelerar");
		}


	}
}
