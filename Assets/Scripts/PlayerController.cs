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

	int currentRow=0;
	int currentColumn=0;

	string[,] stateTable = new string[3,6] {{"0,0","Once Upon a Time","Right","1","",""},
											{"0,1","Once Upon a Time","End","1","",""},
										 	{"1,1","Once Upon a Time","Down","1","",""}
										   };

	public bool isUpdateAllowed(){
		//set direction when touched
		bool isLeft = false;
		bool isRight = false;
		bool isTop = false;
		bool isDown = false;

		if (isLeft) {
			if (stateTable [currentRow,currentColumn - 1].Equals ("End"))
				return false;
			else {
				currentColumn = currentColumn - 1;
				return true;
			}
		} else if (isRight) {
			if (stateTable [currentRow,currentColumn + 1].Equals ("End"))
				return false;
			else {
				currentColumn = currentColumn + 1;
				return true;
			}
		} else if (isTop) {
			if (stateTable [currentRow - 1,currentColumn].Equals ("End"))
				return false;
			else {
				currentRow = currentRow - 1;
				return true;
			}
		} else {
			if (stateTable [currentRow + 1,currentColumn].Equals ("End"))
				return false;
			else {
				currentRow = currentRow + 1;
				return true;
			}
		}

		return false;
	}


	//private static event Action<SwipeDirection> Swipe;
	private bool swiping = false;
	private bool eventSent = false;
	private Vector2 lastPosition;
	private float width = 0.0f;
	private float height = 0.0f;


	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.Landscape;

		float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Vector3 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
		Vector3 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));

		//viewport width and height can be determined by difference between bottomCorner and topCorner

		width = (bottomCorner.x - topCorner.x);
		height = (topCorner.z - bottomCorner.z);

		minX = bottomCorner.x;
		maxX = topCorner.x; 
		minZ = bottomCorner.z;
		maxZ = topCorner.z;

	}
	
	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate(){
		Vector3 movement;
		float moveHorizantal=0.0f;
		float moveVertical = 0.0f;
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
					if (swiping) {
						Vector2 direction = Input.GetTouch(0).position - lastPosition;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							if (direction.x > 0) {
								//Swipe(SwipeDirection.Right);
								moveHorizantal=width;
							}
							else{
								//Swipe(SwipeDirection.Left);
								moveHorizantal=(-1) * width;
							}
							movement = new Vector3 (moveHorizantal, 0.0f, 0.0f);
							//device 6 style
							transform.Translate(movement);
						}
						else{
							if (direction.y > 0){
								//Swipe(SwipeDirection.Up);
								moveVertical=height;
							}
							else{
								//Swipe(SwipeDirection.Down);
								moveVertical=(-1)*height;
							}
							movement = new Vector3 (0.0f, 0.0f, moveVertical);
							transform.Translate (movement);
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
}
