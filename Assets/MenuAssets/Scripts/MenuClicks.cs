using NovaSamples.Effects;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class MenuClicks : MonoBehaviour
{
    public Volume globalVolume;
    public static bool SetMenuNemGame;
    public static bool SetMenuOptions;
    public static bool resetOptions = false;
    public RectTransform MainMenuRect;
    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public RectTransform PanelNewGame;
    public RectTransform PanelMainMenu;
    public RectTransform GameLogo;
    private Vector3 InitialVectorButtonMenu;
    private Vector3 TargetVectorButtonMenu;
    private GameObject[] buttonsMenu;
    public List<Button> buttonComponents = new List<Button>();
    public List<ButtonMenuHover> eventsHover = new List<ButtonMenuHover>();
    private bool waitForNewGameScreen = false;
    private bool waitForOptionsScreen = false;

    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        PanelNewGame.anchoredPosition = new Vector2(-1920, -.46f);
        PanelMainMenu.anchoredPosition = new Vector2(0, 0);
        buttonsMenu = GameObject.FindGameObjectsWithTag("ButtonMenu");
        InitialVectorButtonMenu = buttonsMenu[0].transform.localScale;
        TargetVectorButtonMenu = new Vector3(2.35f, 2.35f, 1.175f);
        GameLogo.anchoredPosition = new Vector2(0, 211);
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

    void EnableWaitForNewGameScreen() => waitForNewGameScreen = true;
    public void NewGameButtonClick()
    {
        LeanTween.move(PanelNewGame, new Vector2(-4, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelMainMenu, new Vector2(1920, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
        Invoke("EnableWaitForNewGameScreen", 1f);
    }

    void EnableWaitForOptionsScreen() => waitForOptionsScreen = true;
    public void OptionsButtonClick()
    {
        LeanTween.move(GameLogo, new Vector2(0, -700), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0f, -1241f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 0), .6f).setEase(LeanTweenType.easeInOutQuad); 
        SetMenuOptions = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
        Invoke("EnableWaitForOptionsScreen", .5f);
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }
      
    public void ArrowButtonClick() 
    {
        LeanTween.move(GameLogo, new Vector2(0, 211), .5f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(MainMenuRect, new (0, -330.6f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 1054), .5f).setEase(LeanTweenType.easeInOutQuad);
        SetMenuOptions = false;
        waitForOptionsScreen = false;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
    }

    public void ArrowButtonClickNewGame()
    {
        LeanTween.move(PanelMainMenu, new Vector2(0, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-1920, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = false;
        waitForNewGameScreen = false;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
    }

    void Update()
    {  
        resetOptions = OptionsMenu.anchoredPosition.y > 1000 ? true : false;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SetMenuOptions && waitForOptionsScreen) ArrowButtonClick();
            if (SetMenuNemGame && waitForNewGameScreen) ArrowButtonClickNewGame();
        }
    }
};