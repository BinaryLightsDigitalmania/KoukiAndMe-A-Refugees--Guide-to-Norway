using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {
    public static HudController instance;
    public GameObject[] RoomAssets;
    public GameObject ButtonDecorate; 
    public GameObject[] KitchenAssets;
    public DecorManager decoManager;
    public GameObject dialogBox;
    public GameObject FullCompanion; 
    void Awake() {

        instance = this; 
    }
    #region Scenes
    public void DisplayRoom()
    {
        DisableKitchen();
        DisablePopupExplain();
   
        DisableRoad();
        DisableStore();
        DisableTalkingBubble(); 
        for (int i = 0; i < RoomAssets.Length; i++)
        {
            RoomAssets[i].SetActive(true); 

        }
        DisablePopupShop();
        ButtonDecorate.SetActive(false); 
        decoManager.SetRoom(); 


    }
    public void DisableRoom() {


         for (int i = 0; i < RoomAssets.Length; i++)
        {
            RoomAssets[i].SetActive(false); 

        }

    }
    public GameObject EnterStartKitchen;
    public GameObject EnterEndKitchen ;
    public GameObject PanelExerciceMemory;
    public void ExerciceMemory() {
        PanelExerciceMemory.SetActive(true); 
      PanelExerciceMemory.GetComponent<MemoryExercice>().StartGame(); 
    }
    public void DisplayKitchen() {
        DisableRoom();
        DisablePopupExplain();
        DisablePopupShop();
        DisableRoad();
        DisableStore();
        DisableTalkingBubble();

        FullCompanion.GetComponent<CompanionBehaviour>().ChangeEntry(EnterStartKitchen,EnterEndKitchen );
        for (int i = 0; i < KitchenAssets.Length; i++)
        {
            KitchenAssets[i].SetActive(true);

        }
        PanelExerciceMemory.SetActive(false);
    }
    public void DisableKitchen()
    {
        for (int i = 0; i < KitchenAssets.Length; i++)
        {
            KitchenAssets[i].SetActive(false);

        }
    }
    public GameObject[] GroceryPanel;
    public GameObject GroceryExercice;
    public GameObject GroceryEndingCompanion; 

    public void DisplayStore() {
        DisableExercice();
        DisableKitchen();
        DisablePopupShop();
        DisablePopupExplain();
        for (int i = 0; i < GroceryPanel.Length; i++)
        {

            GroceryPanel[i].SetActive(true); 
        }
        FullCompanion.GetComponent<CompanionBehaviour>().ChangeEntry(EnterStartKitchen, GroceryEndingCompanion);
    }
    public void DisableStore()
    {
        for (int i = 0; i < GroceryPanel.Length; i++)
        {

            GroceryPanel[i].SetActive(false);
        }
    }
    public void StartGameGrocery() {
        GroceryExercice.GetComponent<ExerciceGrocery>().StartGame(); 

    }
    public void DisplayRoad() {


    }
    public void DisableRoad()
    {


    }

    public void DisplayTurtle() {


    }
    public void DisableTurtle()
    {


    }
    #endregion

    #region UI 

    public void WinStars() {


    }
    public void WinsTrophy() {


    }

    #region UIRoom 
    public void DisplayUIRoom()
    {

    }
    public void DisableUIRoom() {

    }

    public void DisplayPopupExplain(int id) {


    }
    public void DisablePopupExplain() {

    }

    // if Diplay Shop explain show : bed wardrobe or desk / else shows everything
    public GameObject[] ShopPanel;
    public ShopManager ShopScript;
    bool ShopIsOpen = false; 
    public void DisplayPopupShop( )
    {
        if (!ShopIsOpen)
        {

            DisplayPopupShop(false); 
        }
        else {

            DisablePopupShop(); 
        }
       
     

    }
    public void ActivateButtonOption() {
        ButtonDecorate.SetActive(true);
    }

    public void DisplayPopupShop(bool explain, int id=0 )
    {
        ShopIsOpen = true; 
        for (int i = 0; i < ShopPanel.Length; i++)
        {
            ShopPanel[i].SetActive(true);

        }
        if (explain)
        {
            ShopScript.SetShopExplain(id); 

        }
        else
        {
            GameManager.instance.conversationIsFrozen = true;
            //dialogBox.GetComponent<Animator>().SetTrigger("directFadeTrigger");
            dialogBox.GetComponent<Animator>().SetTrigger("fadeDialogueTrigger");
            ShopScript.OpenShop();
        }

    }
    public void DisablePopupShop() {
        ShopIsOpen = false;
        for (int i = 0; i < ShopPanel.Length; i++)
        {
            ShopPanel[i].SetActive(false);

        }

        GameManager.instance.conversationIsFrozen = false;
        dialogBox.GetComponent<Animator>().SetTrigger("initDialogueTrigger"); 


    }

    public void DisplayExercice() {

    }
    public void DisableExercice() {

    }

    public void DisplayTalkingBuble(Sprite speach, bool isConfirmation=false) {


    }
    public void DisableTalkingBubble() {


    }


    #endregion

    #region UIKitchen
    public void DisplayUIKitchen() {

    }
    public void DisableUIKitchen() {


    }
    #endregion

    #region UIStore
    public void DisplayUIStore()
    {

    }
    public void DisableUIStore() {


    }
    #endregion

    #endregion

    #region exercice 
    #endregion
}
