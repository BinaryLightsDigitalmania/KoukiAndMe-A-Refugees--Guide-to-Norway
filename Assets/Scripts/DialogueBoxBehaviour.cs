using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
public enum SpeachType {
    mood, 
    explanation,
    welcome

}
public class DialogueBoxBehaviour : MonoBehaviour {
    public List<Speach> dialogBoxText;

    public static int textIndex = 0;
    public static int welcomeIndex = 0;
    public static int moodIndex = 0;
    public static int explanationsIndex = 0;
    public static int currentExplanationIndex = 0;
    public Speach currentSpeach;
    public SpeachType speachType ;
    public GameObject textImage;
    public GameObject moodBoard;
    public GameObject companion;
    public float timeToNextReplica;
    // Use this for initialization
  
    void Start() {
        dialogBoxText = GameManager.instance.welcomeSpeech;

       
}

    // Update is called once per frame
    void Update() {
        //GameManager.Instance.moodSpeech
    }


    public void UpdateText()
    {
        GameManager.instance.ActualScenario = textIndex;
        if (!GameManager.instance.conversationIsFrozen)
        {
            if (GameManager.instance.scenario.Exists(o => o.scenarioPoint == textIndex))
            {
                PlotTwist pt = GameManager.instance.scenario.Find(o => o.scenarioPoint == textIndex);


                switch (pt.speachTypeId)
                {
                    case 0:
                        speachType = SpeachType.welcome;
                        break;
                    case 1:
                        speachType = SpeachType.mood;
                        break;
                    case 2:
                        speachType = SpeachType.explanation;
                        break;

                }
            }

            if ((speachType == SpeachType.welcome) && welcomeIndex < GameManager.instance.welcomeSpeech.Count)
            {
                currentSpeach = GameManager.instance.welcomeSpeech[welcomeIndex];
                if (welcomeIndex < GameManager.instance.welcomeSpeech.Count - 1)
                {
                    welcomeIndex++;
                }

            }

            else if ((speachType == SpeachType.mood) && moodIndex < GameManager.instance.moodSpeech.Count)
            {
                currentSpeach = GameManager.instance.moodSpeech[moodIndex];
                currentSpeach.responseObjectName.SetActive(true);

                if (moodIndex < GameManager.instance.moodSpeech.Count - 1)
                {
                    moodIndex++;
                }

            }
            else if (currentExplanationIndex < GameManager.instance.explanations[0].explanation.Count)
            {
                currentSpeach = GameManager.instance.explanations[0].explanation[currentExplanationIndex];
                if (currentExplanationIndex < GameManager.instance.explanations[explanationsIndex].explanation.Count - 1)
                {
                    currentExplanationIndex++;
                }

                currentSpeach.responseObjectName.SetActive(true);







            }


            if (currentSpeach != null)
            {
                textImage.GetComponent<Image>().sprite = currentSpeach.spriteSpeach;

                textIndex++;
                if (!currentSpeach.NextIsTriggered)

                {

                    if (currentSpeach.audio != null)
                    {
                        Invoke("ToNextReplica", currentSpeach.audio.length);
                        audioSource.clip = currentSpeach.audio;
                        audioSource.Play();
                    }
                    else
                    {
                        Invoke("ToNextReplica", timeToNextReplica);

                    }

                }
                else
                {
                    audioSource.clip = currentSpeach.audio;
                    audioSource.Play();
                    GameManager.instance.conversationIsFrozen = true;

                }
            }

        }

    }




