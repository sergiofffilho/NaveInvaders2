using UnityEngine;
using System.Collections;

public class BotaoDisparo : MonoBehaviour {
	ControladorNave nave;
	// Use this for initialization
	void Start () {
		nave = GameObject.FindGameObjectWithTag ("nave").GetComponent<ControladorNave>();

	}
	
	// Update is called once per frame
	public void AtirarDown(){
		nave.atirar = true;
	}
	public void AtirarUp(){
		nave.atirar = false;
	}


}
