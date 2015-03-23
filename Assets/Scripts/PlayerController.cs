using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizantal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3  (moveHorizantal,0.0f,moveVertical);
		//Rigidbody.AddForce (movement);
		GetComponent<Rigidbody>().AddForce (movement*10);
	}
}
