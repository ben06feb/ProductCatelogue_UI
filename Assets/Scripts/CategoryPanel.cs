using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Cateory info - index and name
[System.Serializable]
public struct CategoryInfos
{
    public int categoryId;
    public string categoryName;
}

public class CategoryPanel : MonoBehaviour
{
    //Individual category details - to generate and set properties
    [SerializeField]
    private CategoryInfo category;

    //Hold all category details
    [SerializeField]
    private List<CategoryInfo> allCategories = new List<CategoryInfo>();

    //Hold all category button property
    [SerializeField]
    private List<Button> allCategoriesBtn = new List<Button>();

    //Hold all category info - to view
    [SerializeField]
    private List<CategoryInfos> categoryInfo = new List<CategoryInfos>();

    //Selected category
    [SerializeField]
    private CategoryInfo highlightedCategory;

    //Colors based on selection
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color onSelectionColor;

    [SerializeField]
    private Button clearBtn;

    //Main items grid panel access
    [SerializeField]
    private ItemsPanel itemsPanel;
    //Item details panel access
    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    //Generate category 
    public void CreateCategories(List<CategoryInfos> infos)
    {
        //Reset values
        category.SetStatusTo(true);
        ClearAllSlides();

        Transform parent = category.GetParent();
        for (int gridIndex = 0; gridIndex < infos.Count; gridIndex++)
        {
            CategoryInfos info = infos[gridIndex];
            CategoryInfo newCategory = null;
            newCategory = Instantiate(category, parent) as CategoryInfo;

            //Set properties to category
            if (newCategory != null)
            {
                newCategory.SetStatusTo(true);

                int index = info.categoryId;
                newCategory.name = info.categoryName;
                newCategory.SetBasicInfo(index, info.categoryName);
                newCategory.SetCategorySeletionStatus(true, normalColor);
                newCategory.GetCategoryBtn();

                allCategories.Add(newCategory);
            }
        }

        //Save all category info
        categoryInfo = infos;
        //Disable sample category
        category.SetStatusTo(false);
        //Set clear button in-active
        clearBtn.interactable = false;
    }

    //Reset all details
    public void ClearAllSlides()
    {
        foreach (CategoryInfo tile in allCategories)
        {
            tile.SelfDestroy();
        }
        allCategories.Clear();
        allCategoriesBtn.Clear();
    }

    //Add generated category button to list
    public void AddCategoriesBtnToList(Button button)
    {
        allCategoriesBtn.Add(button);
    }

    public void SegregateCategory (int categoryId)
    {
        for (int categoryIndex = 0; categoryIndex < allCategoriesBtn.Count; categoryIndex++)
        {
            //Reset all category button
            allCategories[categoryIndex].SetCategorySeletionStatus(true, normalColor);
        }
        //Update selected category details
        allCategories[categoryId].SetCategorySeletionStatus(false, onSelectionColor);
        //Set clear button active
        clearBtn.interactable = true;

        //Filter out selected category items
        itemsPanel.OnCategorySelection(categoryId);
        //Disable item details displaying
        itemDetailsPanel.SetDetailsUIStatus(false);
    }

    public void ClearAllSelection ()
    {
        for (int categoryIndex = 0; categoryIndex < allCategoriesBtn.Count; categoryIndex++)
        {
            //Reset all category button
            allCategories[categoryIndex].SetCategorySeletionStatus(true, normalColor);
        }

        //Set clear button in-active
        clearBtn.interactable = false;
        //Clear filter - show all items
        itemsPanel.OnClearingCategorySelection();
        //Disable item details displaying
        itemDetailsPanel.SetDetailsUIStatus(false);
    }
}
