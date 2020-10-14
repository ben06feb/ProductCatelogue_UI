using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryInfo : MonoBehaviour
{
    //Category properties
    [SerializeField]
    private int categoryId = 0;
    [SerializeField]
    private string categoryName = "category";
    [SerializeField]
    private TextMeshProUGUI categoryHeaderTxt;
    [SerializeField]
    private Image categoryBGImg;
    [SerializeField]
    private Button categoryBtn;
    

    //Main category grid panel access
    [SerializeField]
    private CategoryPanel categoryPanel;

    public void SetCategorySeletionStatus(bool btnStatus, Color color)
    {
        categoryBtn.interactable = btnStatus;
        categoryBGImg.color = color;
    }

    //Set dummy category gameobject status - active/inactive
    public void SetStatusTo(bool status)
    {
        gameObject.SetActive(status);
    }

    public void SetBasicInfo (int id, string name)
    {
        categoryId = id;
        categoryName = name;
        categoryHeaderTxt.text = categoryName;
    }

    //Destroy category on reset
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    //Get parent of category for category generation
    public Transform GetParent()
    {
        return transform.parent;
    }

    //Get button of category on generation, add with category panel list
    public void GetCategoryBtn()
    {
        categoryPanel.AddCategoriesBtnToList(categoryBtn);
    }

    public void CategorySelection ()
    {
        categoryPanel.SegregateCategory(categoryId);
    }
}
