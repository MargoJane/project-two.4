using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
	public static Vector3 cubePosition;
	public static Vector3 airplanePosition;
	public static float xPosition;
	public static float yPosition;
	public static int numCubesX;
	public static int numCubesY;
	public static int numCubesXLine1;

	public static int maxX;
	public static int maxY;

	public static GameObject[,] myCube;
	public static GameObject firstCube;
	public GameObject cubePrefab;
	public static bool airplaneActivated;

	public static GameObject airplaneCube;
	public static GameObject dropOffCube;

	public static float turnTime;
	public static float timeSpace;
	public static int cargo;
	public static int maxCargo;
	public static int minCargo;
	public static int cargoPoints;

	public Text cargoAirplaneText;
	public Text cargoScoreText;

	public static int airplaneX;
	public static int airplaneY;

	// Use this for initialization
	void Start () {
		airplaneActivated = false;
		timeSpace = 1.5f;
		turnTime = timeSpace;
		cargo = 0;
		maxCargo = 90;
		minCargo = 0;
		cargoPoints = 0;

		numCubesX = 16;
		numCubesY = 9;
		xPosition = -22f;
		yPosition = 8f;

		yPosition -= 2;
		xPosition = -22f;

		myCube = new GameObject[numCubesX, numCubesY];

		//makes the rows of cubes
		for (int y = 0; y < numCubesY; y++) {
			for (int x = 0; x < numCubesX; x++) {
				cubePosition = new Vector3 (xPosition, yPosition, 0);
				myCube[x,y] = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				myCube[x,y].GetComponent<Renderer> ().material.color = Color.white;
				myCube[x,y].GetComponent<cubeScript> ().X = x;
				myCube[x,y].GetComponent<cubeScript> ().Y = y;
				xPosition += 3;
			}
			xPosition = -22f;
			yPosition -= 2;
		}

		//makes first cube airplane cube initially
		myCube[0,0].GetComponent<Renderer> ().material.color = Color.red;
		airplaneCube = myCube[0,0];
		airplaneX = airplaneCube.GetComponent<cubeScript> ().X;
		airplaneY = airplaneCube.GetComponent<cubeScript> ().Y;

		//makes depot cube black
		myCube[numCubesX-1,numCubesY-1].GetComponent<Renderer> ().material.color = Color.black;
		dropOffCube = myCube[numCubesX-1, numCubesY-1];

	}

	// Update is called once per frame
	void Update (){
		//moves the airplane
		if (airplaneActivated == true) {
			if (Input.GetKeyDown (KeyCode.UpArrow) && airplaneY > 0) {
				if (airplaneY == numCubesY-1 && airplaneX == numCubesX-1) {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.black;
				} else {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.white;
				}
				airplaneY -= 1;
				airplaneCube = myCube [airplaneX, airplaneY];

				airplaneCube.GetComponent<Renderer> ().material.color = Color.yellow;

			} else if (Input.GetKeyDown (KeyCode.DownArrow) && airplaneY < numCubesY-1) {
				if (airplaneY == numCubesY-1 && airplaneX == numCubesX-1) {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.black;
				} else {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.white;
				}
				airplaneY += 1;
				airplaneCube = myCube [airplaneX, airplaneY];

				airplaneCube.GetComponent<Renderer> ().material.color = Color.yellow;

			} else if (Input.GetKeyDown (KeyCode.RightArrow) && airplaneX < numCubesX-1) {
				if (airplaneY == numCubesY-1 && airplaneX == numCubesX-1) {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.black;
				} else {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.white;
				}
				airplaneX += 1;
				airplaneCube = myCube [airplaneX, airplaneY];

				airplaneCube.GetComponent<Renderer> ().material.color = Color.yellow;

			} else if (Input.GetKeyDown (KeyCode.LeftArrow) && airplaneX > 0) {
				if (airplaneY == numCubesY-1 && airplaneX == numCubesX-1) {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.black;
				} else {
					airplaneCube.GetComponent<Renderer> ().material.color = Color.white;
				}
				airplaneX -= 1;
				airplaneCube = myCube [airplaneX, airplaneY];

				airplaneCube.GetComponent<Renderer> ().material.color = Color.yellow;

			}
		}


		//checks if airplane is in initial spot and if it has 90 cargo tons. If it's trye it adds 10 cargo turnTime seconds
		if (airplaneX == 0 && airplaneY == 0) {
			if (cargo < maxCargo) {
				if (Time.time > turnTime) {
					cargo += 10;
					cargoAirplaneText.text = "Cargo: " + cargo;
					turnTime += timeSpace;
				}
			} 
			//checks if airplane is in cargo drop off area, drops off cargo if so and adds score
		} else if (airplaneX == numCubesX-1 && airplaneY == numCubesY-1) {	
			if (cargo > minCargo) {
				cargoPoints = cargoPoints + cargo;
				cargoScoreText.text = "Score: " + cargoPoints;
				cargo = 0;
				cargoAirplaneText.text = "Cargo: " + cargo;
				turnTime = Time.time;
			}

		} else {
			turnTime = Time.time;
		}
	}
		
	public static void processClick(GameObject clickedCube) {
		//checks if clickCube is the same as airPlane cube
		if (airplaneX == clickedCube.GetComponent<cubeScript> ().X && airplaneY == clickedCube.GetComponent<cubeScript> ().Y) {
			//deactivates the airplane if activated airplane is clicked
			if (airplaneActivated == true) {
				airplaneActivated = false;
				airplaneCube.GetComponent<Renderer> ().material.color = Color.red;
			}
			//activates the airplane if deactivated airplane is clicked
			else if (airplaneActivated == false) {
				airplaneActivated = true;
				airplaneCube.GetComponent<Renderer> ().material.color = Color.yellow;
			}
		//moves the active airplane to a cloud cube if an airplane is active and a cloud cube is clicked, and turns the old cube back to white
		}
	}
}
