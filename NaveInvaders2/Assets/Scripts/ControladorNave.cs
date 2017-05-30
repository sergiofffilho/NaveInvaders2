using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {

	public float velocidade;
	public Vector3 movimentacao; 
	public float distanciaCamera;
	float aceleracao;
	bool acelerando;
	public GameObject prefab;
	public bool atirar, possoatirar;
	public GameObject escudo;

	float tempoRecarga ;
	float tempoRecargaEscudo;
	float tempoVisualizacaoEscudo;
	JoyStick joystick;

	bool possoativar;

	void Start () {
		possoatirar = true;
		possoativar = true;
		atirar = false;
		tempoRecarga = 1;
		tempoRecargaEscudo = 20.0f;
		tempoVisualizacaoEscudo = 8.0f;
		distanciaCamera = 8;
		velocidade = 30;
		aceleracao = 0.1f;

		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<JoyStick>();
	}
	
	void Update () {
//		if(Input.GetAxis("Fire1") == 1){
//			atirar ();
//		}
		controladorVelocidade ();
		controladorCamera ();

		if (atirar){
			if (possoatirar) {
				StartCoroutine (Atirar ());
			}
		}
	}

	void controladorCamera(){
		Vector3 moverCamera = transform.position - transform.forward * distanciaCamera + Vector3.up * 5.0f;
		float bias = 0.97f;

		Camera.main.transform.position = Camera.main.transform.position  * bias + moverCamera * (1.0f - bias);
		Camera.main.transform.LookAt (transform.position + transform.forward * 30 );

	}

	void controladorVelocidade(){
		movimentacao = transform.forward * Time.deltaTime * velocidade;
		transform.position += movimentacao;
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

	public IEnumerator Atirar () {
		//Instantiate your projectile
		Disparar();
		//wait for some time
		possoatirar = false;
		yield return new WaitForSeconds (tempoRecarga);
		possoatirar = true;
	}

	void Disparar(){
		Instantiate(prefab, new Vector3(2.0f, 2f, 2f), Quaternion.identity);
	}

	public void escudoBotao(){
		if (possoativar) {
			StartCoroutine (Escudo ());
		}
	}

	public IEnumerator Escudo () {
		ativarEscudo ();
		possoativar = false;
		yield return new WaitForSeconds (tempoRecargaEscudo);
		possoativar = true;
	}

	void ativarEscudo(){
		escudo.SetActive (true);
		StartCoroutine (MostrarEscudo ());
	}

	public IEnumerator MostrarEscudo () {
		yield return new WaitForSeconds (tempoVisualizacaoEscudo);
		escudo.SetActive (false);
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.CompareTag ("fase")) {
			Debug.Log ("Morreu fase");
		}

		if (coll.gameObject.CompareTag ("parede")) {
			Debug.Log ("Morreu parede");
		}

		if (coll.gameObject.CompareTag ("porta")) {
			Debug.Log ("Morreu porta");
		}
	}
}
