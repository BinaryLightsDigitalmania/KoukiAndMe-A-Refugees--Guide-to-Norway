using UnityEngine;
using System.Collections;

public class OptionButtonArrow : MonoBehaviour {

    void OnEnable() {
        GameManager.instance.conversationIsFrozen = true;
        HudController.instance.ActivateButtonOption(); 
    }

 
}
