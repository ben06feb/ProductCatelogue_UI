using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    //Toggle group - gender
    [SerializeField]
    private Toggle selectedGenderToggle;
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
        selectedGenderToggle = toggle;
        itemsPanel.FilterItemsBasedOnSelections(toggle);

        //Get active filter details
        //There is no filter applied - show all details
        IEnumerable<Toggle> activeGenderToggle = genderToggleGroup.ActiveToggles();
        List<Toggle> activeGenderToggleList = activeGenderToggle.ToList();
        if(activeGenderToggleList.Count <= 0)
        {
            ResetFilter();
        }
    }

    //Reset filter and show all items in selected category
    public void ResetFilter ()
    {
        ClearSelectedGenderDetails();
        genderToggleGroup.SetAllTogglesOff(true);
        itemsPanel.ResetFilterDetails();
    }

    //Close filter panel by resetting the details
    public void CloseFilterWithReset ()
    {
        ClearSelectedGenderDetails();
        genderToggleGroup.SetAllTogglesOff(true);
        itemsPanel.ResetFilterDetails();
        SetFilterUIStatus(false);
        //Reset item details UI position
        ResetItemUIPosition();
    }

    //Make selected gender selection null
    public void ClearSelectedGenderDetails ()
    {
        selectedGenderToggle = null;
    }

    //Get selected toggle for gender
    public Toggle GetSelectedGenderToggleDetails ()
    {
        return selectedGenderToggle;
    }

    public void ResetItemUIPosition ()
    {
        //Reset item details UI position
        itemsUIGObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }
}
