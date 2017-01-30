using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Collections.Generic; 
[System.Serializable]
public class itemExerciceGRocery {
	public GameObject Plaque; 
	public GameObject PanelGrandePlaque; 
	public GameObject GrandePlaque; 
	public GameObject MissingLetterPlaque; 
	public GameObject MissingLetterGrandePlaque; 
	public Sprite CorrectGameObject; 
	public int idCorrectSprite; 
	public GameObject ButtonPlaque; 
	public GameObject[] ButtonLetters; 
	public bool isCorrect; 
	public int idGoodLetter;

    public void InitGame(){
		Plaque.SetActive (true); 
		PanelGrandePlaque.SetActive (false); 
		MissingLetterPlaque.SetActive (false); 
		ButtonPlaque.SetActive (true); 
		isCorrect = false; 
		for (int i = 0; i < ButtonLetters.Length; i++) {

			ButtonLetters [i].SetActive (false); 

		}
	}
	public void SetGameItem(List<Sprite> randomSprite, GameObject plaque){
		GrandePlaque = plaque;
        isCorrect = false; 
		GrandePlaque.SetActive (true); 
		PanelGrandePlaque.SetActive (true); 
		MissingLetterGrandePlaque.SetActive (false); 
		ButtonPlaque.SetActive (false); 
		int j = 0; 
		idGoodLetter = Random.Range (0, 3);

		for (int i = 0; i < ButtonLetters.Length; i++) {
			if (i == idGoodLetter) {
			
				ButtonLetters [i].GetComponent<Image> ().sprite = CorrectGameObject; 
			} else {
			
				ButtonLetters [i].GetComponent<Image> ().sprite = randomSprite[j]; 
				j++; 
			}
			ButtonLetters [idGoodLetter].GetComponentInParent<Animator>().SetBool ("Help",false); 
			ButtonLetters [i].SetActive (true); 
		
		}

	}
	public void DesactiveButton(){
		ButtonPlaque.SetActive (false); 

	}
	public void ReactiveButton(){
		ButtonPlaque.SetActive (true); 
	
	}
	public bool CheckAnswer(int id){
		if (id == idGoodLetter) {
			for (int i = 0; i < ButtonLetters.Length; i++) {

				ButtonLetters [i].SetActive (false); 
				MissingLetterGrandePlaque.SetActive (true); 
			}

            isCorrect = true;
            return true;
		} else {
			ButtonLetters [idGoodLetter].GetComponentInParent<Animator>().SetBool ("Help",true); 
			return false;
		
		}
	
	
	}
	
    public void EndExercice()
    {
        Plaque.SetActive(true);
        PanelGrandePlaque.SetActive(false);
        MissingLetterPlaque.SetActive(true);
        ButtonPlaque.SetActive(false);

    }

}
public class ExerciceGrocery : MonoBehaviour {
	public 	List<itemExerciceGRocery> ListItems = new List<itemExerciceGRocery> (); 
	public List<Sprite> allSpritesButton;
    public GameObject ReplayPanel;
    void Start(){

        StartGame(); 

	}

    public DialogueBoxBehaviour dialogueScript;
   // public Speach StartGameSprite;
    public Speach GoodJob;
    public Speach WrongAnswer;
    public Speach EndGameSprite;
    public void StartGame(){

        for (int i = 0; i < ListItems.Count; i++) {
			ListItems [i].InitGame (); 
		
		
		}
		for (int i = 0; i < buttonsToDesactive.Length; i++) {

			buttonsToDesactive [i].SetActive (false); 

		}
	
	}
	int idGame=-1; 
    
	public void SetGame(int id){
        StopCoroutine("waitBeforDesactive");
        if (idGame != -1)
        {
            ListItems[idGame].PanelGrandePlaque.SetActive(false); 
        }
      //  Debug.Log("Set Game " + id); 
      
        idGame = id; 
		for (int i = 0; i < buttonsToDesactive.Length; i++) {

			buttonsToDesactive [i].SetActive (true); 

		}
		List<int> tempRandomIndex = new List<int> (); 
		List<Sprite> randomSprites = new List<Sprite> (); 
		List<Sprite> tempSprites = new List<Sprite> (allSpritesButton ); 
	

		tempSprites.Remove (allSpritesButton [ListItems [id].idCorrectSprite]);

		for (int i = 0; i < 2; i++) {
			int random = 0;
			do {

				random= Random.Range(0, tempSprites.Count); 

			} while(tempRandomIndex.Exists (o => o == random)); 
			//Debug.Log (random); 

			randomSprites.Add (tempSprites [random]); 
		
		}
		//Debug.Log (id); 
		ListItems [id].SetGameItem(randomSprites, Plaque); 

	}
	public GameObject Plaque; 
	public GameObject[] buttonsToDesactive;
    public IEnumerator waitBeforDesactive()
    {

        yield return new WaitForSeconds(2);
        ListItems[idGame].EndExercice();
        Plaque.SetActive(false);
        if (!ListItems.Exists(o => !o.isCorrect))
        {
            ReplayPanel.SetActive(true);
        }

    }
    public void ClickedOnButton(int id){
		if (ListItems [idGame].CheckAnswer (id)) {
	     	StartCoroutine("waitBeforDesactive"); 
			for (int i = 0; i < buttonsToDesactive.Length; i++) {
			
				buttonsToDesactive [i].SetActive (false); 
			
			}

          
            if (!ListItems.Exists(o => !o.isCorrect))
            {

                dialogueScript.Cheering(EndGameSprite);
            
             //   GameManager.instance.conversationIsFrozen = false;
            }
            else
            {
                dialogueScript.Cheering(GoodJob);
               // GameManager.instance.conversationIsFrozen = false;

            }

        }
        else{
            dialogueScript.Cheering(WrongAnswer);
           // GameManager.instance.conversationIsFrozen = false;

        }


	}
    public void EndGame()
    {


    }
}
