using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Tile")
		{
			Debug.Log("hit");
			Debug.Log("Current Position:" + col.GetComponent<TileScript>().transform.position.x + "," + col.GetComponent<TileScript>().transform.position.y);
		}
	}
}