    public void UpdateTextOld() {
      /*  GameManager.instance.ActualScenario = textIndex; 
        //Debug.Log (GameManager.instance.conversationIsFrozen);
        if (!GameManager.instance.conversationIsFrozen) {
            if (speachType == null && welcomeIndex < GameManager.instance.welcomeSpeech.Count)
            {
                currentSpeach = GameManager.instance.welcomeSpeech[welcomeIndex];
                
         

                if (welcomeIndex < GameManager.instance.welcomeSpeech.Count - 1)
                {
                    welcomeIndex++;
                }

            }
            else if (speachType == false && moodIndex < GameManager.instance.moodSpeech.Count)
            {
                currentSpeach = GameManager.instance.moodSpeech[moodIndex];
                //  Debug.Log(GameManager.instance.moodSpeech[moodIndex].responseObjectName); 
                currentSpeach.responseObjectName.SetActive(true);
                //print ("mood" + moodIndex + " " + GameManager.instance.moodSpeech[moodIndex].id);
                textIndex++;

                if (moodIndex < GameManager.instance.moodSpeech.Count - 1)
                {
                    moodIndex++;
                }

            }
            else if( explanationsIndex < GameManager.instance.explanations.Count)
            {

               currentSpeach = GameManager.instance.explanations[explanationsIndex].explanation[currentExplanationIndex];
                if (currentExplanationIndex < GameManager.instance.explanations[explanationsIndex].explanation.Count - 1)
                {
                    //Debug.Log (" Explanations index " + explanationsIndex + " Explana index " + currentExplanationIndex);
                    currentExplanationIndex++;
                }

                //   Debug.Log(explanationsIndex + " " + currentExplanationIndex);
currentSpeach.responseObjectName.SetActive(true);




                textIndex++;

                if (currentExplanationIndex == GameManager.instance.explanations[explanationsIndex].explanation.Count - 1)
                {
                    //Debug.Log ("to next explanation " +  (GameManager.instance.explanations[explanationsIndex].explanation.Count-1));
                    if (explanationsIndex < GameManager.instance.explanations.Count - 1)
                    {
                        explanationsIndex++;
                    }

                    currentExplanationIndex = 0;


                }
            
                //Debug.Log ("Explanations index " + explanationsIndex + " Explana index " + currentExplanationIndex);
            }

            foreach (GameManager.PlotTwist pt in GameManager.instance.scenario) {
                if (pt.scenarioPoint == textIndex) {
                    switch (pt.speachTypeId) {
                        case 0:
                            speachType = null;
                            break;
                        case 1:
                            speachType = false;
                            break;
                        case 2:
                            speachType = true;
                            break;

                    }
                }

                textImage.GetComponent<Image>().sprite = currentSpeach.spriteSpeach;
                if (GameManager.instance.conversationIsFrozen == false)
                {
                    textIndex++;
                    if (!currentSpeach.NextIsTriggered)

                    {
                       
                            if (currentSpeach.audio != null)
                            {
                                Invoke("ToNextReplica", currentSpeach.audio.length);
                            audioSource.clip = currentSpeach.audio;
                            audioSource.Play(); 
                            }
                            else
                            {
                                Invoke("ToNextReplica", timeToNextReplica);

                            }
                    
                    }
                    else
                    {
                        GameManager.instance.conversationIsFrozen = true;

                    }
                }
              
            }

        }
        */
    }
    public AudioSource audioSource; 
    public void ToNextReplica()
    {
       
        companion.GetComponent<Animator>().SetTrigger("talkTrigger");
        gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");
    }
    public void FromCheeringToNext() {
  GameManager.instance.conversationIsFrozen = false;
        companion.GetComponent<Animator>().SetTrigger("talkTrigger");
        gameObject.GetComponent<Animator>().SetTrigger("nextReplicaTrigger");

    }

    public List<Speach> DesignedSpeach = new List<Speach>(); 
    public void MoodReply(int indexSprite)
    {
        //ToNextReplica();
        Cheering(GameManager.instance.DesignedSpeach[indexSprite]);
        Invoke("SlideDown", GameManager.instance.DesignedSpeach[indexSprite].audio.length); 
    }
    public void SlideDown() {
        companion.GetComponent<Animator>().SetBool("SlideDown", true); 

    }
    public void Cheering(Speach s)
    {
        GameManager.instance.conversationIsFrozen = true;
        textImage.GetComponent<Image>().sprite = s.spriteSpeach;
        if (!s.NextIsTriggered)

        {

            if (s.audio != null)
            {
                Invoke("FromCheeringToNext", s.audio.length);
                audioSource.clip = s.audio;
                audioSource.Play();
            }
            else
            {
                Invoke("FromCheeringToNext", timeToNextReplica);

            }
          
        }
        else
        {
            if (s.audio != null)
            {
                
                audioSource.PlayOneShot(s.audio);
            }
            GameManager.instance.conversationIsFrozen = true;

        }
    }
   /* public void BedMoodReply()
    {
        ToNextReplica();

        textImage.GetComponent<Image>().sprite = s;
        Invoke("Unfreeze", timeToNextReplica);

    }*/

    public void Unfreeze() {
        GameManager.instance.conversationIsFrozen = false;
        Invoke("ToNextReplica", timeToNextReplica);
    }

}
