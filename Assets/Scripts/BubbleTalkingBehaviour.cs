using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ArabicSupport;
public enum  typeDialogBehaviour{

     StartSpeaking, 
    SpeakNextAnimation, 
    CloseAnimaton, 
    OpenQuestionWithButton, 
    nothing

}
public class BubbleTalkingBehaviour : MonoBehaviour {
    public GameObject ButtonsInDialog;
    public Text TextLabel;
    public AudioSource audiosource; 
    public void StartSpeaking(string dialogue, AudioClip clipToplay, bool isArab) {
        OpenDialog( dialogue,  clipToplay, isArab); 
    }
    public void UpdateText() {
        if (TextLabel.gameObject.activeInHierarchy)
        {

            TextLabel.gameObject.SetActive(false);
        }
        else
        {
            TextLabel.gameObject.SetActive(true);
        }

    }
     void OpenDialog(string dialogue, AudioClip clipToplay, bool isArab) {
        audiosource.clip = clipToplay;
        audiosource.Play();
        if (!isArab)
        {
            TextLabel.text = dialogue;
        }else
        {
           
            TextLabel.text = ArabicFixer.Fix(dialogue, false, false); 

        }
        gameObject.GetComponent<Animator>().SetTrigger("initDialogueTrigger");
        gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");
    }
     public void SpeakAnimation() {

    gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");
    }
    public void SpeakNextAnimation() {
        gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");
    }
    public void CloseAnimation() {
    gameObject.GetComponent<Animator>().SetTrigger("fadeDialogueTrigger");

    }
    public void OpenQuestionWithButtons(string dialogue, AudioClip clipToplay)
    {
        audiosource.clip = clipToplay;
        audiosource.Play();
        TextLabel.text = dialogue;
        ButtonsInDialog.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("initDialogueTrigger");
        gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");
    }
    public void AnswerQuestionWithButtons(bool answerIsYes) {
       
    ButtonsInDialog.SetActive(false); 

    }
}
