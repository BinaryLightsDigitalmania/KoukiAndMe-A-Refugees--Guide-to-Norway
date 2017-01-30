using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public enum Language
{
    arabic=1, 
    Norwegian=0
}
public enum TypeAction
{
    pickMood, 
    pickColor, 

    nothing ,   OpenCloset,
}
[System.Serializable]
public class Dialogue
{
    public Language typeLanguage;
    public string dialogueTextNor;
    public AudioClip dialogueClipNor;
    public string dialogueTextAr;
    public AudioClip dialogueClipAr;
  public WaitForNext WaitBeforNextDialogue;
}
[System.Serializable]
public class WaitForNext {

   public bool haveToWait=false;
   public float SecondsToWait;

}
[System.Serializable]
public class MoveBetweenPoints{

    public   GameObject StartPoint;
    public   GameObject EndPoint; 

}
[System.Serializable]
public class comportement
{
    public int Index;
    public typeDialogBehaviour TypeDialogue;
    public TypeKoukiBehaviour TypeKouki;
    public TypeAction typeAction;
    public List<Dialogue> DialogueList;
    public WaitForNext WaitBeforNextComportement;
  
    public MoveBetweenPoints MoveBetweenPoints;
}

public class ScenarioController : MonoBehaviour {
    public List<comportement> ComportementBehaviourStart;
    public comportement comportementWhenTouched;
    public KoukiBehaviour koukiScript;
    public BubbleTalkingBehaviour BubbleScript;

    int ActualIndex;
    int ActualIndexDialogue;

    // Use this for initialization
    void Start() {
        ActualIndex = 0;
        StartComportementWelcomeBehaviour();
    }
    public void StartComportementWelcomeBehaviour() {
        ActualIndex = 0;
        isStartComportement = true;
        PlayComportement();

    }
    public void PlayComportement()
    {
        comportement actualComportement = new comportement();
        if (ActualIndex < ComportementBehaviourStart.Count)
        {
            if (isStartComportement)
            {
                actualComportement = ComportementBehaviourStart[ActualIndex];
            }
                  ActualIndexDialogue = 0;
            switch (actualComportement.TypeDialogue)
            {
                case typeDialogBehaviour.StartSpeaking:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                        actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                    koukiScript.Speak();
                    break;
                case typeDialogBehaviour.SpeakNextAnimation:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                       actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                    koukiScript.Speak();
                    break;
                case typeDialogBehaviour.OpenQuestionWithButton:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                      actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                    koukiScript.Speak();
                    break;
                case typeDialogBehaviour.CloseAnimaton:
                    BubbleScript.CloseAnimation();
                    break;
                default:
                    break;
            }
            switch (actualComportement.TypeKouki)
            {
                case TypeKoukiBehaviour.SlideEnter:
                    koukiScript.SlideEnter(actualComportement.MoveBetweenPoints.EndPoint, actualComportement.MoveBetweenPoints.StartPoint);
                    break;
                case TypeKoukiBehaviour.Cheer:
                    koukiScript.Cheer();
                    break;
                case TypeKoukiBehaviour.EndAskQuestion:

                    koukiScript.EndAsking();

                    break;
                case TypeKoukiBehaviour.askQuestion:
                    koukiScript.AskQuestion();
                    break;
                case TypeKoukiBehaviour.laugh:
                    koukiScript.Laugh();
                    break;
                case TypeKoukiBehaviour.Wave:
                    koukiScript.WaveAnimation();
                    break;
                default:

                    break;
            }
     
            if (actualComportement.DialogueList != null)
            {
                if ((actualComportement.DialogueList.Count - 1) > ActualIndexDialogue)
                {
                    PlayDialogue();

                } else if (actualComportement.WaitBeforNextComportement.haveToWait)
            {
                PlayComportementNext();

            }
            }
           
            switch (actualComportement.typeAction)
            {
                case TypeAction.pickColor:
                    ShowColorPicker();
                    break;
                case TypeAction.pickMood:
                    ShowMoodPicker();
                    break;
                case TypeAction.OpenCloset:
                  
                    break;
                default:
                    break;
            }
        }

    }
    int actualIndexLanguage = 1;
    public void ChangeLanguage() {
        if (actualIndexLanguage == 1)
        {
            actualIndexLanguage = 0;
        }
        else {
            actualIndexLanguage = 1;
        }
        ActualIndexDialogue = 0;
      
            PlayDialogue();
      
    }
   
