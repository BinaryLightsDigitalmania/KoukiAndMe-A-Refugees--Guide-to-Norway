using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public List<Speach> DesignedSpeach = new List<Speach>();
    void Awake()
    {
       // if (instance == null)
            instance = this;
      /*  else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        */

    }
    void InitDialogue()
    {
        DialogueBoxBehaviour.textIndex = 0;
        DialogueBoxBehaviour.welcomeIndex = 0;
        DialogueBoxBehaviour.moodIndex = 0;
        DialogueBoxBehaviour.explanationsIndex = 0;
        DialogueBoxBehaviour.currentExplanationIndex = 0;
    }
    void Start() {
        StartGame();
        InitDialogue();

    }
    public void StartGame() {
        RoomScene();
    }
    public void RoomScene() {
        HudController.instance.DisplayRoom();
        HintOpenWindow();
    }
    public void HintOpenWindow() {

    }
    public void OpenWindow() {


    }
    public void StartExplanation() {


    }
    //start one of the three exercices
    public void ExerciceExplanation(int idExplanation) {


    }
    //wrong answer 
    public void HelpExplanation(int idExplanation) {


    }
    //spent too much time hanging 
    public void Knocking()
    {


    }
    //good answer 
    public void WinExplanation(int idExplanation) {


    }
    //show shop explanation
    public void RewardExplanation(int idExplanation) {


    }
    public void EndEplanation() {



    }
    //start the whole room exercice 
    public void StartExercice(int idExercice) {


    }
    //start a question from the exercice
    public void StartQuestion(int idQuestion) {


    }
    //wrong answer 
    public void HelpQuestion(int idQuestion)
    {


    }
    //good answer 
    public void WinQuestion(int idQuestion)
    {


    }
    //show shop explanation
    public void RewardQuestion(int idQuestion)
    {


    }
    //passer a la prochaine question ou finir l'exercice
    public void EndQuestion(int idQuestion)
    {



    }
    //finir l'exercice et gagner un trophé + if room gagner un bouton optimisation de la chambre
    public void EndExercice(int idExercice) {


    }
    //win a reward whenFinish Game
    public void RewardTrophe(int idTrophe) {


    }
    //win a decorButton
    public void WinDecorRoomButton() {


    }
    //when clicked on an item Change the Decor / affiche Confirmation horse
    public void ChangeDecor(int idItem) {


    }
    //when Buy Item buy with stars/ Save in preferences
    public void BuyDecor(int idItem) {


    }
    //when finish room scene Play : horse it smells good 
    public void FinishRoomScene() {


    }



    //public AudioSource audio; 

	public  List<Speach> welcomeSpeech;
	public  List<Speach> moodSpeech;
	[SerializeField]
	public List<ExplanationSpeachList> explanations;

	public List<PlotTwist> scenario;



	[System.Serializable]
	public struct ExplanationSpeachList
	{
		public 	List<Speach> explanation;
	}
    public int ActualScenario;
    public List<GameObject> ContextualPanels;
	public bool conversationIsFrozen = false;

}
[System.Serializable]
public class PlotTwist
{
    public int scenarioPoint;
    public int speachTypeId;
}
