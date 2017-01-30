using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScenarioSteps : MonoBehaviour
{

    // Use this for initialization

    public List<comportement> RoomMoodScript; // Comportement of Kouki in the room 
    public List<comportement> MoodComportement;  // Comportement of Kouki after player answers "Mood" question
    public List<comportement> MoodColorPicker; // Comportement of Kouki after player picks a color
    public List<comportement> CloseQuestion; // Comportement of Kouki At the end of the mood picker
    public List<comportement> MeteoPicker; //comportement of kouki after pickuing color
    public List<comportement> ComportementCloset; //comportement of kouki after choosing what to wear for the closet
    public List<comportement> ColorCurtainPicker; // Comportement of Kouki At the end of the color picker
    public comportement LookCurtain; //comportement after changing curtain color

    public List<comportement> WelcomeScript; //Welcoming steps
    public List<comportement> EndComportementScript; //ending steps
   
    List<comportement> actualComportementList; //actual steps the character has to make
    List<Dialogue> ActualDialogueList = new List<Dialogue>(); 
    int ActualIndexComportement;
    int ActualDialogueIndex;
    int ActualIndexScenario;

    public KoukiBehaviour koukiScript;
    public BubbleTalkingBehaviour BubbleScript;

    public GameObject WelcomeScene; //The gameobject containing welcome screen
    public GameObject RoomScene;//The gameobject containing Room Screen
    public Color[] ColorCurtains;
    public GameObject Curtain;
    public GameObject Button;
    public GameObject CurtainPointer;
    public GameObject[] MeteoGuy;
    public GameObject ClosetClosed;
    public GameObject ClosetOpen;

    public Button ButtonReplay;
    public GameObject MoodBehaviour;
    public GameObject ColorBehaviour;
    public Button buttonCurtain;
   
    int actualIndexLanguage = 1;


    void Start()
    {

        Welcome();
    }

    public void Welcome()
    {
        ActualIndexScenario = 0;
        WelcomeScene.SetActive(true);
        RoomScene.SetActive(false);
        actualComportementList = WelcomeScript;
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        PlayComportement();
    }


    public void PasserDansLaChambreMoodQuestion()
    {

        ActualIndexScenario = 1;
        WelcomeScene.SetActive(false);
        RoomScene.SetActive(true);
        actualComportementList = RoomMoodScript;
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        PlayComportement();
    }

    public void EndComportement()
    {
        Button.SetActive(false);
        switch (ActualIndexScenario)
        {
            case 0:
                //etait en Welcome Passe dans la chambre 
                PasserDansLaChambreMoodQuestion();
                break;
            case 2:
                PointCurtain();
                break;
            case 3:
                ShowColorPicker();
                break;
            case 4:
                ShowMeteo();
                break;
            case 6:
                GoOutside();
                break;
            default:
                break;
        }
    }

    public void PlayComportement()
    {
        comportement actualComportement = new comportement();
        if (ActualIndexComportement < actualComportementList.Count)
        {

            actualComportement = actualComportementList[ActualIndexComportement];
            ActualDialogueList = actualComportement.DialogueList;
            ActualDialogueIndex = 0;
            switch (actualComportement.TypeDialogue)
            {
                case typeDialogBehaviour.StartSpeaking:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualDialogueIndex].dialogueTextNor,
                        actualComportement.DialogueList[ActualDialogueIndex].dialogueClipNor, false);
                    koukiScript.Speak();
                    break;
                case typeDialogBehaviour.SpeakNextAnimation:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualDialogueIndex].dialogueTextNor,
                       actualComportement.DialogueList[ActualDialogueIndex].dialogueClipNor, false);
                    koukiScript.Speak();
                    break;
                case typeDialogBehaviour.OpenQuestionWithButton:
                    BubbleScript.StartSpeaking(actualComportement.DialogueList[ActualDialogueIndex].dialogueTextNor,
                      actualComportement.DialogueList[ActualDialogueIndex].dialogueClipNor, false);
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
                case TypeKoukiBehaviour.changeclothes:
                    koukiScript.ChangeClothes();
                    // OpenCloset(); 
                    break;
                default:

                    break;
            }

            if (actualComportement.DialogueList != null)
            {
                if ((actualComportement.DialogueList.Count - 1) > ActualDialogueIndex)
                {
                    if (actualComportement.DialogueList[ActualDialogueIndex].WaitBeforNextDialogue.haveToWait)
                    {
                        StartCoroutine(WaitBeforDisplayNextDialogue(actualComportement.DialogueList[ActualDialogueIndex].WaitBeforNextDialogue.SecondsToWait));

                    }
                    else
                    {
                        ActualDialogueIndex++;
                        PlayDialogue();
                    }

                }
                else if (actualComportement.WaitBeforNextComportement.haveToWait)
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
                    OpenCloset();
                    break;
                default:
                    break;
            }
        }

    }
    public void ActivateNext()
    {

        Button.SetActive(true);
    }
    public void ChoosColor(int ColorChosen)
    {
        actualComportementList.Clear();
        CurtainPointer.SetActive(false);
        actualComportementList.Add(MoodColorPicker.Find(o => o.Index == ColorChosen));
        actualComportementList[0].WaitBeforNextComportement.haveToWait = true;
        actualComportementList[0].WaitBeforNextComportement.SecondsToWait = 3;
        actualComportementList.Add(LookCurtain);
        buttonCurtain.interactable = false;
        Curtain.GetComponent<Image>().color = ColorCurtains[ColorChosen];
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        ActualIndexScenario = 4;
        PlayComportement();
    }
    public void ChooseMood(int MoodChosen)
    {
        actualComportementList.Clear();
        actualComportementList.Add(MoodComportement.Find(o => o.Index == MoodChosen));
        actualComportementList[0].WaitBeforNextComportement.haveToWait = true;
        actualComportementList[0].WaitBeforNextComportement.SecondsToWait = 3;
        actualComportementList.Add(CloseQuestion[0]);
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        ActualIndexScenario = 2;
        PlayComportement();



    }
    public void PointCurtain()
    {


        CurtainPointer.SetActive(true);
        buttonCurtain.interactable = true;
        ActualIndexScenario = 3;
        actualComportementList = ColorCurtainPicker;
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        PlayComportement();

    }
    public void ShowMeteo()
    {

        ActualIndexScenario = 5;
        for (int i = 0; i < MeteoGuy.Length; i++)
        {
            MeteoGuy[i].SetActive(true);
        }
        ClosetClosed.GetComponent<Button>().interactable = true;
        actualComportementList = MeteoPicker;
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        PlayComportement();

    }

    public void WearSomething()
    {


    }
    public void OpenCloset()
    {
        if (ActualIndexScenario != 6)
        {
            ActualIndexScenario = 6;

            for (int i = 0; i < MeteoGuy.Length; i++)
            {
                MeteoGuy[i].SetActive(false);
            }
            ClosetClosed.GetComponent<Button>().interactable = false;
            ClosetClosed.SetActive(false);
            ClosetOpen.SetActive(true);
            actualComportementList = ComportementCloset;
            ActualIndexComportement = 0;
            ActualDialogueIndex = 0;
            PlayComportement();
        }


    }
    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void GoOutside()
    {
        ActualIndexScenario = 7;
        actualComportementList = EndComportementScript;
        ActualIndexComportement = 0;
        ActualDialogueIndex = 0;
        WelcomeScene.SetActive(true);
        RoomScene.SetActive(false);
        ButtonReplay.gameObject.SetActive(true);
        PlayComportement();
    }
    public void ShowMoodPicker()
    {
        MoodBehaviour.SetActive(true);
    }
    public void ShowColorPicker()
    {
        buttonCurtain.interactable = false;
        ActualIndexScenario = 10;
        ColorBehaviour.SetActive(true);
    }


    public void PlayComportementNext()
    {
        if (ActualIndexComportement < (actualComportementList.Count - 1))
        {

            if (actualComportementList[ActualIndexComportement].WaitBeforNextComportement.haveToWait)
            {
                StartCoroutine(WaitBeforNextComportement(actualComportementList[ActualIndexComportement].WaitBeforNextComportement.SecondsToWait));
            }
            else
            {
                ActualIndexComportement++;
                PlayComportement();
                if (actualComportementList.Count == ActualIndexComportement)
                {

                    ActivateNext();
                }

            }


        }
        else
        {
            ActivateNext();

        }

    }
    IEnumerator WaitBeforDisplayNextDialogue(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        ActualDialogueIndex++;
        PlayDialogue();

    }
    IEnumerator WaitBeforNextComportement(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        ActualIndexComportement++;
        PlayComportement();
        if (actualComportementList.Count == ActualIndexComportement)
        {

            ActivateNext();
        }

    }

    public void PlayDialogue()
    {



        if (actualIndexLanguage == 1)
        {
            if (ActualDialogueIndex < ActualDialogueList.Count)
                BubbleScript.StartSpeaking(ActualDialogueList[ActualDialogueIndex].dialogueTextNor,
                          ActualDialogueList[ActualDialogueIndex].dialogueClipNor, false);
        }
        else
        {
            if (ActualDialogueIndex < ActualDialogueList.Count)
                BubbleScript.StartSpeaking(ActualDialogueList[ActualDialogueIndex].dialogueTextAr,
                                 ActualDialogueList[ActualDialogueIndex].dialogueClipAr, true);

        }
        koukiScript.Speak();

        if ((ActualDialogueIndex < (ActualDialogueList.Count - 1)) && (ActualDialogueList[ActualDialogueIndex].WaitBeforNextDialogue.haveToWait))
        {
            StartCoroutine(WaitBeforDisplayNextDialogue(ActualDialogueList[ActualDialogueIndex].WaitBeforNextDialogue.SecondsToWait));



        }
        else if (ActualDialogueIndex >= (ActualDialogueList.Count - 1))
        {

            PlayComportementNext();

        }
    }

    public void ChangeLanguage()
    {
        if (actualIndexLanguage == 1)
        {
            actualIndexLanguage = 0;
        }
        else
        {
            actualIndexLanguage = 1;
        }
        ActualDialogueIndex = 0;

        PlayDialogue();

    }

}
