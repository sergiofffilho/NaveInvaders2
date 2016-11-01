using UnityEngine;
using System.Collections;

public class ControladorNave : MonoBehaviour {


	void Start () {
	
	}
	
	void Update () {
		controladorVelocidade ();
	}

	void controladorVelocidade(){
		transform.Rotate(Input.GetAxis("Vertical"),0.0f, -Input.GetAxis("Horizontal"));

	} 
}
