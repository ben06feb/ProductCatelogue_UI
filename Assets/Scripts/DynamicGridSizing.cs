using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSizing : MonoBehaviour
{
    private float width; //To get width of content rect transform

    ////Values to calculate based on mode for 9:16/16:9 resolution on full screen
    //[SerializeField]
    //private float landscapeBy = 4.2f;
    //[SerializeField]
    //private float portraitBy = 1.1f;

    private void Update()
    {
        //Update grid size based on landscape/portrait mode change
        GetGridSize();
    }

    //Find grid size using screen with and height in calulation with display content rect.
    //Apply to the calculated size to grids.
    //Calculations are made for 9:16/16:9 screen resolution
    void GetGridSize ()
    {
        float widthHeight = 0f;
        //Find width of content rect
        width = gameObject.GetComponent<RectTransform>().rect.width;

        if (Screen.width > Screen.height)
            widthHeight = (width/ 3)-30f; //In landscape mode
        else
            widthHeight = width - 40f; //In Portrait mode

        //In device - can make mode check using below and sub sequent orientation properties
        //Input.deviceOrientation == DeviceOrientation.LandscapeLeft
        
        Vector2 newSize = new Vector2(widthHeight, widthHeight);
        //update grid cell size in grid layout
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
