using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSizing : MonoBehaviour
{
    private float width; //To get width of content rect transform

    //Values to calculate based on mode for 9:16/16:9 resolution on full screen
    [SerializeField]
    private float landscapeBy = 4.2f;
    [SerializeField]
    private float portraitBy = 1.1f;

    private void Update()
    {
        //Update grid size based on landscape/portrait mode change
        GetGridSize(landscapeBy, portraitBy);
    }

    //Find grid size using screen with and height in calulation with display content rect.
    //Apply to the calculated size to grids.
    //Calculations are made for 9:16/16:9 screen resolution
    void GetGridSize (float landscapeVal, float portraitVal)
    {
        float dividedBy = 0f;

        if (Screen.width > Screen.height)
            dividedBy = landscapeVal; //In landscape mode
        else
            dividedBy = portraitVal; //In Portrait mode

        //In device - can make mode check using below and sub sequent orientation properties
        //Input.deviceOrientation == DeviceOrientation.LandscapeLeft

        //Find width of content rect
        width = gameObject.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / dividedBy, width / dividedBy);
        //update grid cell size in grid layout
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
