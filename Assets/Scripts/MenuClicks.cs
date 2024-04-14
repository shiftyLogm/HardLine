using NovaSamples.Effects;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MenuClicks : MonoBehaviour
{
    public static bool SetMenuNemGame;
    public static bool SetMenuOptions;
    public static bool resetOptions = false;
    public RectTransform MainMenuRect;
    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public Image BonfireReal;
    public RectTransform bonfirePos;
    public RectTransform BonfireFalse;
    public GameObject blockblur_obj;
    public RectTransform blockblur_rect;
    public RectTransform PanelNewGame;
    public RectTransform PanelMainMenu;
    private Vector3 InitialVectorButtonMenu;
    private Vector3 TargetVectorButtonMenu;
    private GameObject[] buttonsMenu;
    public List<Button> buttonComponents = new List<Button>();
    public List<ButtonMenuHover> eventsHover = new List<ButtonMenuHover>();

    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        BonfireFalse.anchoredPosition = new Vector2(2891, -367.6f);
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        PanelNewGame.anchoredPosition = new Vector2(-1920, -.46f);
        bonfirePos.anchoredPosition = new Vector2(570, -367.7f);
        blockblur_rect.anchoredPosition = new Vector2(504, -356);
        PanelMainMenu.anchoredPosition = new Vector2(0, 0);
        blockblur_obj.SetActive(false);
        buttonsMenu = GameObject.FindGameObjectsWithTag("ButtonMenu");
        InitialVectorButtonMenu = buttonsMenu[0].transform.localScale;
        TargetVectorButtonMenu = new Vector3(2.35f, 2.35f, 1.175f);
    }

    private void DisableAndEnableOnClick(List<Button> list, bool value)
    {
        foreach(var button in list) button.interactable = value;
    }

    private void DisableHoverButton(List<ButtonMenuHover> list, Color color, Vector3 vector)
    {
        foreach(var idx in list)
        {
            var a = idx.GetComponent<ButtonMenuHover>();
            a.hoverColor = color;
            a.hoverVector = vector;
        }
    }

    private void turnButtonsNormal()
    {
        foreach (var button in buttonsMenu)
        {
            var hoverComp = button.GetComponent<ButtonMenuHover>();
            hoverComp.hoverColor = Color.yellow;
            hoverComp.hoverVector = TargetVectorButtonMenu;
        }
    }

    public void NewGameButtonClick()
    {
        BonfireFalse.transform.position = new Vector2(2891, -1121);
        LeanTween.move(blockblur_rect, new Vector2(2457, -356), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-4, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelMainMenu, new Vector2(1920, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
    }

    public void OptionsButtonClick()
    {
        LeanTween.alpha(BonfireReal.rectTransform, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0f, -700f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 0), .5f).setEase(LeanTweenType.easeInOutQuad); 
        LeanTween.value(gameObject, updateBlur, 0f, 30f, .5f).setEase(LeanTweenType.easeInOutQuad);
        Invoke("activateBlur", .25f);
        blockblur_obj.SetActive(true);
        SetMenuOptions = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    void desactivateBlur() => blockblur_obj.SetActive(false);
    void activateBlur() => blockblur_obj.SetActive(true); 
      
    public void ArrowButtonClick() 
    {
        LeanTween.alpha(BonfireReal.rectTransform, 1f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0, -330.6f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 1054), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, updateBlur, 30f, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        Invoke("desactivateBlur", .25f);
        SetMenuOptions = false;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
    }

    public void ArrowButtonClickNewGame()
    {
        BonfireFalse.anchoredPosition = new Vector2(2891, -367.6f);
        LeanTween.move(PanelMainMenu, new Vector2(0, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(blockblur_rect, new Vector2(504, -356), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-1920, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = false;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
    }

    void updateBlur(float val) => BlurEffect.blurRadius = val;
    void Update()
    {  
        AdjustBloom.valueIntensity = OptionsMenu.anchoredPosition.y <= 200 ? 0 : 1;
        resetOptions = OptionsMenu.anchoredPosition.y > 1000 ? true : false;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SetMenuOptions) ArrowButtonClick();
            if (SetMenuNemGame) ArrowButtonClickNewGame();
        }
    }
};