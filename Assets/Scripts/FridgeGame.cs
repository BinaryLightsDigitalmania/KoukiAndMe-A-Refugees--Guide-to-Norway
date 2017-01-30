using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

[System.Serializable] 
public class ExerciceHarakaKitchen{
	public GameObject Panel;
	public GameObject haraka; 
	public GameObject[] harakat; 
	public GameObject Button;
	public GameObject PositionOnTheTable; 
	public int idCorrectHaraka;
    public bool isDone; 
	public void InitExercice(){
		Panel.SetActive (false); 
		Button.SetActive (true); 
		PositionOnTheTable.SetActive (false); 
	}
	public void SetExercice(){
		Panel.SetActive (true); 
		haraka.SetActive (false);
		Button.SetActive (false); 
		PositionOnTheTable.SetActive (false); 
		for (int i = 0; i < harakat.Length; i++) {
			harakat [i].GetComponent<Animator> ().SetBool ("HarakaHint", false); 
			harakat [i].SetActive (true); 
		}
	}
	public bool CheckCorrectExercice(int id){

		if (id != idCorrectHaraka) {

			harakat [idCorrectHaraka].GetComponent<Animator> ().SetBool ("HarakaHint", true); 
		
		} else {
            isDone = true; 
			haraka.SetActive (true);
            for (int i = 0; i < harakat.Length; i++)
            {
                harakat[i].GetComponent<Animator>().SetBool("HarakaHint", false);
                harakat[i].SetActive(false);
            }
        }
		return (id == idCorrectHaraka);
	}
public void DesactivePanel(){
		
		Panel.SetActive (false); 
		PositionOnTheTable.SetActive (true); 
	}
}
public class FridgeGame : MonoBehaviour {
	public List <ExerciceHarakaKitchen> exercices = new List<ExerciceHarakaKitchen>();
	public GameObject PanelGame;
    public GameObject arrowTriggerFridgeOpen;
    public GameObject arrowTriggerFridgeClosed;
   
    void Start(){

		StartGame (); 

	}
	public void StartGame(){
	// hint fridge 
		for (int i = 0; i < exercices.Count; i++) {
		
			exercices [i].InitExercice (); 
		
		}
	
	}
	public void OpenedFridge(){

        arrowTriggerFridgeClosed.SetActive(false);
        arrowTriggerFridgeOpen.SetActive(true); 
    }
	int actualIdExercice; 
	public void StartExercice(int id){
        arrowTriggerFridgeOpen.SetActive(false);
        GameManager.instance.conversationIsFrozen = true;
        PanelGame.SetActive (true); 
		actualIdExercice = id; 
		exercices[id].SetExercice(); 
	
	
	}
  public   DialogueBoxBehaviour dialogueScript;
    public Speach GoodJob;
    public Speach WrongAnswer;
    public Speach EndGameSprite;
    public void ClickedButton(int id)
    {

        if (exercices[actualIdExercice].CheckCorrectExercice(id))
        {
            dialogueScript.Cheering(GoodJob);
            GameManager.instance.conversationIsFrozen = true;
            StartCoroutine(WaitBeforDesactive(actualIdExercice));

        }
        else
        {
            dialogueScript.Cheering(WrongAnswer);
            GameManager.instance.conversationIsFrozen = true;

        }


    }
    public float TimeToWaitBeforDesactive=2;
    IEnumerator WaitBeforDesactive(int id) {
        yield return new WaitForSeconds(TimeToWaitBeforDesactive);
        PanelGame.SetActive(false);
        exercices[id].DesactivePanel();
        arrowTriggerFridgeOpen.SetActive(true);
        if (!exercices.Exists(o => !o.isDone))
        {
            EndGame();
         
        }
	}
    public void EndGame() {
       dialogueScript.Cheering(EndGameSprite);
        HudController.instance.ExerciceMemory(); 
          arrowTriggerFridgeOpen.SetActive(false);
    }
   
}
