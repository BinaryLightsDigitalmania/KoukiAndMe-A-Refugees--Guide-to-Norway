using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum DesignedBool {
correct, 
wrong, 
pending

}
[System.Serializable]
public class ItemMemory {

    public int idItem=-1;
    public int idAnswer;
    public bool isLetterItem;
    public Sprite NameItem;
    public Sprite ImageItem;
    public bool isClickedOn;
    public bool isTrue; 

}
public class MemoryExercice : MonoBehaviour {
    public List<ItemMemory> listItemExercise = new List<ItemMemory>();
    public List<MemoryExerciceItem> listItemsInPanel = new List<MemoryExerciceItem>();
    public GameObject PrefabMemoryExercice;
    public GameObject Container;
    public DialogueBoxBehaviour dialogueScript;
   // public Speach StartGameSprite;
    public Speach GoodJob;
    public Speach WrongAnswer;
    public Speach EndGameSprite;
    void Start() {
        //StartGame(); 
    }
    public GameObject Companion;
    public GameObject PositionEndCompanion;
    public GameObject PositionStartCompanion; 
    public void StartGame()
    {
        Companion.GetComponent<CompanionBehaviour>().ChangeEntry(PositionStartCompanion, PositionEndCompanion);
        dialogueScript.Unfreeze();
        //  Container.GetComponent<GridLayoutGroup>().enabled = true;
        //reset
        for (int i = 0; i < listItemExercise.Count; i++)
        {
            listItemExercise[i].idItem = -1;
        }
        listItemsInPanel.Clear();

        //randomize
        do
        {
            int randomIndex = 0;
            do
            {
                randomIndex = Random.Range(0, listItemExercise.Count);
            } while (listItemExercise[randomIndex].idItem != -1);
            listItemExercise[randomIndex].idItem = listItemsInPanel.Count;

            GameObject GO = Instantiate(PrefabMemoryExercice);
            GO.GetComponent<MemoryExerciceItem>().SetItem(listItemExercise[randomIndex], this, TimeToWaitBeforUnreveal);
            GO.transform.parent = Container.transform;
            GO.transform.localPosition = Vector3.zero;
            GO.transform.localScale = Vector3.one;
            listItemsInPanel.Add(GO.GetComponent<MemoryExerciceItem>());

        } while (listItemsInPanel.Count < listItemExercise.Count);
      //  Container.GetComponent<GridLayoutGroup>().enabled = false; 
    }
    public void ItemClicked() {


    }
    int nbrTryBeforSinceCorrect;
    public int nbrTryBeforHelp; 
    public DesignedBool CheckItems(int idItem)
    {

        if (listItemExercise.Exists(o => o.isClickedOn))
        {

            if (listItemExercise.Find(o => o.isClickedOn).idAnswer == listItemExercise.Find(o => o.idItem == idItem).idAnswer)
            {
                listItemExercise.Find(o => o.isClickedOn).isTrue = true;
                listItemExercise.Find(o => o.idItem == idItem).isTrue = true;
                nbrTryBeforSinceCorrect = 0;
                listItemsInPanel[listItemExercise.Find(o => o.isClickedOn).idItem].CorrectAnswer();
                listItemExercise.Find(o => o.isClickedOn).isClickedOn = false;
                if (!listItemExercise.Exists(o => !o.isTrue))
                { WinItems(); }
                else
                {
                    dialogueScript.Cheering(GoodJob);
                    GameManager.instance.conversationIsFrozen = true;
                }
                //  StartCoroutine(waitBeforErase(  listItemExercise.Find(o => o.idItem == idItem).idAnswer));
                return DesignedBool.correct;


            }
            else
            {
                DesactiveAllButtons();
                listItemsInPanel[listItemExercise.Find(o => o.isClickedOn).idItem].WrongAnswer();
                listItemExercise.Find(o => o.isClickedOn).isClickedOn = false;
                StartCoroutine(waitBeforReactive());
                nbrTryBeforSinceCorrect++;
                dialogueScript.Cheering(WrongAnswer);
                GameManager.instance.conversationIsFrozen = true;
                return DesignedBool.wrong;
            }


        }
        else
        {
          
          
            listItemExercise.Find(o => o.idItem == idItem).isClickedOn = true;
            if (nbrTryBeforHelp <= nbrTryBeforSinceCorrect)
            {
                int idItemToHelp = listItemExercise.Find(j => j.idAnswer == listItemExercise.Find(o => o.idItem == idItem).idAnswer && !j.isClickedOn).idItem;
                HelpItems(idItemToHelp); 
            }
            return DesignedBool.pending;
        }
    }
    public float TimeToWaitBeforUnreveal = 2;
    IEnumerator waitBeforReactive() {
        yield return new WaitForSeconds(TimeToWaitBeforUnreveal);
        ReactiveAllButton();
    }
    IEnumerator waitBeforErase(int idAnswer)
    {
        yield return new WaitForSeconds(TimeToWaitBeforUnreveal);
        ReactiveAllButton();
       List<ItemMemory> templist = new List<ItemMemory>();
        templist = listItemExercise.FindAll(o => o.idAnswer == idAnswer);

        for (int i = 0; i < templist.Count; i++)
        {
            listItemsInPanel[templist[i].idItem].gameObject.SetActive(false);
        }
    }
    public void DesactiveAllButtons() {

        for (int i = 0; i < listItemsInPanel.Count; i++)
        {
            listItemsInPanel[i].DesactiveCard();
        }
    }
    public void ReactiveAllButton() {
        List<ItemMemory> templist = new List<ItemMemory>();
        templist =listItemExercise.FindAll(o => !o.isTrue &&!o.isClickedOn  ); 

        for (int i = 0; i < templist.Count; i++)
        {
            //Debug.Log(""+templist[i].idItem);
            listItemsInPanel[templist[i].idItem].ReactiveCard(); 
        }

    }
    public void WinItems() {
        dialogueScript.Cheering(EndGameSprite);
        dialogueScript.Unfreeze(); 

    }
    public void LooseItems() {


    }
    public void HelpItems(int idItem) {
        listItemsInPanel[idItem].Help(); 

    }
}
