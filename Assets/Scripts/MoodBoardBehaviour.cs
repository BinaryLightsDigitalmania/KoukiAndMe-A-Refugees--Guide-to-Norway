using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoodBoardBehaviour : MonoBehaviour {
    public bool isMouseDown = false;
    public bool highlightBounce = false;
    public bool isMouseUp = false;
    public GameObject highlightSprite;
    public float highlightSpeed = 0.7f;
    public Vector2 destScale;
    // Use this for initialization
    void Start() {
      //  GameManager.instance.conversationIsFrozen = true;

    }

    // Update is called once per frame
    void Update() {

    }
    public GameObject[] Buttons; 
    public void MoodButtonDown(GameObject highlightedButton)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Button>().enabled = false; 
        }
        highlightSprite.transform.position = highlightedButton.transform.position;
        gameObject.GetComponent<Animator>().SetTrigger("moodButtonPressed");
    }

    public void MoodButtonUp(GameObject hs)
    {
        highlightSprite = hs;
        isMouseDown = false;
        isMouseUp = true;
    }

    public void Inactivate() {
        
        gameObject.SetActive(false);
    }

    
    }
