using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Items info - index, name, rating, price, description
[System.Serializable]
public struct ItemInfos
{
    public int itemId;
    public string itemName;
    public float itemRating;
    public int itemPrice;
    public string itemDescription;
    public string itemGender;
    public string itemCategory;
    public int itemCategoryId;
}

public class ItemsPanel : MonoBehaviour
{
    //Individual item details - to generate and set properties
    [SerializeField]
    private ItemInfo item;

    //Hold all item details
    [SerializeField]
    private List<ItemInfo> allItems = new List<ItemInfo>();

    //Hold all item info - to view
    [SerializeField]
    private List<ItemInfos> itemInfos = new List<ItemInfos>();


    //Generate items 
    public void CreateItems(List<ItemInfos> infos)
    {
        //Reset values
        item.SetStatusTo(true);
        ClearAllSlides();

        Transform parent = item.GetParent();
        for (int gridIndex = 0; gridIndex < infos.Count; gridIndex++)
        {
            ItemInfos info = infos[gridIndex];
            ItemInfo newItem = null;
            newItem = Instantiate(item, parent) as ItemInfo;

            //Set properties to item
            if (newItem != null)
            {
                newItem.SetStatusTo(true);

                newItem.name = info.itemName;
                newItem.SetItemDetails(info);

                allItems.Add(newItem);
            }
        }

        //Save all item info
        itemInfos = infos;
        //Disable sample item
        item.SetStatusTo(false);
    }

    //Reset all details
    public void ClearAllSlides()
    {
        foreach (ItemInfo tile in allItems)
        {
            tile.SelfDestroy();
        }
        allItems.Clear();
    }

    //To show selected category items
    public void OnCategorySelection(int categoryId)
    {
        for (int itemInfoIndex = 0; itemInfoIndex < allItems.Count; itemInfoIndex++)
        {
            //Enable selected category and disable others
            if (allItems[itemInfoIndex].GetCategoryId() != categoryId)
            {
                allItems[itemInfoIndex].SetStatusTo(false);
            }
            else
            {
                allItems[itemInfoIndex].SetStatusTo(true);
            }
        }
    }

    public void OnClearingCategorySelection()
    {
        for (int itemInfoIndex = 0; itemInfoIndex < allItems.Count; itemInfoIndex++)
        {
            allItems[itemInfoIndex].SetStatusTo(true);
        }
    }
}
