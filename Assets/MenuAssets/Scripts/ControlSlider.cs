using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlSlider : MonoBehaviour
{   
    private Slider slider;
    private bool changeValueSlider;
    private Button button;
    private Navigation buttonSelect;
    public Selectable tabLogoSound;
    public GameObject tabLogo;
    private bool turnMenu;

    public void OnClick()
    {
        slider = GetComponentInChildren<Slider>();
        button = GetComponent<Button>();
        changeValueSlider = true;
        buttonSelect = button.navigation;
        buttonSelect.selectOnLeft = null;
        button.navigation = buttonSelect;
        MenuClicks.SetMenuOptions = false;
        turnMenu = true;
    }

    public void OnDeselect()
    {
        try
        {
            changeValueSlider = false;
            buttonSelect.selectOnLeft = tabLogoSound;
            button.navigation = buttonSelect;
        }

        catch (NullReferenceException) {}
    }

    void EnableBoolMenuClicks() => MenuClicks.SetMenuOptions = true;  

    void Update()
    {
        if (changeValueSlider)
        {
            slider.value += Input.GetKeyDown(KeyCode.RightArrow) ? 0.1f : 
            Input.GetKeyDown(KeyCode.LeftArrow) ? -0.1f : 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && turnMenu)
        {
            OnDeselect();
            Invoke("EnableBoolMenuClicks", 0.1f);
            OptionsNavigate.setNavigateOptions = true;
            turnMenu = false;
        }
    }

}
