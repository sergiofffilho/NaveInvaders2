using UnityEngine;
using System.Collections;

public class ControladorDisparo : MonoBehaviour {
	ControladorNave nave;

	// Use this for initialization
	void Start () {
		nave = GameObject.Find ("Nave").GetComponent<ControladorNave>();	
		transform.position = nave.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (nave.movimentacao * 20);
	}
}
