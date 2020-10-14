using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    //Item details UI set
    [SerializeField]
    private GameObject itemDetailsUIGObj;

    //Item message UI set
    [SerializeField]
    private GameObject itemMessageUIGObj;

    //Item details to display
    [SerializeField]
    private RawImage itemImg;
    [SerializeField]
    private TextMeshProUGUI itemNameTxt;
    [SerializeField]
    private TextMeshProUGUI itemDescriptionTxt;
    [SerializeField]
    private TextMeshProUGUI itemRatingTxt;
    [SerializeField]
    private TextMeshProUGUI itemPriceTxt;
    [SerializeField]
    private TextMeshProUGUI itemGenderTxt;

    //To active/in-active details
    public void SetDetailsUIStatus (bool status)
    {
        itemDetailsUIGObj.SetActive(status);
    }

    //Set selected item details in UI
    public void SetItemDetails(ItemInfos info)
    {
        itemNameTxt.text = info.itemName;
        itemDescriptionTxt.text = info.itemDescription;
        itemRatingTxt.text = "Rating - " + String.Format("{0:0.0}", info.itemRating);
        itemPriceTxt.text = info.itemPrice.ToString() +" Rs";
        itemGenderTxt.text = info.itemGender;
    }

    //To active/in-active message
    public void SetMessageUIStatus(bool status)
    {
        itemMessageUIGObj.SetActive(status);
    }

    public void SetAllUIDetailsStatus (bool status)
    {
        itemDetailsUIGObj.SetActive(status);
        itemMessageUIGObj.SetActive(status);
    }
}
