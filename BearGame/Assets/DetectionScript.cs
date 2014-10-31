using UnityEngine;
using System.Collections;

public class DetectionScript : MonoBehaviour {

	public enum DetectedTile {Empty, Wall};
	public DetectedTile detected = DetectedTile.Empty;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Tile")
		{
			Debug.Log("Detected Empty Tile");
			detected = DetectedTile.Empty;
		}

		if (col.tag == "Wall")
		{
			Debug.Log("Detected Solid Tile");
			detected = DetectedTile.Wall;
		}
	}
}
