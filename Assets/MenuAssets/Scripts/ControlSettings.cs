using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettings : MonoBehaviour
{
    private GameObject objControlsPanel;
    private RectTransform objControlsRect;
    private float y;
    private float yChange;
    void Start()
    {
        objControlsPanel = GameObject.FindGameObjectWithTag("PageControlls");
        objControlsRect = objControlsPanel.GetComponent<RectTransform>();
    }

    //70 e 74
    public void OnSelect()
    {
        yChange = objControlsRect.anchoredPosition.y;
        if (GetComponent<RectTransform>().anchoredPosition.y > 110 && yChange != 67.9) yChange -= 67.9f;
        else if (GetComponent<RectTransform>().anchoredPosition.y < -125 && yChange != -67.9) yChange += 67.9f;

        objControlsRect.anchoredPosition = new Vector2(objControlsRect.anchoredPosition.x, yChange);
    }

    void Update()
    {
        y = objControlsRect.anchoredPosition.y;

        if (y > 67.9 || y < -67.9)
        {
            objControlsRect.anchoredPosition = new Vector2(
                objControlsRect.anchoredPosition.x, y > 67.9 ? 67.9f : -67.9f);
        }
    }
}
