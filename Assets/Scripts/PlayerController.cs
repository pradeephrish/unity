using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//world boundries

	private float minX, maxX, minZ, maxZ;
	// Use this for initialization
	void Start () {
		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Vector3 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
		Vector3 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));
		
		minX = bottomCorner.x;
		maxX = topCorner.x;
		minZ = bottomCorner.z;
		maxZ = topCorner.z;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		float moveHorizantal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		//check position of player if it going outside boundry, don't allow
		Vector3 pos = transform.position;

		Debug.Log ("Min X" + minX + " Max X" + maxX + " Min Z" + minZ + " Max Z" + maxZ + ">>  x:" + pos.x + " z:" + pos.z);


			Vector3 movement;
			if (moveHorizantal != 0) {
				movement = new Vector3 (moveHorizantal, 0.0f, 0.0f);
				if(pos.x > minX  && pos.x < maxX )
					transform.Translate (movement);
				else{
					//reset to one step back
					Debug.Log("Horizantal Boundry");
					if(pos.x < minX){
						movement = new Vector3 (1.0f, 0.0f, 0.0f);
					}else{
						movement = new Vector3(-1.0f, 0.0f, 0.0f);
					}
					transform.Translate(movement);
				}
			} 
			if(moveVertical != 0){
				movement = new Vector3 (0.0f, 0.0f, moveVertical);
				if(pos.z > minZ  && pos.z < maxZ )
					transform.Translate (movement);
				else{
					//reset to one step back
					Debug.Log("Vertical Boundry");
					if(pos.z < minZ){
						movement = new Vector3 (0.0f, 0.0f, 1.0f);
					}else{
						movement = new Vector3 ( 0.0f, 0.0f, -1.0f);
					}
					transform.Translate(movement);
				}
			}

			//Rigidbody.AddForce (movement);
			//GetComponent<Rigidbody> ().AddRelativeForce (movement * 1000);
			//stop the body
			//GetComponent<Rigidbody> ().velocity = Vector3.zero;
			moveHorizantal = 0;
			moveVertical = 0;
		} 
}
