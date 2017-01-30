using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Newtonsoft.Json;
using UnityEngine.UI; 


public enum TypeDecor {
    bed, 
    closet, 
    desk, 
    drawer, 
    curtain, 
    defaut

}
public enum CatDeco
{
    ChangeSprite, 
    DesactiveAsset

}
[System.Serializable]
public class ItemDeco
{
    public int id;
    public CatDeco Category;
    public GameObject ObjectDeco;
    public bool isDefault;
    public bool isBaught;
    public bool isActif; 
    public int PriceStars;
    public TypeDecor TypeDeco;
    public Sprite spriteToChange;
    public Sprite spriteName;
    public AudioClip clip; 
    public void changeSprite() {
       ObjectDeco.GetComponent<Image>().sprite = spriteToChange; 

    }
}
public class UserDeco {

   public List<int> ListDecoInGame= new List<int>();
    public UserDeco()
    {
        ListDecoInGame = new List<int>();

    }
 public UserDeco(List<ItemDeco> ListDeco)
    {
       ListDecoInGame = new List<int>(); 
        for (int i = 0; i < ListDeco.Count; i++)
        {
            ListDecoInGame.Add(ListDeco[i].id); 

        }

    }
   
}
public class DecorManager : MonoBehaviour
{
    public AudioSource audio; 
    public List<ItemDeco> ListDecoInGame = new List<ItemDeco>();
    UserDeco userDecoItems;

