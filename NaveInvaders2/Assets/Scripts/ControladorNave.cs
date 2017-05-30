using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {

	public float velocidade;
	public float velocidadeMaxima;
	float velocidadeMinima;

	public Vector3 movimentacao; 
	public float distanciaCamera;
	float aceleracao;
	bool acelerando, freando;
	public GameObject prefab;
	public bool atirar, possoatirar;
	public GameObject escudo;

	float tempoRecarga ;
	float tempoRecargaEscudo;
	float tempoVisualizacaoEscudo;

	JoyStick joystick;

	bool possoativar;

	void Start () {
		velocidadeMaxima = 50;
		velocidadeMinima = 15;
		possoatirar = true;
		possoativar = true;
		atirar = false;
		tempoRecarga = 1;
		tempoRecargaEscudo = 20.0f;
		tempoVisualizacaoEscudo = 8.0f;
		distanciaCamera = 8;
		velocidade = 30;
		aceleracao = 0f;

		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<JoyStick>();
	}
	
	void Update () {

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

		Debug.Log (velocidade);

		if (acelerando && velocidade >= velocidadeMaxima){
			acelerando = false;
			aceleracao = 0;
		}
		if (freando && velocidade <= velocidadeMinima){
			freando = false;
			aceleracao = 0;
		}
		if (aceleracao == 0){
			if (velocidade > 30) {
				velocidade -= 2f * Time.deltaTime;
			} else {
				velocidade += 2f * Time.deltaTime;

			}
		}
		velocidade += aceleracao * Time.deltaTime;
		movimentacao = transform.forward * Time.deltaTime * velocidade;
		transform.position += movimentacao;
		transform.Rotate (-joystick.Vertical(), joystick.Horizontal(),0.0f);
	}

	public void acelerarBotao(){
		
		acelerando = true;
		acelerar ();
	}
	void acelerar(){
		aceleracao = 10f;
		acelerando = true;
	}	

	public void desacelerarBotao(){
		desacelerar ();
	}

	void desacelerar(){
		freando = true;
		aceleracao = -10f;
		
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
