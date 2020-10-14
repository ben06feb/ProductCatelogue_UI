using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterPanel : MonoBehaviour
{
    //Filter details UI set
    [SerializeField]
    private GameObject filterUISetGObj;

    //Main items grid panel access
    [SerializeField]
    private ItemsPanel itemsPanel;
    //Item details panel access
    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;
    //Toggle group - gender
    [SerializeField]
    private ToggleGroup genderToggleGroup;
    //Filter UI gObj
    [SerializeField]
    private GameObject filterUIGObj;
    //Items display panel
    [SerializeField]
    private GameObject itemsUIGObj;


    private void Start()
    {
        //Make all toggle details off
        genderToggleGroup.SetAllTogglesOff(true);
    }

    //To active/in-active filter
    public void SetFilterUIStatus(bool status)
    {
        filterUISetGObj.SetActive(status);
        if (status == true)
        {
            float filterUIWidth = filterUIGObj.GetComponent<RectTransform>().rect.width;
            Debug.Log("filter UI width - " + filterUIWidth);
            //Move items panel to right
            itemsUIGObj.GetComponent<RectTransform>().offsetMin = new Vector2(filterUIWidth, 0);
        }
    }

    //Filter details when gender details selected
    public void FilterItems (Toggle toggle)
    {
        itemsPanel.FilterItemsBasedOnSelections(toggle);
    }

    //Reset filter and show all items in selected category
    public void ResetFilter ()
    {
        genderToggleGroup.SetAllTogglesOff(true);
        itemsPanel.ResetFilterDetails();
    }

    //Close filter panel by resetting the details
    public void CloseFilterWithReset ()
    {
        genderToggleGroup.SetAllTogglesOff(true);
        itemsPanel.ResetFilterDetails();
        SetFilterUIStatus(false);
        //Reset item details UI position
        itemsUIGObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }
}
