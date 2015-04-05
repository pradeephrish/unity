using UnityEngine;
using System.Collections;

public enum SwipeDirection{
	Up,
	Down,
	Right,
	Left
}

public class PlayerController : MonoBehaviour {

	//world boundries
	private float minX, maxX, minZ, maxZ;



	//private static event Action<SwipeDirection> Swipe;
	private bool swiping = false;
	private bool eventSent = false;
	private Vector2 lastPosition;


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
		if (Input.touchCount == 0) 
			return;
		
		if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0){
			if (swiping == false){
				swiping = true;
				lastPosition = Input.GetTouch(0).position;
				return;
			}
			else{
				if (!eventSent) {
					if (Swipe != null) {
						Vector2 direction = Input.GetTouch(0).position - lastPosition;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							if (direction.x > 0) 
								Swipe(SwipeDirection.Right);
							else
								Swipe(SwipeDirection.Left);
						}
						else{
							if (direction.y > 0)
								Swipe(SwipeDirection.Up);
							else
								Swipe(SwipeDirection.Down);
						}
						
						eventSent = true;
					}
				}
			}
		}
		else{
			swiping = false;
			eventSent = false;
		}

	}

	void FixedUpdate(){
		float moveHorizantal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//For Android
		//float pointer_x = Input.GetAxis ("Mouse X");
		//float pointer_y = Input.GetAxis ("Mouse Y");
		//float moveHorizantal = 0.0f;
		//float moveVertical = 0.0f; 
		//if (Input.touchCount > 0) {
		//	moveHorizantal = Input.touches[0].deltaPosition.x;
		//	moveVertical = Input.touches[0].deltaPosition.y;
		//}


		//check position of player if it going outside boundry, don't allow
		Vector3 pos = transform.position;

		Debug.Log ("Min X" + minX + " Max X" + maxX + " Min Z" + minZ + " Max Z" + maxZ + ">>  x:" + pos.x + " z:" + pos.z);


			Vector3 movement;
			if (moveHorizantal != 0) {
				movement = new Vector3 (moveHorizantal, 0.0f, 0.0f)*0.3f;
				if(pos.x > minX  && pos.x < maxX )
					transform.Translate (movement);
				else{
					//reset to one step back
					Debug.Log("Horizantal Boundry");
				/*
					if(pos.x < minX){
						movement = new Vector3 (1.0f, 0.0f, 0.0f);
					}else{
						movement = new Vector3(-1.0f, 0.0f, 0.0f);
					}
				*/
					transform.Translate(movement);
				}
			} 
			if(moveVertical != 0){
				movement = new Vector3 (0.0f, 0.0f, moveVertical)*0.3f;
				if(pos.z > minZ  && pos.z < maxZ )
					transform.Translate (movement);
				else{
					//reset to one step back
					Debug.Log("Vertical Boundry");
					movement = new Vector3 (0.0f, 0.0f, 0.3f);
					/*if(pos.z < minZ){
						movement = new Vector3 (0.0f, 0.0f, 1.0f);
					}else{
						movement = new Vector3 ( 0.0f, 0.0f, -1.0f);
					}*/
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
