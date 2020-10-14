using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //static instance of this script
    private static GameManager instance;

    //Category panel in which grid layout generates and hold all its values
    [SerializeField]
    private CategoryPanel categoryPanel;

    string[] category = { "Watches", "Cloths", "Jewels" };
    [SerializeField]
    private List<string> categoryList = new List<string>();

    [SerializeField]
    private List<CategoryInfos> categoryInfos = new List<CategoryInfos>();

    //Items panel in which grid layout generates and hold all its values
    [SerializeField]
    private ItemsPanel itemsPanel;

    [SerializeField]
    private List<ItemInfos> itemInfos = new List<ItemInfos>();

    int defaultItemCount = 10;
    [SerializeField]
    private List<int> itemCountList = new List<int>();

    int defaultMinPriceOfItemsInCategory = 500;
    int defaultMaxPriceOfItemsInCategory = 900;
    [SerializeField]
    private List<int> minPriceList = new List<int>();
    [SerializeField]
    private List<int> maxPriceList = new List<int>();

    string commonDescription = "Item description";
    [SerializeField]
    private List<string> commonDescriptionList = new List<string>();

    string[] gender = { "Men", "Women", "Boys", "Girls"};


    void Awake()
    {
        //Get instance of the script to access outside
        instance = this;

        //Disable logs except EDITOR
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }

    //Resturn script instance
    public static GameManager Instance()
    {
        return instance;
    }

    private void Start()
    {
        //Check available categories
        ValidateCategory();
    }

    //Validate whether categories available or not
    void ValidateCategory()
    {
        if (categoryList.Count <= 0)
        {
            //Add default categories
            categoryList.AddRange(category);
        }

        ValidateItemsDetails();
        SetCategoryList();
    }

    void SetCategoryList()
    {
        //Reset category, item infos
        categoryInfos.Clear();
        itemInfos.Clear();
        int categoryCount = categoryList.Count;

        if (categoryCount > 0)
        {
            for (int categoryIndex = 0; categoryIndex < categoryCount; categoryIndex++)
            {
                //Set Category info
                CategoryInfos info = new CategoryInfos();
                info.categoryId = categoryIndex;
                info.categoryName = categoryList[categoryIndex];
                categoryInfos.Add(info);

                //save items for each category
                SetItemDetails(itemCountList[categoryIndex], categoryIndex);
            }

            //Create Categories
            categoryPanel.CreateCategories(categoryInfos);
            //Create items
            GenerateItems();
        }
    }


    void ValidateItemsDetails()
    {
        if (itemCountList.Count != categoryList.Count)
        {
            //Add default count
            if (itemCountList.Count < categoryList.Count)
            {
                int diffInCount = categoryList.Count - itemCountList.Count;
                for (int addIndex = 0; addIndex < diffInCount; addIndex++)
                {
                    //Add - Match with category list count - by adding default item count to list
                    itemCountList.Add(defaultItemCount);
                }
            }
            else
            {
                for (int removeIndex = 0; removeIndex < itemCountList.Count; removeIndex++)
                {
                    //Remove - Match with category list count - by removing extra item count from list
                    if (removeIndex >= categoryList.Count)
                    {
                        itemCountList.RemoveAt(removeIndex);
                    }
                }
            }
           // itemCountList.AddRange(itemCount);
        }

        if (minPriceList.Count != categoryList.Count)
        {
            //Add min price
            if (minPriceList.Count < categoryList.Count)
            {
                int diffInCount = categoryList.Count - minPriceList.Count;
                for (int addIndex = 0; addIndex < diffInCount; addIndex++)
                {
                    //Add - Match with category list count - by adding default min price to list
                    minPriceList.Add(defaultMinPriceOfItemsInCategory);
                }
            }
            else
            {
                for (int removeIndex = 0; removeIndex < minPriceList.Count; removeIndex++)
                {
                    //Remove - Match with category list count - by removing extra min price from list
                    if (removeIndex >= categoryList.Count)
                    {
                        minPriceList.RemoveAt(removeIndex);
                    }
                }
            }

            //Avoid price below <= 0 from Editor
            for (int minIndex = 0; minIndex < minPriceList.Count; minIndex++)
            {
                if (minPriceList[minIndex] <= 0)
                    minPriceList[minIndex] = defaultMinPriceOfItemsInCategory;
            }

            //minPriceList.AddRange(minPriceOfItemsInCategory);
        }

        if (maxPriceList.Count != categoryList.Count)
        {
            //Add max price
            if (maxPriceList.Count < categoryList.Count)
            {
                int diffInCount = categoryList.Count - maxPriceList.Count;
                for (int addIndex = 0; addIndex < diffInCount; addIndex++)
                {
                    //Add - Match with category list count - by adding default max price to list
                    maxPriceList.Add(defaultMaxPriceOfItemsInCategory);
                }
            }
            else
            {
                for (int removeIndex = 0; removeIndex < maxPriceList.Count; removeIndex++)
                {
                    //Remove - Match with category list count - by removing extra max price from list
                    if (removeIndex >= categoryList.Count)
                    {
                        maxPriceList.RemoveAt(removeIndex);
                    }
                }
            }

            //Avoid price below <= 0 from Editor
            for (int maxIndex = 0; maxIndex < maxPriceList.Count; maxIndex++)
            {
                if (maxPriceList[maxIndex] <= 0)
                    maxPriceList[maxIndex] = defaultMaxPriceOfItemsInCategory;
            }

            //maxPriceList.AddRange(maxPriceOfItemsInCategory);
        }

        if (commonDescriptionList.Count != categoryList.Count)
        {
            //Add common description for item category
            if (commonDescriptionList.Count < categoryList.Count)
            {
                int diffInCount = categoryList.Count - commonDescriptionList.Count;
                for (int addIndex = 0; addIndex < diffInCount; addIndex++)
                {
                    //Add - Match with category list count - by adding common description to list
                    commonDescriptionList.Add(commonDescription);
                }
            }
            else
            {
                for (int removeIndex = 0; removeIndex < commonDescriptionList.Count; removeIndex++)
                {
                    //Remove - Match with category list count - by removing extra description from list
                    if (removeIndex >= categoryList.Count)
                    {
                        commonDescriptionList.RemoveAt(removeIndex);
                    }
                }
            }

            //Check for empty description added from Editor
            for (int desIndex = 0; desIndex < commonDescriptionList.Count; desIndex++)
            {
                if (commonDescriptionList[desIndex] == string.Empty)
                    commonDescriptionList[desIndex] = commonDescription;
            }
        }
    }

    void SetItemDetails(int itemCount, int categoryId)
    {
        int itemsCount = itemCount;

        if (itemsCount > 0)
        {
            for (int itemsIndex = 0; itemsIndex < itemsCount; itemsIndex++)
            {
                //Set item info
                ItemInfos info = new ItemInfos();
                info.itemId = itemsIndex;
                info.itemName = categoryList[categoryId] + itemsIndex;
                info.itemCategory = categoryList[categoryId];
                info.itemCategoryId = categoryId;
                //Assign random price
                info.itemPrice = Random.Range(minPriceList[categoryId], maxPriceList[categoryId]);
                info.itemDescription = commonDescriptionList[categoryId];
                //Assign random gender
                int index = Random.Range(0, gender.Length);
                info.itemGender = gender[index];

                info.itemRating = Random.Range(1f,5f);

                itemInfos.Add(info);
            }
        }
    }

    void GenerateItems ()
    {
        //Shuffle the info list - to generate items in different order - mix
        for (int infoIndex = 0; infoIndex < itemInfos.Count; infoIndex++)
        {
            ItemInfos temp = itemInfos[infoIndex];
            int randomIndex = Random.Range(infoIndex, itemInfos.Count);
            itemInfos[infoIndex] = itemInfos[randomIndex];
            itemInfos[randomIndex] = temp;
        }

        //Create Items
        itemsPanel.CreateItems(itemInfos);
    }
}