    public void SetRoom()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/DecoUser.txt"); 
        if (System.IO.File.Exists(Application.persistentDataPath+"/DecoUser.txt"))
        {
            string DecoUser = System.IO.File.ReadAllText(Application.persistentDataPath + "/DecoUser.txt");
            userDecoItems = new UserDeco();
            userDecoItems = JsonUtility.FromJson<UserDeco>(DecoUser);

        }
        else
        {
            userDecoItems = new UserDeco(ListDecoInGame.FindAll(o => (o.isActif) || (o.isDefault)));
            System.IO.File.WriteAllText(Application.persistentDataPath + "/DecoUser.txt", JsonUtility.ToJson(userDecoItems));
        }
        for (int i = 0; i < ListDecoInGame.Count; i++)
        {
            if (userDecoItems.ListDecoInGame.Exists(o => o == ListDecoInGame[i].id))
            { if (ListDecoInGame[i].Category == CatDeco.DesactiveAsset)
                {
                    ListDecoInGame[i].ObjectDeco.SetActive(true);

                }
                else
                {
                    ListDecoInGame[i].ObjectDeco.SetActive(true);
                    ListDecoInGame[i].changeSprite();
                }

            }
            else
            {
                if (ListDecoInGame[i].Category == CatDeco.DesactiveAsset)
                {
                    ListDecoInGame[i].ObjectDeco.SetActive(false);
                }
            }

        }

    }

    public void SetNewDeco(int idDeco)
    {
        if (ListDecoInGame.Exists(o => o.id == idDeco))
        {
            if (ListDecoInGame.Find(o => o.id == idDeco).clip != null)
            {
                audio.PlayOneShot(ListDecoInGame.Find(o => o.id == idDeco).clip);
            }
            if (ListDecoInGame.Find(o => o.id == idDeco).Category == CatDeco.DesactiveAsset)
            {
                ListDecoInGame.Find(o => o.id == idDeco).isActif = true;
                userDecoItems.ListDecoInGame.Add(idDeco);
                ListDecoInGame.Find(o => o.id == idDeco).ObjectDeco.SetActive(true);

            }
            else
            {
                if (ListDecoInGame.FindAll(o => o.isActif).Exists(o => o.ObjectDeco == ListDecoInGame.Find(j => j.id == idDeco).ObjectDeco))
                {
                    int idToRemove = ListDecoInGame.FindAll(o => o.isActif).Find(o => o.ObjectDeco == ListDecoInGame.Find(j => j.id == idDeco).ObjectDeco).id;
                    userDecoItems.ListDecoInGame.Remove(idToRemove);
                    ListDecoInGame.Find(j => j.id == idToRemove).isActif = false;

                }
                ListDecoInGame.Find(o => o.id == idDeco).isActif = true;
                userDecoItems.ListDecoInGame.Add(idDeco);
                ListDecoInGame.Find(o => o.id == idDeco).changeSprite();
                ListDecoInGame.Find(o => o.id == idDeco).ObjectDeco.SetActive(true);
            }

        }
        else
        {

            Debug.LogError("SetNewDeco Deco not found :: " + idDeco);
        }
       
        System.IO.File.WriteAllText(Application.persistentDataPath + "/DecoUser.txt", JsonUtility.ToJson(userDecoItems));
        shopScript.SetShop(); 
    }
    public ShopManager shopScript; 
    public void DesactiveDeco(int idDeco) {
        if (ListDecoInGame.Exists(o => o.id == idDeco))
        {
            if (ListDecoInGame.Find(o => o.id == idDeco).Category == CatDeco.DesactiveAsset)
            {
                ListDecoInGame.Find(o => o.id == idDeco).isActif = false;
                userDecoItems.ListDecoInGame.Remove(idDeco);
                ListDecoInGame.Find(o => o.id == idDeco).ObjectDeco.SetActive(false);

            }
            else
            {
                if (ListDecoInGame.FindAll(o => o.isActif).Exists(o => o.ObjectDeco == ListDecoInGame.Find(j => j.id == idDeco).ObjectDeco))
                {
                    if (ListDecoInGame.Exists(o => o.isDefault && o.TypeDeco == ListDecoInGame.Find(j => j.id == idDeco).TypeDeco))
                    {
                        int idToReplace = ListDecoInGame.FindAll(o => o.isDefault).Find(o => o.TypeDeco == ListDecoInGame.Find(j => j.id == idDeco).TypeDeco).id;
                        userDecoItems.ListDecoInGame.Add(idToReplace);
                        ListDecoInGame.Find(j => j.id == idToReplace).isActif = true;
                        ListDecoInGame.Find(o => o.id == idToReplace).changeSprite();
                    }
                    else
                    {
                      

                    }
                }
                ListDecoInGame.Find(o => o.id == idDeco).isActif = false;
                userDecoItems.ListDecoInGame.Remove(idDeco);
             
            }

        }
        else
        {

            Debug.LogError("SetNewDeco Deco not found :: " + idDeco);
        }
        System.IO.File.WriteAllText(Application.persistentDataPath + "/DecoUser.txt", JsonUtility.ToJson(userDecoItems));
        shopScript.SetShop();

    }
 
    int idLastItemChanged;
    int idNewItem; 
    public void TryNewDeco(int idDeco) {
        if (ListDecoInGame.Exists(o => o.id == idDeco))
        {
            idNewItem = idDeco; 
            if (ListDecoInGame.Find(o => o.id == idDeco).Category == CatDeco.DesactiveAsset)
            {
               
                ListDecoInGame.Find(o => o.id == idDeco).ObjectDeco.SetActive(true);

            }
            else
            {
                if (ListDecoInGame.FindAll(o => o.isActif).Exists(o => o.ObjectDeco == ListDecoInGame.Find(j => j.id == idDeco).ObjectDeco))
                {
                     idLastItemChanged = ListDecoInGame.FindAll(o => o.isActif).Find(o => o.ObjectDeco == ListDecoInGame.Find(j => j.id == idDeco).ObjectDeco).id;
                 

                }
               
                ListDecoInGame.Find(o => o.id == idDeco).changeSprite();
                ListDecoInGame.Find(o => o.id == idDeco).ObjectDeco.SetActive(true);
            }

        }
        else
        {

            Debug.LogError("SetNewDeco Deco not found :: " + idDeco);
        }

    }
    public void PutBackItem() {
        if (ListDecoInGame.Exists(o => o.id == idNewItem)&&ListDecoInGame.Exists(o => o.id == idLastItemChanged))
        {
          
            if (ListDecoInGame.Find(o => o.id == idNewItem).Category == CatDeco.DesactiveAsset)
            {

                ListDecoInGame.Find(o => o.id == idNewItem).ObjectDeco.SetActive(false);

            }
            else
            {
               
                ListDecoInGame.Find(o => o.id == idLastItemChanged).changeSprite();
                ListDecoInGame.Find(o => o.id == idLastItemChanged).ObjectDeco.SetActive(true);
            }

        }
        else
        {

            Debug.LogError("SetNewDeco Deco not found :: " + idLastItemChanged +" ... " + idNewItem);
        }
  
    }
    public void AskForBuy(int idDeco) {
    //    Debug.Log("AskForBuy " +idDeco);
        BuyNewDeco(idDeco); 

    }
    public void BuyNewDeco(int idDeco)
    {
        if (ListDecoInGame.Exists(o => o.id == idDeco))
        {
            ListDecoInGame.Find(o => o.id == idDeco).isBaught = true;
        }
        else
        {

            Debug.LogError("SetNewDeco Deco not found :: " + idDeco);
        }
        SetNewDeco(idDeco);
     
    }
}
