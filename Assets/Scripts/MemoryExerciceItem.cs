using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemoryExerciceItem : MonoBehaviour {
    public Image ImageItem; 
    public Image NameItem;
    public GameObject PanelCard;
    public GameObject PanelBack;
    public GameObject ButtonCard;
    public Sprite NeutralBG;
    public Sprite WrongBG;
    public Sprite CorrectBG;
    public Image BG;
    public Image Clicked; 
    MemoryExercice exerciceScript;
     int id;
    public void SetItem(ItemMemory item,MemoryExercice _exerciceScript , float waitBeforDesactive)
    {
        UnrevealCard();
        exerciceScript = _exerciceScript;
        id = item.idItem; 
        ImageItem.sprite = item.ImageItem;
        NameItem.sprite = item.NameItem;
        TimeToWaitBeforUnreveal = waitBeforDesactive; 
         

}
    public void ClickOnItem()
    {
        anim.SetBool("Help", false);
        DesignedBool itemClickedOn = exerciceScript.CheckItems(id); 
        if (itemClickedOn== DesignedBool.correct)
        {
            CorrectAnswer();

        }
        else if (itemClickedOn == DesignedBool.wrong)
        {
            WrongAnswer();

        }
        else {
            RevealCard(); 

        }
    }
    public void CorrectAnswer() {
        RevealCard();
        BG.sprite  = CorrectBG;
       Clicked.gameObject.SetActive(false);
        StartCoroutine(waitBeforDesactive()); 
    }
    public void WrongAnswer()
    {
      
        RevealCard();
        BG.sprite = WrongBG;
        StartCoroutine(waitBeforUnreveal()); 
    }
     float TimeToWaitBeforUnreveal;
    IEnumerator waitBeforUnreveal() {
        yield return new WaitForSeconds(TimeToWaitBeforUnreveal);
        UnrevealCard(); 
    }
    IEnumerator waitBeforDesactive()
    {
        yield return new WaitForSeconds((TimeToWaitBeforUnreveal*2));
        BG.gameObject.SetActive(false); 
        Clicked.gameObject.SetActive(false);
        PanelCard.SetActive(false);
        PanelBack.SetActive(false);
        ButtonCard.SetActive(false);
    }
    public void DesactiveCard()
    {
        anim.SetBool("Help", false);
        ButtonCard.SetActive(false);
    }
   
    public void ReactiveCard()
    {
        ButtonCard.SetActive(true);
    }
    public void UnrevealCard()
    {
        BG.sprite = NeutralBG; 
        Clicked.gameObject.SetActive(false);
        PanelCard.SetActive(false);
        PanelBack.SetActive(true);
       
        ButtonCard.SetActive(true);
    }
    public void RevealCard()
    {
        BG.sprite = NeutralBG; 
        Clicked.gameObject.SetActive(true);
        PanelCard.SetActive(true);
        PanelBack.SetActive(false);
        ButtonCard.SetActive(false);
    }
    public Animator anim; 
    public void Help() {
        anim.SetBool("Help", true); 

    }



}
