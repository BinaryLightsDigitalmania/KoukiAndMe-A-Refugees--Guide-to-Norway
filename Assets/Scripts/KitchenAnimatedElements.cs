using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public enum typeItemOpened {
    door, 
    curtain, 
    fridge


}
[System.Serializable]
public class ItemOpened {
  public  int idItem;
  public  bool isOpened;
  public  typeItemOpened type;
 public   Animator anim;
 public   string animationBool;
    public void ItemClicked() {
        isOpened = !isOpened;
        anim.SetBool(animationBool, isOpened); 
    }
 
}
public class KitchenAnimatedElements : MonoBehaviour
{void Start()
    {

      //  HudController.instance.DisplayKitchen(); 
    }
    public List<ItemOpened> listItemsToAnimate = new List<ItemOpened>();
    public void DoorClicked(int idDoor)
    {
        if (listItemsToAnimate.Exists(o => o.type == typeItemOpened.door && o.idItem == idDoor))
        { listItemsToAnimate.Find(o => o.type == typeItemOpened.door && o.idItem == idDoor).ItemClicked(); }

    }
    public GameObject buttonFridge; 
    public void FridgeClicked()
    {
        if (listItemsToAnimate.Exists(o => o.type == typeItemOpened.fridge))
        { listItemsToAnimate.Find(o => o.type == typeItemOpened.fridge).ItemClicked();
            if (listItemsToAnimate.Find(o => o.type == typeItemOpened.fridge).isOpened) {
                buttonFridge.SetActive(false); 

            }

        }


    }
    public void ReactiveButtonFridge() {

        buttonFridge.SetActive(true);
    }

    public void CurtainClicked(int idCurtain)
    {
        if (listItemsToAnimate.Exists(o => o.type == typeItemOpened.curtain && o.idItem == idCurtain))
        { listItemsToAnimate.Find(o => o.type == typeItemOpened.curtain && o.idItem == idCurtain).ItemClicked(); }

    }


}
