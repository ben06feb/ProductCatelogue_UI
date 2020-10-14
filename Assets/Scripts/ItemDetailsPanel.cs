using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject itemDetailsUIGObj;

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


    public void SetDetailsUIStatus (bool status)
    {
        itemDetailsUIGObj.SetActive(status);
    }

    public void SetItemDetails(ItemInfos info)
    {
        itemNameTxt.text = info.itemName;
        itemDescriptionTxt.text = info.itemDescription;
        itemRatingTxt.text = "Rating - " + String.Format("{0:0.0}", info.itemRating);
        itemPriceTxt.text = info.itemPrice.ToString() +" Rs";
        itemGenderTxt.text = info.itemGender;
    }

}
