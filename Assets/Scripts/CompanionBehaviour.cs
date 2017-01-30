using UnityEngine;
using System.Collections;

public class CompanionBehaviour : MonoBehaviour {
	public GameObject dialogueBox;
	public float timeToBlink = 0;

	public GameObject endEntryPath;
	public GameObject startEntryPath;
    public void ChangeEntry(GameObject _startEntryPath , GameObject _endEntryPath )
    {
        endEntryPath = _endEntryPath;
        startEntryPath = _startEntryPath;
        GetComponent<Animator>().SetTrigger("Enter"); 
    }
	public bool isInEntryAnimation = false;
    public GameObject fullComp;
    // Use this for initialization
    void Start () {
		//transform.position = startEntryPath.transform.position;
		timeToBlink = Random.Range (0.5f, 5f);
	}
	
	// Update is called once per frame
	void Update () {

		if (isInEntryAnimation && endEntryPath != null && startEntryPath != null ) {
			//transform.position = Vector2.Lerp (startEntryPath.transform.position,endEntryPath.transform.position,Mathf.Abs(startEntryPath.transform.position.x - endEntryPath.transform.position.x) / 100 * 5);
			transform.position = Vector2.MoveTowards(transform.position,endEntryPath.transform.position,Mathf.Abs(startEntryPath.transform.position.x - endEntryPath.transform.position.x) / 100 * 5);
		}

		timeToBlink -= Time.deltaTime;
		if (timeToBlink <= 0) {
			gameObject.GetComponent<Animator>().SetTrigger ("blinkTrigger");
			timeToBlink = Random.Range (0.5f, 5f);
		}


	
	}

	public void InitiateDialogue()
	{
		dialogueBox.GetComponent<Animator> ().SetTrigger ("initDialogueTrigger");
	}

	public void InitEntryMotion()
	{
		isInEntryAnimation = true;
	}
    public void SlideDown() {


    }
    public void Inactivate() {
        fullComp.SetActive(true);
        this.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
    }
}
