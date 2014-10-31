using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public enum Direction{North, East, South, West};
	public Direction currentDirection = Direction.East;
	public Direction lastDirection = Direction.East;
	public Direction arrowDirection = Direction.East;
	public float speed = .1f;
	
	public float lureValue = 100f;
	
	public float targetDistanceX = 0;
	public float targetDistanceY = 0;
	public float targetDistance = 0;
	public int lureSpotted = 0;
	
	public bool onArrow = false;

//	public GameObject childN;
//	public GameObject childE;
//	public GameObject childS;
//	public GameObject childW;

	// Use this for initialization
	void Start () {
		//Find Children
//		childN = GameObject.FindGameObjectWithTag("ChildN").gameObject;
//		childE = GameObject.FindGameObjectWithTag("ChildE").gameObject;
//		childS = GameObject.FindGameObjectWithTag("ChildS").gameObject;
//		childW = GameObject.FindGameObjectWithTag("ChildW").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//Reset Variables
		lureSpotted = 0;
		targetDistanceX = 0;
		targetDistanceY = 0;
		targetDistance = 0;
		lastDirection = currentDirection;

		//Movement
		switch (currentDirection)
		{
		case Direction.East:
			
			transform.Translate(Vector3.right * Time.deltaTime * speed);
			break;

		case Direction.North:
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			break;

		case Direction.South:
			transform.Translate(Vector3.down * Time.deltaTime * speed);
			break;

		case Direction.West:
			transform.Translate(Vector3.left * Time.deltaTime * speed);
			break;
		}

		//Raycasting
		RaycastHit hit;
		
		Vector3 abovePos = new Vector3(transform.position.x, transform.position.y + .49f, transform.position.z);
		Vector3 belowPos = new Vector3(transform.position.x, transform.position.y - .49f, transform.position.z);
		Vector3 leftPos = new Vector3(transform.position.x - .49f, transform.position.y, transform.position.z);
		Vector3 rightPos = new Vector3(transform.position.x + .49f, transform.position.y, transform.position.z);
		
		Ray centerUpRay = new Ray(transform.position, Vector3.up);
		Ray leftUpray = new Ray(leftPos, Vector3.up);
		Ray rightUpray = new Ray(rightPos, Vector3.up);
		
		Ray centerRightRay = new Ray(transform.position, Vector3.right);
		Ray topRightRay = new Ray(abovePos, Vector3.right);
		Ray bottomRightRay = new Ray(belowPos, Vector3.right);
		
		Ray centerDownRay = new Ray(transform.position, Vector3.down);
		Ray leftDownRay = new Ray(leftPos, Vector3.down);
		Ray rightDownRay = new Ray(rightPos, Vector3.down);
		
		Ray centerLeftRay = new Ray(transform.position, Vector3.left);
		Ray topLeftRay = new Ray(abovePos, Vector3.left);
		Ray bottomLeftRay = new Ray(belowPos, Vector3.left);
		
		
		//NOTE TO SELF, USE REVERSE RAY FOR DETECTING THE OBJECTS!!!!
		Ray revUpRay = new Ray (abovePos, Vector3.down);
		Ray revDownRay = new Ray (belowPos, Vector3.up); 
		Ray revRightRay = new Ray (rightPos, Vector3.left);
		Ray RevLeftRay = new Ray (leftPos, Vector3.right);
		
		Debug.DrawRay(abovePos, Vector3.down * .5f, Color.cyan);
		

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
		if(currentDirection == Direction.East && Physics.Raycast( centerRightRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall")
		{
			
			currentDirection = arrowDirection;
			if(Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.South;
			if(Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.North; 
			if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls)) 
				currentDirection = Direction.West;
			transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);		
		}

		if(currentDirection == Direction.South && Physics.Raycast( centerDownRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall")
		{
			
			currentDirection = arrowDirection;
			if(Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.West;
			if(Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.East;
			if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls)) 
				currentDirection = Direction.North;
			transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);	
		}

		if(currentDirection == Direction.West && Physics.Raycast( centerLeftRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall")
		{
				
			currentDirection = arrowDirection;
			if(Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.North;
			if(Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.South;
			if (Physics.Raycast (centerUpRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerDownRay, out hit, .75f, layerMaskWalls)) 
				currentDirection = Direction.East;
			transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		}

		if(currentDirection == Direction.North && Physics.Raycast( centerUpRay, out hit, .5f, layerMaskWalls) && hit.collider.tag == "Wall")
		{	
				
			currentDirection = arrowDirection;
			if(Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.East;
			if(Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls))
				currentDirection = Direction.West;
			if (Physics.Raycast (centerRightRay, out hit, .75f, layerMaskWalls) && Physics.Raycast (centerLeftRay, out hit, .75f, layerMaskWalls)) 
				currentDirection = Direction.South;
			transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
		}

		if((Physics.Raycast(centerRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(topRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(bottomRightRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure"))
		{

			
			lureSpotted++;	
			Debug.DrawRay(transform.position, Vector3.right * 25f, Color.red);
			Debug.DrawRay(abovePos, Vector3.right * 25f, Color.red);
			Debug.DrawRay(belowPos, Vector3.right * 25f, Color.red);
				
			if (currentDirection == Direction.North) 
				transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
			if (currentDirection == Direction.South)
				transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
			
			targetDistanceX = Mathf.Abs(hit.transform.position.x - transform.position.x);
			targetDistanceY = Mathf.Abs(hit.transform.position.y - transform.position.y);
				
			if (targetDistance != 0)
			{
				if (Mathf.Abs(targetDistanceX - targetDistanceY) < Mathf.Floor(targetDistance))
				{
					currentDirection = Direction.East;
					targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
				}
				else if (Mathf.Abs(targetDistanceX - targetDistanceY) == targetDistance)
				{
					if (onArrow)
						currentDirection = arrowDirection;
					else
						currentDirection = lastDirection;
				}
			}
			else
			{
				currentDirection = Direction.East;
				targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
			}
		
		}
		if((Physics.Raycast(centerLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(topLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(bottomLeftRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure"))
		{

				
			Debug.DrawRay(transform.position, Vector3.left * 25f, Color.red);
			Debug.DrawRay(abovePos, Vector3.left * 25f, Color.red);
			Debug.DrawRay(belowPos, Vector3.left * 25f, Color.red);
			if (currentDirection == Direction.North) 
				transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
			if (currentDirection == Direction.South)
				transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
				
			targetDistanceX = Mathf.Abs(hit.transform.position.x - transform.position.x);
			targetDistanceY = Mathf.Abs(hit.transform.position.y - transform.position.y);
			if (targetDistance != 0)
			{
				if (Mathf.Abs(targetDistanceX - targetDistanceY) < Mathf.Floor(targetDistance))
				{
					currentDirection = Direction.West;
					targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
				}
				else if (Mathf.Abs(targetDistanceX - targetDistanceY) == targetDistance)
				{
					if (onArrow)
						currentDirection = arrowDirection;
					else
						currentDirection = lastDirection;
				}
			}
			else
			{
				currentDirection = Direction.West;
				targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
			}
			
		}
		if((Physics.Raycast(centerUpRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(leftUpray, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(rightUpray, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure"))
		{
			Debug.DrawRay(transform.position, Vector3.up * 25f, Color.red);
			Debug.DrawRay(leftPos, Vector3.up * 25f, Color.red);
			Debug.DrawRay(rightPos, Vector3.up * 25f, Color.red);
			if (currentDirection == Direction.East)
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
			if (currentDirection == Direction.West)
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
			
			lureSpotted++;
			
			
			targetDistanceX = Mathf.Abs(hit.transform.position.x - transform.position.x);
			targetDistanceY = Mathf.Abs(hit.transform.position.y - transform.position.y);
			
			
			if (targetDistance != 0)
			{
				if (Mathf.Abs(targetDistanceX - targetDistanceY) < Mathf.Floor(targetDistance))
				{	
					currentDirection = Direction.North;
					targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
				}
				else if (Mathf.Abs(targetDistanceX - targetDistanceY) == targetDistance)
				{	
					if (onArrow)
						currentDirection = arrowDirection;
					else
						currentDirection = lastDirection;
				}
			}
			else
			{
				currentDirection = Direction.North;
				targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
			}
			
				
			
			
		}
		if((Physics.Raycast(centerDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(leftDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure") &&
			(Physics.Raycast(rightDownRay, out hit, Mathf.Infinity, finalMask) && hit.collider.tag == "Lure"))
		{
			
			
			
			Debug.DrawRay(transform.position, Vector3.down * 25f, Color.red);
			Debug.DrawRay(leftPos, Vector3.down * 25f, Color.red);
			Debug.DrawRay(rightPos, Vector3.down * 25f, Color.red);
			if (currentDirection == Direction.East)
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
			if (currentDirection == Direction.West)
				transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
			
			lureSpotted++;	
				
			targetDistanceX = Mathf.Abs(hit.transform.position.x - transform.position.x);
			targetDistanceY = Mathf.Abs(hit.transform.position.y - transform.position.y);
			
			if (targetDistance != 0)
			{
				if (Mathf.Abs(targetDistanceX - targetDistanceY) < Mathf.Floor(targetDistance))
				{
					currentDirection = Direction.South;
					targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
				}
				else if (Mathf.Abs(targetDistanceX - targetDistanceY) == targetDistance)
				{
					if (onArrow)
						currentDirection = arrowDirection;
					else
						currentDirection = lastDirection;
				}
			}
			else
			{
				currentDirection = Direction.South;
				targetDistance = Mathf.Abs(targetDistanceX - targetDistanceY);
			}
		
		}
		
		if( //(Physics.Raycast(centerDownRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			//(Physics.Raycast(leftDownRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			//(Physics.Raycast(rightDownRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(centerUpRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(leftUpray, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(rightUpray, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			//(Physics.Raycast(centerLeftRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			//(Physics.Raycast(topLeftRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			//(Physics.Raycast(bottomLeftRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(centerRightRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(topRightRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow") &&
			(Physics.Raycast(bottomRightRay, out hit, Mathf.Infinity, finalMask2) && hit.collider.tag == "Arrow"))
		{
			onArrow = true;
			Debug.Log("On Arrow");
		}
		else
		{
			onArrow = false;
			Debug.Log("Off Arrow");
		}
		
	
	}

	void OnTriggerExit(Collider col)
	{
		//onArrow = false;
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		
		if(col.tag == "Lure")
		{
			Destroy(col.gameObject);
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

		if(col.tag == "Arrow" && onArrow == true)
		{
			//onArrow = true;
			//Debug.Log("Hit Arrow");
			string arrowName = col.GetComponent<SpriteRenderer>().sprite.name;
			switch(arrowName)
			{
				//4 way intersections
			case "Phssthpok_32x32_18":
				if(currentDirection == Direction.North) {currentDirection = Direction.West;break;}
				if(currentDirection == Direction.East) {currentDirection = Direction.North;break;}
				if(currentDirection == Direction.South) {currentDirection = Direction.East;break;}
				if(currentDirection == Direction.West) {currentDirection = Direction.South;}
				break;
				
			case "Phssthpok_32x32_19":
				if(currentDirection == Direction.North) {currentDirection = Direction.East;break;}
				if(currentDirection == Direction.East) {currentDirection = Direction.South;break;}
				if(currentDirection == Direction.South) {currentDirection = Direction.West;break;}
				if(currentDirection == Direction.West) {currentDirection = Direction.North;}
				break;

				//3 way intersections
			case "Phssthpok_32x32_20":
				arrowDirection = Direction.South;
				break;

			case "Phssthpok_32x32_21":
				arrowDirection = Direction.North;
				break;

			case "Phssthpok_32x32_22":
				arrowDirection = Direction.West;
				break;

			case "Phssthpok_32x32_23":
				arrowDirection = Direction.East;
				break;

			case "Phssthpok_32x32_24":
				arrowDirection = Direction.North;
				break;

			case "Phssthpok_32x32_25":
				arrowDirection = Direction.South;
				break;

			case "Phssthpok_32x32_26":
				arrowDirection = Direction.East;
				break;

			case "Phssthpok_32x32_27":
				arrowDirection = Direction.West;
				break;

			}
		}
	}

}