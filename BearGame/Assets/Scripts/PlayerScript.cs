using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
		public enum Direction
		{
				North,
				East,
				South,
				West}
		;
		public Direction currentDirection = Direction.East;
		public Direction lastDirection = Direction.East;
		public Direction arrowDirection = Direction.East;
		public float speed = .1f;
		public float lureValue = 100f;
		public bool oppositeSpotted = false;
		public bool oppositeEqual = false;
		public float oppositeDistance = 0;
		public float targetDistanceX = 0;
		public float targetDistanceY = 0;
		public float targetDistance = 0;
		public int lureSpotted = 0;
		public string arrowName = "";
		public bool onArrow = false;

//	public GameObject childN;
//	public GameObject childE;
//	public GameObject childS;
//	public GameObject childW;

		// Use this for initialization
		void Start ()
		{
				//Find Children
//		childN = GameObject.FindGameObjectWithTag("ChildN").gameObject;
//		childE = GameObject.FindGameObjectWithTag("ChildE").gameObject;
//		childS = GameObject.FindGameObjectWithTag("ChildS").gameObject;
//		childW = GameObject.FindGameObjectWithTag("ChildW").gameObject;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
		
				//Reset Variables
				lureSpotted = 0;
				targetDistanceX = 0;
				targetDistanceY = 0;
				targetDistance = 0;
				lastDirection = currentDirection;
				oppositeSpotted = false;
				oppositeEqual = false;
				oppositeDistance = 0;

				//Movement
				switch (currentDirection) {
				case Direction.East:
			
						transform.Translate (Vector3.right * Time.deltaTime * speed);
						break;

				case Direction.North:
						transform.Translate (Vector3.up * Time.deltaTime * speed);
						break;

				case Direction.South:
						transform.Translate (Vector3.down * Time.deltaTime * speed);
						break;

				case Direction.West:
						transform.Translate (Vector3.left * Time.deltaTime * speed);
						break;
				}

				//Raycasting
				RaycastHit hit;
		
				Vector3 abovePos = new Vector3 (transform.position.x, transform.position.y + .49f, transform.position.z);
				Vector3 belowPos = new Vector3 (transform.position.x, transform.position.y - .49f, transform.position.z);
				Vector3 leftPos = new Vector3 (transform.position.x - .49f, transform.position.y, transform.position.z);
				Vector3 rightPos = new Vector3 (transform.position.x + .49f, transform.position.y, transform.position.z);
		
				Ray centerUpRay = new Ray (transform.position, Vector3.up);
				Ray leftUpray = new Ray (leftPos, Vector3.up);
				Ray rightUpray = new Ray (rightPos, Vector3.up);
		
				Ray centerRightRay = new Ray (transform.position, Vector3.right);
				Ray topRightRay = new Ray (abovePos, Vector3.right);
				Ray bottomRightRay = new Ray (belowPos, Vector3.right);
		
				Ray centerDownRay = new Ray (transform.position, Vector3.down);
				Ray leftDownRay = new Ray (leftPos, Vector3.down);
				Ray rightDownRay = new Ray (rightPos, Vector3.down);
		
				Ray centerLeftRay = new Ray (transform.position, Vector3.left);
				Ray topLeftRay = new Ray (abovePos, Vector3.left);
				Ray bottomLeftRay = new Ray (belowPos, Vector3.left);


				Ray centerVertRay = new Ray (transform.position, Vector3.forward);
				Ray upVertRay = new Ray (abovePos, Vector3.forward);
				Ray downVertRay = new Ray (belowPos, Vector3.forward);
				Ray rightVertRay = new Ray (rightPos, Vector3.forward);
				Ray leftVertRay = new Ray (leftPos, Vector3.forward);
		
		
				//NOTE TO SELF, USE REVERSE RAY FOR DETECTING THE OBJECTS!!!! <----- DUMB IDEA!!!!
//		Ray revUpRay = new Ray (abovePos, Vector3.down);  
//		Ray revDownRay = new Ray (belowPos, Vector3.up); 
//		Ray revRightRay = new Ray (rightPos, Vector3.left);
//		Ray RevLeftRay = new Ray (leftPos, Vector3.right);
		
				Debug.DrawRay (transform.position, Vector3.forward * 2f, Color.cyan);
				Debug.DrawRay (abovePos, Vector3.forward * 2f, Color.cyan);
				Debug.DrawRay (belowPos, Vector3.forward * 2f, Color.cyan);
				Debug.DrawRay (rightPos, Vector3.forward * 2f, Color.cyan);
				Debug.DrawRay (leftPos, Vector3.forward * 2f, Color.cyan);
		

				// Draw Rays for Debuging Purposes
				/*
		Debug.DrawRay(transform.position, Vector3.up * .5f, Color.green);		//Center -> Up
		Debug.DrawRay(leftPos, Vector3.up * .5f, Color.green);					//Left -> Up
		Debug.DrawRay(rightPos, Vector3.up * .5f, Color.green);				//Right -> Up	
		
		Debug.DrawRay(transform.position, Vector3.down * .5f, Color.green);		//Center -> Down
		Debug.DrawRay(leftPos, Vector3.down * .5f, Color.green);					//Left -> Down
		Debug.DrawRay(rightPos, Vector3.down * .5f, Color.green);				//Right -> Down
		
		Debug.DrawRay(transform.position, Vector3.right * .5f, Color.green); 	//Center -> Right
		Debug.DrawRay(abovePos, Vector3.right * .5f, Color.green);				//Top -> Right
		Debug.DrawRay(belowPos, Vector3.right * .5f, Color.green);				//Botton -> Right
		
		Debug.DrawRay(transform.position, Vector3.left * .5f, Color.green);		//Center -> Left
		Debug.DrawRay(abovePos, Vector3.left * .5f, Color.green);				//Top -> Left
		Debug.DrawRay(belowPos, Vector3.left * .5f, Color.green);				//Botton -> Left
		*/
	
				int layerMaskWalls = 1 << 9;
				int layerMaskItems = 1 << 10;
				int layerMaskArrows = 1 << 13;
				int finalMask = layerMaskWalls | layerMaskItems;
				int finalMask2 = layerMaskWalls | layerMaskArrows;


				if (Physics.Raycast (centerVertRay, out hit, Mathf.Infinity, layerMaskArrows) && hit.collider.tag == "Arrow" &&
						Physics.Raycast (upVertRay, out hit, Mathf.Infinity, layerMaskArrows) && hit.collider.tag == "Arrow" &&
						Physics.Raycast (downVertRay, out hit, Mathf.Infinity, layerMaskArrows) && hit.collider.tag == "Arrow" &&
						Physics.Raycast (rightVertRay, out hit, Mathf.Infinity, layerMaskArrows) && hit.collider.tag == "Arrow" &&
						Physics.Raycast (leftVertRay, out hit, Mathf.Infinity, layerMaskArrows) && hit.collider.tag == "Arrow"
		   
		   
		   
		   
		   ) {
						onArrow = true;
						arrowName = hit.transform.GetComponent<SpriteRenderer> ().sprite.name;
						//Debug.Log("On Arrow");
				} else {
						onArrow = false;
						//Debug.Log("Off Arrow");
				}

				if (onArrow == true) {
						//onArrow = true;
						//Debug.Log("Hit Arrow");
			
						switch (arrowName) {
						//4 way intersections
						case "Phssthpok_32x32_18":
								if (currentDirection == Direction.North) {
										arrowDirection = Direction.West;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.East) {
										arrowDirection = Direction.North;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.South) {
										arrowDirection = Direction.East;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.West) {
										arrowDirection = Direction.South;
										currentDirection = arrowDirection;
								}
								break;
				
						case "Phssthpok_32x32_19":
								if (currentDirection == Direction.North) {
										arrowDirection = Direction.East;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.East) {
										arrowDirection = Direction.South;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.South) {
										arrowDirection = Direction.West;
										currentDirection = arrowDirection;
										break;
								}
								if (currentDirection == Direction.West) {
										arrowDirection = Direction.North;
										currentDirection = arrowDirection;
								}
								
								break;
				
						//3 way intersections
						case "Phssthpok_32x32_20":
								if(currentDirection == Direction.East)
									arrowDirection = Direction.South;
								break;
				
						case "Phssthpok_32x32_21":
								if(currentDirection == Direction.East)
									arrowDirection = Direction.North;
								break;
				
						case "Phssthpok_32x32_22":
								if(currentDirection == Direction.South)
									arrowDirection = Direction.West;
								break;
				
						case "Phssthpok_32x32_23":
								if(currentDirection == Direction.South)
									arrowDirection = Direction.East;
								break;
				
						case "Phssthpok_32x32_24":
								if(currentDirection == Direction.West)
									arrowDirection = Direction.North;
								break;
				
						case "Phssthpok_32x32_25":
								if(currentDirection == Direction.West)
									arrowDirection = Direction.South;
								break;
				
						case "Phssthpok_32x32_26":
								if(currentDirection == Direction.North)
									arrowDirection = Direction.East;
								break;
				
						case "Phssthpok_32x32_27":
								if(currentDirection == Direction.North)
									arrowDirection = Direction.West;
								break;
				
						}
						//
						Debug.Log ("Test");
						transform.position = new Vector3 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y), transform.position.z);		
				}

				if (currentDirection == Direction.East && Physics.Raycast (centerRightRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall") {
			
						currentDirection = arrowDirection;
						if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.South;
						if (Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.North; 
						if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls)) 
								currentDirection = Direction.West;
						transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);		
				}

				if (currentDirection == Direction.South && Physics.Raycast (centerDownRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall") {
			
						currentDirection = arrowDirection;
						if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.West;
						if (Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.East;
						if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls)) 
								currentDirection = Direction.North;
						transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);	
				}

				if (currentDirection == Direction.West && Physics.Raycast (centerLeftRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall") {
				
						currentDirection = arrowDirection;
						if (Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.North;
						if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.South;
						if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls)) 
								currentDirection = Direction.East;
						transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
				}

				if (currentDirection == Direction.North && Physics.Raycast (centerUpRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall") {	
				
						currentDirection = arrowDirection;
						if (Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.East;
						if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls))
								currentDirection = Direction.West;
						if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls)) 
								currentDirection = Direction.South;
						transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);
				}




				if ((Physics.Raycast (centerRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (topRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (bottomRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure")) {
						Debug.DrawRay (transform.position, Vector3.right * 25f, Color.red);
						Debug.DrawRay (abovePos, Vector3.right * 25f, Color.red);
						Debug.DrawRay (belowPos, Vector3.right * 25f, Color.red);
						if (currentDirection == Direction.North) 
								transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);
						if (currentDirection == Direction.South)
								transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);
						targetDistanceX = Mathf.Abs (hit.transform.position.x - transform.position.x);
						targetDistanceY = Mathf.Abs (hit.transform.position.y - transform.position.y);
						if (targetDistance != 0) {
								if (Mathf.Abs (targetDistanceX - targetDistanceY) < Mathf.Floor (targetDistance)) {
										
										currentDirection = Direction.East;
										targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
										lureSpotted++;	
										Debug.Log ("LURESPOOTED: to EAST" + lureSpotted);
										if (lastDirection == Direction.East)
											oppositeSpotted = true;
								} else if (Mathf.Abs (targetDistanceX - targetDistanceY) == targetDistance) {
										lureSpotted++;	
										Debug.Log ("LURESPOOTED: to EAST" + lureSpotted);
										if (lastDirection == Direction.East && oppositeSpotted)
											oppositeEqual = true;
										if (onArrow) {
												currentDirection = arrowDirection;		
										} else {
												currentDirection = lastDirection;
										}
								}
						} else {
								currentDirection = Direction.East;
								targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
								lureSpotted++;	
								Debug.Log ("LURESPOOTED: to EAST" + lureSpotted);
								
						}
						if (lastDirection == Direction.East)
							oppositeDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
				}
				if ((Physics.Raycast (centerLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (topLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (bottomLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure")) {
						Debug.DrawRay (transform.position, Vector3.left * 25f, Color.red);
						Debug.DrawRay (abovePos, Vector3.left * 25f, Color.red);
						Debug.DrawRay (belowPos, Vector3.left * 25f, Color.red);
						if (currentDirection == Direction.North) 
								transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);
						if (currentDirection == Direction.South)
								transform.position = new Vector3 (transform.position.x, Mathf.Round (transform.position.y), transform.position.z);				
						targetDistanceX = Mathf.Abs (hit.transform.position.x - transform.position.x);
						targetDistanceY = Mathf.Abs (hit.transform.position.y - transform.position.y);
						if (targetDistance != 0) {
								if (Mathf.Abs (targetDistanceX - targetDistanceY) < Mathf.Floor (targetDistance)) {
										currentDirection = Direction.West;
										targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
										lureSpotted++;	
										Debug.Log ("LURESPOOTED: to WEST" + lureSpotted);
										if (lastDirection == Direction.West)
											oppositeSpotted = true;
								} else if (Mathf.Abs (targetDistanceX - targetDistanceY) == targetDistance) {
										lureSpotted++;	
										Debug.Log ("LURESPOOTED: to WEST" + lureSpotted);
										if (lastDirection == Direction.West && oppositeSpotted)
											oppositeEqual = true;
										if (onArrow)
												currentDirection = arrowDirection;
										else
												currentDirection = lastDirection;
								}
						} else {
								currentDirection = Direction.West;
								targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
								lureSpotted++;	
								Debug.Log ("LURESPOOTED: to WEST" + lureSpotted);
								
						}
						if (lastDirection == Direction.West)
							oppositeDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
			
				}
				if ((Physics.Raycast (centerUpRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (leftUpray, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (rightUpray, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure")) {
						Debug.DrawRay (transform.position, Vector3.up * 25f, Color.red);
						Debug.DrawRay (leftPos, Vector3.up * 25f, Color.red);
						Debug.DrawRay (rightPos, Vector3.up * 25f, Color.red);
						if (currentDirection == Direction.East)
								transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
						if (currentDirection == Direction.West)
								transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
						targetDistanceX = Mathf.Abs (hit.transform.position.x - transform.position.x);
						targetDistanceY = Mathf.Abs (hit.transform.position.y - transform.position.y);
						if (targetDistance != 0) {
								if (Mathf.Abs (targetDistanceX - targetDistanceY) < Mathf.Floor (targetDistance)) { //<-----THIS IS WHERE THE PROBLEM IS	
										Debug.Log("WHY IS THIS GETTING HIT?");
										lureSpotted++;
										Debug.Log ("LURESPOOTED: to NORTH" + lureSpotted);
										if(lastDirection == Direction.North)
											oppositeSpotted = true;
										currentDirection = Direction.North;
										targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
								} else if (Mathf.Abs (targetDistanceX - targetDistanceY) == targetDistance) {	
										lureSpotted++;
										Debug.Log ("LURESPOOTED: to NORTH" + lureSpotted);
										if(lastDirection == Direction.North && oppositeSpotted)
											oppositeEqual = true;								
										if (onArrow) {
												currentDirection = arrowDirection;
										} else {
												currentDirection = lastDirection;
										}
								}
						} else {
								currentDirection = Direction.North;
								targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
								lureSpotted++;	
								Debug.Log ("LURESPOOTED: to NORTH" + lureSpotted);
								
						}
						if(lastDirection == Direction.North)
							oppositeDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
				
			
			
				}
				if ((Physics.Raycast (centerDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (leftDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
						(Physics.Raycast (rightDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure")) {
			
			
			
						Debug.DrawRay (transform.position, Vector3.down * 25f, Color.red);
						Debug.DrawRay (leftPos, Vector3.down * 25f, Color.red);
						Debug.DrawRay (rightPos, Vector3.down * 25f, Color.red);
						if (currentDirection == Direction.East)
								transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
						if (currentDirection == Direction.West)
								transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
			

						targetDistanceX = Mathf.Abs (hit.transform.position.x - transform.position.x);
						targetDistanceY = Mathf.Abs (hit.transform.position.y - transform.position.y);
						
						if (targetDistance != 0) {

								if (Mathf.Abs (targetDistanceX - targetDistanceY) < Mathf.Floor (targetDistance)) {

										currentDirection = Direction.South;
										targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
										lureSpotted++;
										Debug.Log ("LURESPOOTED: to SOUTH" + lureSpotted);	
										if(lastDirection == Direction.South)
											oppositeSpotted = true;
								} else if (Mathf.Abs (targetDistanceX - targetDistanceY) == targetDistance) {
										lureSpotted++;	
										Debug.Log ("LURESPOOTED: to SOUTH" + lureSpotted);	
										if(lastDirection == Direction.South && oppositeSpotted)
											oppositeSpotted = true;
										if (onArrow)
												currentDirection = arrowDirection;
										else {
												currentDirection = lastDirection;
												Debug.Log ("THIS IS WHERE TE PROBLEM IS");
										}
								}
						} else {
								currentDirection = Direction.South;
								targetDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
								lureSpotted++;	
								Debug.Log ("LURESPOOTED: to SOUTH" + lureSpotted);
								
						}
						if(lastDirection == Direction.South)
							oppositeDistance = Mathf.Abs (targetDistanceX - targetDistanceY);
				}

				if (lureSpotted == 2) {
					if(oppositeDistance == targetDistance){
						currentDirection = lastDirection;
						Debug.Log("THIS SHOULD BE HIT!");
					}
					//else
						//currentDirection = arrowDirection;
				}

				if (lureSpotted == 3) {
					if(oppositeDistance <= targetDistance){
						currentDirection = lastDirection;
						Debug.Log("THIS SHOULD BE HIT!");
					}
					else{
						Debug.Log ("SHOULD GO TO ARROW DIRECTION HERE");
						currentDirection = arrowDirection;
					}
				}
		



		
	
		}

		void OnTriggerExit (Collider col)
		{
				//onArrow = false;
		}
	
		void OnTriggerEnter (Collider col)
		{
		
		
				if (col.tag == "Lure") {
						Destroy (col.gameObject);
				}

				/*if(col.tag == "Test")
		{
			//Debug.Log("Hit Swastika");
			string arrowName = col.GetComponent<SpriteRenderer>().sprite.name;
			switch(arrowName)
			{
				//4 way intersections
			case "Phssthpok_32x32_18":
				if(currentDirection == Direction.North) currentDirection = Direction.West;
				if(currentDirection == Direction.East) currentDirection = Direction.North;
				if(currentDirection == Direction.South) currentDirection = Direction.East;
				if(currentDirection == Direction.West) currentDirection = Direction.South;
				break;
				
			case "Phssthpok_32x32_19":
				if(currentDirection == Direction.North) currentDirection = Direction.East;
				if(currentDirection == Direction.East) currentDirection = Direction.South;
				if(currentDirection == Direction.South) currentDirection = Direction.West;
				if(currentDirection == Direction.West) currentDirection = Direction.North;
				break;

			}
		}*/


		}

}