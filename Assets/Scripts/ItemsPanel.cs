using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//Items info - index, name, rating, price, description etc.
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
    //Hold selected category item info only
    [SerializeField]
    private List<ItemInfo> selectedCategoryItemInfos = new List<ItemInfo>();

    //Hold all item info - to view
    [SerializeField]
    private List<ItemInfos> itemInfos = new List<ItemInfos>();

    //Item details panel access
    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    //Filter details panel access
    [SerializeField]
    private FilterPanel filterPanel;

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
        //Reset details
        int itemsDisplayed = 0;
        selectedCategoryItemInfos.Clear();

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
                //Add selected category items in list - useful while doing filter
                selectedCategoryItemInfos.Add(allItems[itemInfoIndex]);
                //Number of items avilable from selected category
                itemsDisplayed += 1;
            }
        }

        //Disable sub fileter details
        filterPanel.SetFilterUIStatus(false);

        //Show no items found message
        if (itemsDisplayed <= 0)
        {
            itemDetailsPanel.SetMessageUIStatus(true);
        }
        else
        {
            //Show sub fileter details
            filterPanel.SetFilterUIStatus(true);
            Toggle genderToggle = filterPanel.GetSelectedGenderToggleDetails();
            if(genderToggle != null)
            {
                //Filter with already applied filter details for New category selected
                FilterItemsBasedOnSelections(genderToggle);
            }
        }
    }

    public void OnClearingCategorySelection()
    {
        selectedCategoryItemInfos.Clear();
        for (int itemInfoIndex = 0; itemInfoIndex < allItems.Count; itemInfoIndex++)
        {
            allItems[itemInfoIndex].SetStatusTo(true);
        }
    }

    public void FilterItemsBasedOnSelections (Toggle genderToggle)
    {
        if (selectedCategoryItemInfos.Count > 0 && genderToggle.isOn == true)
        {
            for (int itemInfoIndex = 0; itemInfoIndex < selectedCategoryItemInfos.Count; itemInfoIndex++)
            {
                //Enable selected category and disable others
                if (selectedCategoryItemInfos[itemInfoIndex].GetItemGender().ToLower() != genderToggle.name.ToLower())
                {
                    selectedCategoryItemInfos[itemInfoIndex].SetStatusTo(false);
                }
                else
                {
                    selectedCategoryItemInfos[itemInfoIndex].SetStatusTo(true);
                }
            }
        }
    }

    public void ResetFilterDetails ()
    {
        if (selectedCategoryItemInfos.Count > 0)
        {
            for (int itemInfoIndex = 0; itemInfoIndex < selectedCategoryItemInfos.Count; itemInfoIndex++)
            {
                //list all items of selected category
                selectedCategoryItemInfos[itemInfoIndex].SetStatusTo(true);
            }
        }
    }
}
