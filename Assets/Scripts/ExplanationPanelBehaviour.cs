using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplanationPanelBehaviour : MonoBehaviour {
	public List<harakaSpot> harakat;
	public List<GameObject> harakatPoints;
	public List<harakaSpot> harakatSpots;
	public harakaSpot haraka;
	public int ind;
	public int selectedSpotIndex = -1;
	public int selectedHarakaIndex = -1;
	public int numberOfCorrectHarakat = 0;
	public bool fadeOut = false;
    public GameObject dialogBox;

	// Use this for initialization
	void Start () {
		ind =0;
        GameManager.instance.conversationIsFrozen = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		//harakat.FindLast (t => (t.isTaken == false)/*&& (t.index == ind)*/).isTaken = true;
	}

	public void ShuffleHarakat(){

		foreach (GameObject harakaPoint in harakatPoints) {
			
			//int ind   = harakat.FindLast (t => (t.isTaken == false)/*&& (t.index == ind)*/).index;

			haraka = harakat[harakat.Count - ind - 1];
			ind++;
		
			haraka.isTaken = true;
		
			haraka.harakaPoint.GetComponent<harakaBehaviour> ().Shuffle (harakaPoint.transform);
		}
	} 

	[System.Serializable]
	public struct harakaSpot
	{
		public GameObject harakaPoint;
		public bool isTaken;
		public int index;
		public string type;
	}

	public void spotSelected(int spotIndex)
	{
		if (selectedHarakaIndex != -1) {
			if (harakat[selectedHarakaIndex].type == harakatSpots[spotIndex].type) {
				//Debug.Log (selectedHarakaIndex + " " + spotIndex);
				harakat [selectedHarakaIndex].harakaPoint.GetComponent<harakaBehaviour> ().destination = harakatSpots [spotIndex].harakaPoint.transform.position;
				selectedSpotIndex = -1;//s7i7a
				selectedHarakaIndex = -1;
				numberOfCorrectHarakat++;
				if (numberOfCorrectHarakat == harakatPoints.Count) {
                    
					GetComponent<Animator>().SetTrigger("finishExplanationTrigger");
                    dialogBox.GetComponent<DialogueBoxBehaviour>().Unfreeze();
                  //  Debug.Log ("GGG !!!! ");
				}
			//	print ("s7i7a");
				return;
			} else {
				selectedSpotIndex = -1;//5ata2
				selectedHarakaIndex = -1;
				//print ("5ata2");
			}
		} else {
			if (selectedSpotIndex == -1) {
				selectedSpotIndex = spotIndex;//selection
				print (selectedSpotIndex + " selected");
			} else if (selectedSpotIndex == spotIndex) {
				//deselect spotIndex 
				selectedSpotIndex = -1;
			} else {
				//deselect selectedSpotIndex 
				selectedSpotIndex = spotIndex;
				print (selectedSpotIndex + " replacing");
			}
		}

	
	}
    public void SkipExercice()
    {

        GetComponent<Animator>().SetTrigger("finishExplanationTrigger");
        dialogBox.GetComponent<DialogueBoxBehaviour>().Unfreeze();
    }
	public void harakaSelected(int harakaIndex)
	{
		if (selectedSpotIndex != -1) {
			if (harakat[harakaIndex].type == harakatSpots[selectedSpotIndex].type) {
				Debug.Log (harakaIndex + " " + selectedSpotIndex);
				harakat [harakaIndex].harakaPoint.GetComponent<harakaBehaviour> ().destination = harakatSpots [selectedSpotIndex].harakaPoint.transform.position;
				selectedHarakaIndex = -1;//s7i7a
				selectedSpotIndex = -1;
				numberOfCorrectHarakat++;
				if (numberOfCorrectHarakat == harakatPoints.Count) {
                    
                    GetComponent<Animator>().SetTrigger("finishExplanationTrigger");
                    dialogBox.GetComponent<DialogueBoxBehaviour>().Unfreeze();
                }
				//print ("s7i7a");
				return;
			} else {
				selectedHarakaIndex = -1;//5ata2
				selectedSpotIndex = -1;
				//print ("5ata2");
			}
		} else {
			if (selectedHarakaIndex == -1) {
				selectedHarakaIndex = harakaIndex;//selection
				//print (selectedHarakaIndex + " selected");
			} else if (selectedHarakaIndex == harakaIndex) {
				//deselect spotIndex 
				selectedHarakaIndex = -1;
			} else {
				//deselect selectedSpotIndex 
				selectedHarakaIndex = harakaIndex;
				//print (selectedHarakaIndex + " replacing");
			}
		}


	}

	public void Inactivate(){

		gameObject.SetActive (false);
	}
}