    public bool isActions; 
    public void PlayDialogue()
    {
        List<Dialogue> DialogueList = new List<Dialogue>();
        
        if (isStartComportement)
        {if (isActions)
            {
                if (idAction == 0)
                {
                    DialogueList = ComportementBehaviourActionMood[idChoice].DialogueList;

                }
                else if (idAction == 1)
                {

                    DialogueList = ComportementBehaviourColor[idChoice].DialogueList;
                }

            }
            else
            {
                DialogueList = ComportementBehaviourStart[ActualIndex].DialogueList;
            }
        }
        if (actualIndexLanguage == 1)
        {
            BubbleScript.StartSpeaking(DialogueList[ActualIndexDialogue].dialogueTextNor,
                      DialogueList[ActualIndexDialogue].dialogueClipNor,false);
        }
        else
        {
            BubbleScript.StartSpeaking(DialogueList[ActualIndexDialogue].dialogueTextAr,
                             DialogueList[ActualIndexDialogue].dialogueClipAr,true);

        }
        koukiScript.Speak();
        ActualIndexDialogue++;
        if ((ActualIndexDialogue < DialogueList.Count) && (DialogueList[ActualIndexDialogue].WaitBeforNextDialogue.haveToWait))
        {
            StartCoroutine(WaitBeforDisplayNextDialogue(DialogueList[ActualIndexDialogue].WaitBeforNextDialogue.SecondsToWait));



        }
        else if (ActualIndexDialogue >= DialogueList.Count)
        {

            PlayComportementNext(); 

        }
    }
    bool isStartComportement;
    public void PlayComportementNext()
    {

        ActualIndex++;
        if ((isStartComportement) && (ActualIndex < ComportementBehaviourStart.Count) && (ComportementBehaviourStart[ActualIndex].WaitBeforNextComportement.haveToWait))
        {
            StartCoroutine(WaitBeforNextComportement(ComportementBehaviourStart[ActualIndex].WaitBeforNextComportement.SecondsToWait));
        }
        else if ((isStartComportement) && (ActualIndex < ComportementBehaviourStart.Count) && (!ComportementBehaviourStart[ActualIndex].WaitBeforNextComportement.haveToWait))
        {
            isActions = false;
            PlayComportement();
        }
        else if ((isStartComportement) && (ActualIndex > ComportementBehaviourStart.Count)) {

            print("EndComportement");
        }

    }
    IEnumerator WaitBeforDisplayNextDialogue(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        PlayDialogue();

    }
    IEnumerator WaitBeforNextComportement(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isActions = false;
        if (isStartComportement)
        {

            PlayComportement();
        }

    }
    public GameObject MoodBehaviour;
    public GameObject ColorBehaviour;
    public void ShowMoodPicker() {
        MoodBehaviour.SetActive(true);
    }
    public void ShowColorPicker() {
        ColorBehaviour.SetActive(true);
    }
    int idAction;
    public void ChooseMood(int MoodChosen)
    {
        print("Mood "
            + MoodChosen);
        idAction = 0;
        isActions = true; 
        idChoice = MoodChosen;
        ActualIndexDialogue = 0; 
        PlayComportementAfterAction();
    }
    int idChoice;
    public List<comportement> ComportementBehaviourActionMood = new List<comportement>();
    public List<comportement> ComportementBehaviourColor= new List<comportement>();
    public void PlayComportementAfterAction()
    {
        comportement actualComportement = new comportement();
        if (idAction == 0)
        {
            actualComportement = ComportementBehaviourActionMood[idChoice];

        }else if(idAction == 1)
        {

 actualComportement = ComportementBehaviourColor[idChoice];
        }

        switch (actualComportement.TypeDialogue)
        {
            case typeDialogBehaviour.StartSpeaking:
                BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                    actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                koukiScript.Speak();
                break;
            case typeDialogBehaviour.SpeakNextAnimation:
                BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                   actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                koukiScript.Speak();
                break;
            case typeDialogBehaviour.OpenQuestionWithButton:
                BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualIndexDialogue].dialogueTextNor,
                  actualComportement.DialogueList[ActualIndexDialogue].dialogueClipNor, false);
                koukiScript.Speak();
                break;
            case typeDialogBehaviour.CloseAnimaton:
                BubbleScript.CloseAnimation();
                break;
            default:
                break;
        }
        switch (actualComportement.TypeKouki)
        {
            case TypeKoukiBehaviour.SlideEnter:
                koukiScript.SlideEnter(actualComportement.MoveBetweenPoints.EndPoint, actualComportement.MoveBetweenPoints.StartPoint);
                break;
            case TypeKoukiBehaviour.Cheer:
                koukiScript.Cheer();
                break;
            case TypeKoukiBehaviour.EndAskQuestion:

                koukiScript.EndAsking();

                break;
            case TypeKoukiBehaviour.askQuestion:
                koukiScript.AskQuestion();
                break;
            case TypeKoukiBehaviour.laugh:
                koukiScript.Laugh();
                break;
            case TypeKoukiBehaviour.Wave:
                koukiScript.WaveAnimation();
                break;
            default:

                break;
        }
     
        if (actualComportement.DialogueList != null)
        {
            ActualIndexDialogue++;
            if ((actualComportement.DialogueList.Count - 1) > ActualIndexDialogue)
            {
               
                PlayDialogue();


            }
            else if (actualComportement.WaitBeforNextComportement.haveToWait)
            {
              
                PlayComportementNext();

            }
        }

    }


    
    public void ChoosColor(int ColorChosen)
    {
        isActions = true;
        print("color "
                  + ColorChosen);
        idAction = 1;
        idChoice = ColorChosen;
        ActualIndexDialogue = 0;
        PlayComportementAfterAction();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
