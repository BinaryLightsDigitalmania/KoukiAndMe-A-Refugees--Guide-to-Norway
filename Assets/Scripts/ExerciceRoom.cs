using UnityEngine;
using System.Collections;

public class ExerciceRoom : MonoBehaviour {
     Explanation ExplanationRoom;
 
    public void StartExplanation() {
        if (TaskManager.Instance.explanation.Exists(o => o.Scene == TypeScene.RoomScene))
        {
            ExplanationRoom = TaskManager.Instance.explanation.Find(o => o.Scene == TypeScene.RoomScene);
            if (ExplanationRoom.speachBeforExercice.Count > 0)
            {
                TellSpeach(ExplanationRoom.speachBeforExercice[0].id);

                if (!ExplanationRoom.speachBeforExercice[0].NextIsTriggered)
                {
                    CurrentIndexSpeach = 0; 
                    StartCoroutine(WaitBeforNextSpeach(ExplanationRoom.speachBeforExercice[0].WaitBeforNext)); 

                }

            }
            else {
                StartExerciceExplanation();

            }
        }
        else {
            Debug.LogError("Room explanation Not Found");
                }
    }
    int CurrentIndexSpeach; 
     IEnumerator WaitBeforNextSpeach(float secondsToWait ) {
        yield return new WaitForSeconds(secondsToWait);
        NextSpeach(); 
    }
    public void TellSpeach(int idSpeach) {
        Debug.LogError("Implement TellSpeach"); 

    }
    public void NextSpeach() {
        if (CurrentIndexSpeach < (ExplanationRoom.speachBeforExercice.Count - 1))
        {
            CurrentIndexSpeach++;

            TellSpeach(ExplanationRoom.speachBeforExercice[CurrentIndexSpeach].id);

            if (!ExplanationRoom.speachBeforExercice[CurrentIndexSpeach].NextIsTriggered)
            {
                
                StartCoroutine(WaitBeforNextSpeach(ExplanationRoom.speachBeforExercice[CurrentIndexSpeach].WaitBeforNext));

            }

        }
        else
        {
            StartExerciceExplanation(); 
        }

        

    }
    public void StartExerciceExplanation() {


    }
}
