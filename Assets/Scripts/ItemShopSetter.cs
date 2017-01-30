using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShopSetter : MonoBehaviour {
    public ItemShop item;
    public Image itemSprite;
    public Text textPrice;
    public Button ButtonItem;
    public GameObject PriceContainer; 
    public Image itemName; 
    public Image DesactiveSprite;
    public Image SelectedItem;
    public void SetShopItem(ItemShop currentItem, bool canBuy, DecorManager decoScript)
    {
        decoManager = decoScript; 
        item = new ItemShop(currentItem);
        itemSprite.sprite = currentItem.ImageItem;
        textPrice.text = currentItem.priceItem.ToString();
        itemName.sprite = currentItem.NameImage;
        if (!item.HasItem)
        {
            DesactiveSprite.gameObject.SetActive((!canBuy));
        }
        else
        {

            DesactiveSprite.gameObject.SetActive(false);
        }
        SelectedItem.gameObject.SetActive((currentItem.isActif));
        PriceContainer.gameObject.SetActive((!currentItem.HasItem));
    }
    DecorManager decoManager;
    public void ActionButton()
    {
        if (item.HasItem)
        {
            if (item.isActif)
            {
                decoManager.DesactiveDeco(item.id);
            }
            else
            {
                decoManager.SetNewDeco(item.id);

            }

        }
        else
        {
            decoManager.AskForBuy(item.id);
        }

    }
}
