using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
    public int idTrigger;

    void OnEnable() {

        Trigger(); 
    }
    
    public void Trigger() {
        switch (idTrigger)
        {
            case 1:
                HudController.instance.DisplayKitchen();
                break;
            case 2:
                HudController.instance.DisplayStore();
                break;
            case 3:
                HudController.instance.ExerciceMemory();
                break;
            case 4:
                HudController.instance.StartGameGrocery();
                break;
            default:
                break; 

        }
    }
}
