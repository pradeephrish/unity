using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float speed=100.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizantal = Input.GetAxis ("Horizontal"); //x
		float moveVertical = Input.GetAxis ("Vertical");   //y
		
		//Vector3 movement = new Vector3 (moveHorizantal,0.0f,0.0f);
		//Rigidbody.AddForce (movement);
		//GetComponent<Rigidbody>().AddForce (movement);
		//transform.Translate(0, moveHorizantal * speed , 0);
	}
}
