using UnityEngine;
using System.Collections;

public class HarakaBehaviour : MonoBehaviour {
	public bool isLerping = false;

	public Vector2 destination;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLerping) {
			transform.position = Vector2.Lerp (transform.position,destination,0.005f);
		}
	}

	public void Shuffle(Transform harakaPoint){
		switch (gameObject.name) {
		case "fatha": 
			destination = harakaPoint.GetChild(0).position;
			break;
		case "dhamma": 
			destination = harakaPoint.GetChild(0).position;
			break;
		case "kasra": 
			destination = harakaPoint.GetChild(1).position;
			break;
		
		default:
			break;
		}
		//destination = harakaPoint.GetComponentInChildren<Transform>()[0];
		isLerping = true;
	}
}
