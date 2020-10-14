using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    //Item properties
    [SerializeField]
    private int itemId = 0;
    [SerializeField]
    private int itemCategoryId = 0;
    [SerializeField]
    private string itemName = "item";
    [SerializeField]
    private string itemCategory = "category";
    [SerializeField]
    private float itemPrice = 0f;
    [SerializeField]
    private float itemRating = 0f;
    [SerializeField]
    private TextMeshProUGUI nameTxt;
    [SerializeField]
    private TextMeshProUGUI ratingTxt;
    [SerializeField]
    private TextMeshProUGUI priceTxt;
    [SerializeField]
    private string itemDescription = "description";
    [SerializeField]
    private string itemGender = "gender";
    [SerializeField]
    private RawImage itemImg;

    private ItemInfos itemInfo;

    //Item details panel access
    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;


    //Set dummy item gameobject status - active/inactive
    public void SetStatusTo(bool status)
    {
        gameObject.SetActive(status);
    }

    //Destroy item on reset
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    //Get parent of items for item generation
    public Transform GetParent()
    {
        return transform.parent;
    }

    public void SetItemDetails (ItemInfos info)
    {
        itemInfo = info;

        itemId = itemInfo.itemId;
        itemCategoryId = itemInfo.itemCategoryId;
        itemName = itemInfo.itemName;
        itemDescription = itemInfo.itemDescription;
        itemRating = itemInfo.itemRating;
        itemPrice = itemInfo.itemPrice;
        itemGender = itemInfo.itemGender;
        itemCategory = itemInfo.itemCategory;

        nameTxt.text = itemName;
        //Rating - Show one digit after decimal
        ratingTxt.text = "Rating - "+String.Format("{0:0.0}", itemRating);
        priceTxt.text = itemPrice.ToString() + " Rs"; 
    }

    //Get category id of item
    public int GetCategoryId()
    {
        return itemCategoryId;
    }

    public void ShowItemDetails ()
    {
        itemDetailsPanel.SetItemDetails(itemInfo);
        itemDetailsPanel.SetDetailsUIStatus(true);
    }
}
