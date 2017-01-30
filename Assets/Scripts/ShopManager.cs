using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemShop
{
    public int id;
    public Sprite ImageItem;
    public Sprite NameImage;
    public int priceItem;
    public bool HasItem;
    public bool isActif; 
    public ItemShop(ItemDeco temp)
    {
        if (temp.Category == CatDeco.ChangeSprite)
        {

            ImageItem = temp.spriteToChange;
        }
        else
        {
            ImageItem = temp.ObjectDeco.GetComponent<Image>().sprite;

        }
        id = temp.id;
        isActif = temp.isActif; 
        NameImage = temp.spriteName;
        priceItem = temp.PriceStars;
        HasItem = temp.isBaught;

    }
    public ItemShop(ItemShop temp)
    {


        ImageItem = temp.ImageItem;

        id = temp.id;
        isActif = temp.isActif; 
        NameImage = temp.NameImage;
        priceItem = temp.priceItem;
        HasItem = temp.HasItem;

    }
}
public class ShopManager : MonoBehaviour {

    public DecorManager decoScript; 
    public List<ItemShop>listItems = new List<ItemShop>();
    public GameObject PrefabItem;
    public GameObject container;
    public ScrollRect scrollView;  
     void SetItems() {
        List<ItemDeco> tempItems = decoScript.ListDecoInGame.FindAll(o => !o.isDefault);
        listItems = new List<ItemShop>(); 
            for (int i = 0; i < tempItems.Count; i++)
        {
            listItems.Add(new ItemShop( tempItems[i])); 

        }


    }
    public float DistanceBetweenItems;
     List<GameObject> GOInScroll=new List<GameObject>();
    public int StarsValue; 
    public void SetShop()
    {
        SetItems();
        //changer la taille du container 
      //  Debug.Log(DistanceBetweenItems * listItems.Count); 
        container.GetComponent<RectTransform>().sizeDelta.Set(DistanceBetweenItems * listItems.Count, container.GetComponent<RectTransform>().sizeDelta.y);
        // ajouter les elements 
        if (GOInScroll.Count > listItems.Count)
        {
            for (int i = 0; i < GOInScroll.Count; i++)
            {
                if (i < listItems.Count)
                {
                    GOInScroll[i].SetActive(true);
                    GOInScroll[i].GetComponent<ItemShopSetter>().SetShopItem(listItems[i], (listItems[i].priceItem <= StarsValue), decoScript);

                }
                else
                {
                    GOInScroll[i].SetActive(false);


                }
            }
        }
        else
        {
            for (int i = 0; i < listItems.Count; i++)
            {
                if (i < GOInScroll.Count)
                {
                    GOInScroll[i].SetActive(true);
                    GOInScroll[i].GetComponent<ItemShopSetter>().SetShopItem(listItems[i], (listItems[i].priceItem <= StarsValue), decoScript);

                }
                else
                {
                    GameObject go = Instantiate(PrefabItem) as GameObject;
                    go.transform.parent = container.transform;
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.GetComponent<ItemShopSetter>().SetShopItem(listItems[i], (listItems[i].priceItem <=StarsValue), decoScript);
                    GOInScroll.Add(go);
                }
            }

        }
     
      
    }
    public void OpenShop() {
        SetShop(); 
        //reset scroll 
  scrollView.horizontalScrollbar.value = 0; 
    }
    public void SetShopExplain(int id) {
        TypeDecor typeExplain = TypeDecor.bed; 
        switch (id)
        {
            case 1:
                typeExplain = TypeDecor.bed; 
                break;
            case 2:
                typeExplain = TypeDecor.closet; 
                break;
            case 3:
                typeExplain = TypeDecor.desk; 
                break;
        }
        List<ItemDeco> tempItems = decoScript.ListDecoInGame.FindAll(o => !o.isDefault && (o.TypeDeco == typeExplain) && (o.Category == CatDeco.ChangeSprite));
       List<ItemShop>   templistItems = new List<ItemShop>();
        for (int i = 0; i < tempItems.Count; i++)
        {
            templistItems.Add(new ItemShop(tempItems[i]));
            templistItems[i].HasItem = true; 
        }
        
        //changer la taille du container 
        container.GetComponent<RectTransform>().sizeDelta.Set(DistanceBetweenItems * templistItems.Count, container.GetComponent<RectTransform>().sizeDelta.y);
        // ajouter les elements 
        if (GOInScroll.Count > templistItems.Count)
        {
            for (int i = 0; i < GOInScroll.Count; i++)
            {
                if (i < templistItems.Count)
                {
                    GOInScroll[i].SetActive(true);
                    GOInScroll[i].GetComponent<ItemShopSetter>().SetShopItem(templistItems[i],true, decoScript);

                }
                else
                {
                    GOInScroll[i].SetActive(false);


                }
            }
        }
        else
        {
            for (int i = 0; i < templistItems.Count; i++)
            {
                if (i < GOInScroll.Count)
                {
                    GOInScroll[i].SetActive(true);
                    GOInScroll[i].GetComponent<ItemShopSetter>().SetShopItem(templistItems[i], true, decoScript);

                }
                else
                {
                    GameObject go = Instantiate(PrefabItem) as GameObject;
                    go.transform.parent = container.transform;
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.GetComponent<ItemShopSetter>().SetShopItem(templistItems[i], true, decoScript);
                    GOInScroll.Add(go);
                }
            }

        }
        //reset scroll 
        scrollView.horizontalScrollbar.value = 0;

    }
}
