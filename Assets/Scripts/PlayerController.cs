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

	
	float speed = 0.1f;

	int direction = 0; // 1,2,3,4  -  left,right,up,down resp

	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Stationary) {
			Vector2 touchPosition = Input.GetTouch (0).position;
			double halfScreen = Screen.width / 10.0;
			double rightScreen = (Screen.width * 9.0) / 10.0;

			double halfVertical = Screen.height / 10.0;
			double belowVertical = (Screen.height * 9.0) / 10.0;
			
			//Check if it is left or right?
			if (touchPosition.x < halfScreen) {
				//transform.Translate(Vector3.left * 10 * Time.deltaTime);
				direction = 1;
			} else if (touchPosition.x > rightScreen) {
				//transform.Translate(Vector3.right * 10 * Time.deltaTime);
				direction = 2;
			} else if (touchPosition.y < halfVertical) {
				//Vector3 movement = new Vector3(0,0,Vector3.left.x*10*Time.deltaTime);
				//transform.Translate(movement);	
				direction = 3;
			} else if (touchPosition.y > belowVertical) {
				//Vector3 movement = new Vector3(0,0,Vector3.left.x*10*Time.deltaTime);
				//transform.Translate(movement*(-1));
				direction = 4;
			}
		} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {

			if (direction == 1){
				//move by all the screen size
				Vector3 movement = new Vector3 (width, 0, 0);
				transform.Translate (movement);
				direction = -1;
			}
			else if (direction == 2) {
				//move by all the screen size
				Vector3 movement = new Vector3 (-width, 0, 0);
				transform.Translate (movement);
				direction = -1;
			}
			else if(direction == 3){
				//move by all the screen size
				Vector3 movement = new Vector3 (0, 0, -height);
				transform.Translate (movement);
				direction = -1;
			}else if(direction == 4){
				//move by all the screen size
				Vector3 movement = new Vector3 (0, 0, height);
				transform.Translate (movement);
				direction = -1;
			}

		}
	}

	void FixedUpdate(){
		/*float weight = Mathf.Cos(Time.time * speed * 2 * Mathf.PI) * 0.5f + 0.5f;
		transform.position = targetA.transform.position * weight
			+ targetB.transform.position * (1-weight);*/
	} 
}
