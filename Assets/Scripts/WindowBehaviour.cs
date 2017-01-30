using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowBehaviour : MonoBehaviour {
	public Sprite alternateStateSprite;
	public GameObject companion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenWindow()
	{
	//	Debug.Log ("azer");
		gameObject.GetComponent<Image> ().sprite = alternateStateSprite;
		gameObject.GetComponent<Image> ().SetNativeSize();


	}
}
